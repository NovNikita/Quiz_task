using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TaskTextController : MonoBehaviour
{
    private Text _myText;
    void Awake()
    {
        _myText = GetComponent<Text>();
    }

    public void SetGoalText(string goal)
    {
        _myText.text = "Find " + goal;
    }

    
    public void HideText()
    {
        _myText.DOFade(0, 0);
    }
}
