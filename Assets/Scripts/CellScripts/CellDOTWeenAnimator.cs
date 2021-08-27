using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


//animation class attached to each cell
public class CellDOTWeenAnimator : MonoBehaviour
{
	private Transform _cardImageTransform;
	private Tween _bounceTween;
	private bool animating = false;

	[SerializeField]
	private float _bounceHeight = 0.1f;

	[SerializeField]
	private float _bounceTime = 0.25f;

	[SerializeField]
	private Ease _easeType = Ease.InOutExpo;

	[SerializeField]
	private ParticleSystem _myParticles;



	private void Start()
    {
        _cardImageTransform = transform.Find("CardImage");
    }


	//bounc effect to spawn in first level with activating itself
    public void AnimateBounce(float bounceHeight, float bounceTime, float delay, Ease easeType)
    {
		Invoke("ActivateItself", delay);

		_bounceTween = transform
				.DOMoveY(transform.position.y + bounceHeight, bounceTime / 2)
				.SetDelay(delay)
				.SetEase(easeType)
				.OnComplete(() =>
				{
					transform
						.DOMoveY(transform.position.y - bounceHeight, bounceTime / 2)
						.SetEase(easeType);
				});
	}



	public void WiggleAnimation()
    {
		if (!animating)
		{
			animating = true;

			Transform originalTransform = _cardImageTransform;
			Sequence mySequence = DOTween.Sequence()
			.Append(_cardImageTransform.DOMoveX(originalTransform.position.x + 0.1f, 0.05f))
			.Append(_cardImageTransform.DOMoveX(originalTransform.position.x, 0.05f))
			.Append(_cardImageTransform.DOMoveX(originalTransform.position.x - 0.1f, 0.05f))
			.Append(_cardImageTransform.DOMoveX(originalTransform.position.x, 0.05f))
			.SetLoops(3)
			.OnComplete(() =>
			{
				animating = false;
			});


			mySequence.Play();
		}
	}



	public void AnimateCorrectAnswer()
    {
		if (!animating)
		{
			_myParticles.Play();

			animating = true;

			_cardImageTransform
				.DOMoveY(_cardImageTransform.position.y + _bounceHeight, _bounceTime / 2)
				.SetEase(_easeType)
				.OnComplete(() =>
				{
					_cardImageTransform
						.DOMoveY(_cardImageTransform.position.y - _bounceHeight, _bounceTime / 2)
						.SetEase(_easeType)
						.OnComplete(() =>
						{
							animating = false;
						});
				});
		}
	}



	private void ActivateItself()
    {
		gameObject.SetActive(true);
    }



	public void StopAnimation()
    {
		_bounceTween.Kill();  //do not work, requires research
    }
}
