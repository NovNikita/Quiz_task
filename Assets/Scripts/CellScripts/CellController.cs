using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


//cell logic attached to each cell
public class CellController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _myCardImageRenderer;

    private CardData _assignedCard;

    private CorrectAnswerControl _correctAnswerControl;
    private CellDOTWeenAnimator _myDOTWeenAnomator;



    private void Awake()
    {
        _correctAnswerControl = GameObject.Find("CorrectAnswerControl").GetComponent<CorrectAnswerControl>();
        _myDOTWeenAnomator = GetComponent<CellDOTWeenAnimator>();
    }



    public void AssignCard(CardData assignedCard)
    {
        _assignedCard = assignedCard;

        _myCardImageRenderer.sprite = _assignedCard.Sprite;

        _correctAnswerControl.AddCardInPlay(_assignedCard);
    }



    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
            if (_correctAnswerControl.CheckUserAnswer(_assignedCard))
            {
                _myDOTWeenAnomator.AnimateCorrectAnswer();
            }

            else
            {
                _myDOTWeenAnomator.WiggleAnimation();
            }
        }
    }



    public void AnimateBounce(float bounceHeight, float bounceTime, float delay, Ease easeType)
    {
        _myDOTWeenAnomator.AnimateBounce(bounceHeight, bounceTime, delay, easeType);
    }



    public void StopAnimation()
    {
        _myDOTWeenAnomator.StopAnimation();
    }
}
