using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

//animation sequence for first level
public class StartAnimation : MonoBehaviour
{
    [SerializeField]
    private float _bounceHeight = 1;

    [SerializeField]
    private float _bounceTime = 0.25f;

    [SerializeField]
    private Ease _easeType = Ease.InOutExpo;

    [SerializeField]
    private float bounceDelayTime = 0.15f;      //delay betweew animation of different cells

    [SerializeField]
    private float textFadeInDuration;

    [SerializeField]
    private Text _goalText;

    public void InitiateStartAnimation(List<CellController> cellList)
    {

        AnimateCells(cellList);
        AnimateGoalTextAppereance();
    }


    private void AnimateGoalTextAppereance()
    {
        _goalText.DOFade(1, textFadeInDuration);
    }


    private void AnimateCells(List<CellController> cellList)
    {
        for (int i = 0; i < cellList.Count; i++)
        {
            cellList[i].AnimateBounce(_bounceHeight, _bounceTime, bounceDelayTime * i + 1, _easeType);
        }
    }


}
