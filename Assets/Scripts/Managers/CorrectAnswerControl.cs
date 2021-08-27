using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//class to randomly select correct answer, storing previously used cards
public class CorrectAnswerControl : MonoBehaviour
{
    private CardData _correctAnswer;
    private List<CardData> _currentCardsInPlay = new List<CardData>();
    private List<CardData> _alreadyUsedCards = new List<CardData>();

    [SerializeField]
    private TaskTextController _taskTextController;

    public UnityEvent _levelSolved;


    //receive set of cards assigned to cells in current level
    public void AddCardInPlay(CardData cardToAdd)
    {
        _currentCardsInPlay.Add(cardToAdd);
    }


    //choose one card amongst ones used in current level, excluding cards from previous levels
    public void RandomizeCorrectAnswer()
    {
        int i = 0;
        while(true)
        {
            //failsafe to prevent infinite loop
            i++;
            if (i>1000)
            {
                Debug.Log("Error: All cards were already used");
                break;
            }
            //failsafe to prevent infinite loop

            int randomIndex = Random.Range(0, _currentCardsInPlay.Count);

            if (_alreadyUsedCards == null)
            {
                _correctAnswer = _currentCardsInPlay[randomIndex];
                break;
            }
            else
            {
                if (_alreadyUsedCards.Contains(_currentCardsInPlay[randomIndex]))
                {
                    continue;
                }
                else
                {
                    _correctAnswer = _currentCardsInPlay[randomIndex];
                    break;
                }
            }
        }

        _taskTextController.SetGoalText(_correctAnswer.Identifier);
    }


    //check if user answer is correct
    public bool CheckUserAnswer(CardData answerToCheck)
    {
        if (answerToCheck == _correctAnswer)
        {
            _alreadyUsedCards.Add(answerToCheck);
            _currentCardsInPlay.Clear();
            _levelSolved?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }



    public void ResetState()
    {
        _correctAnswer = null;
        _currentCardsInPlay.Clear();
        _alreadyUsedCards.Clear();
}
}
