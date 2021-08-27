using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndGameSequence : MonoBehaviour
{
    [SerializeField]
    private Button _restartButton;

    [SerializeField]
    private Image _fadingBackground;

    [SerializeField]
    private float timeToFadeInRestartBackground = 2;

    [SerializeField]
    private float timeToFadeInOutWhiteBackground = 2;

    [SerializeField]
    private UnityEvent RestartScene;

    [SerializeField]
    private UnityEvent RestartAnimationComplete;



    public void InitiateEndGameSequence()
    {
        _restartButton.gameObject.SetActive(true);

        _fadingBackground.enabled = true;
        _fadingBackground.DOFade(1, timeToFadeInRestartBackground);
    }



    public void OnRestartButtonClick()
    {
        _restartButton.gameObject.SetActive(false);

        _fadingBackground.DOColor(Color.white, timeToFadeInOutWhiteBackground);

        StartCoroutine(WaitForWhiteBackgroundFadeIn(timeToFadeInOutWhiteBackground));
    }



    private IEnumerator WaitForWhiteBackgroundFadeIn(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        RestartScene?.Invoke();

        _fadingBackground.DOFade(0, timeToFadeInOutWhiteBackground);

        StartCoroutine(WaitForWhiteBackgroundFadeOut(timeToFadeInOutWhiteBackground));
    }



    private IEnumerator WaitForWhiteBackgroundFadeOut(float timeToWait)
    {
        yield return new WaitForSeconds(timeToFadeInOutWhiteBackground);

        _fadingBackground.color = Color.black;
        _fadingBackground.DOFade(0, 0);
        _fadingBackground.enabled = false;

        RestartAnimationComplete?.Invoke();
    }
}
