using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//main class to process game loop
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelSequence _levelSequence;   //scriptable object storing configuration of levels

    [SerializeField]
    private CardBundleData[] _cardBundleData;

    [SerializeField]
    private float _timeDelayToNextLevel = 1.5f;

    private CellSpawner _cellSpawner;
    private CellContentManager _cellContentManager;
    private CorrectAnswerControl _correctAnswerControl;
    private EndGameSequence _endGameSequence;
    private StartAnimation _startAnimation;
    private List<CellController> _cellPool;

    private int _currentLevel = 0;



    private void Start()
    {
        _cellSpawner = GameObject.Find("CellSpawner").GetComponent<CellSpawner>();
        _cellContentManager = GameObject.Find("CellContentManager").GetComponent<CellContentManager>();
        _correctAnswerControl = GameObject.Find("CorrectAnswerControl").GetComponent<CorrectAnswerControl>();
        _endGameSequence = GameObject.Find("EndGameSequence").GetComponent<EndGameSequence>();
        _startAnimation = GameObject.Find("StartAnimation").GetComponent<StartAnimation>();

        _correctAnswerControl._levelSolved.AddListener(StartNextLevel);

        StartNewGame();
    }



    public void StartNewGame()
    {
        _currentLevel = 0;
        StartLevel(_currentLevel);
    }



    private void StartLevel(int levelToStart)
    {
        int rowsToSpawn = _levelSequence.LevelDescription[levelToStart].RowCount;
        int columnsToSpawn = _levelSequence.LevelDescription[levelToStart].ColumnCount;

        bool useAnimation = false;
        if (levelToStart == 0)
            useAnimation = true;

        _cellPool = _cellSpawner.SpawnCells(rowsToSpawn, columnsToSpawn, useAnimation);

        _cellContentManager.PopulateCells(_cellPool);

        _correctAnswerControl.RandomizeCorrectAnswer();

        if (useAnimation)
        {
            _startAnimation.InitiateStartAnimation(_cellPool);
        }
    }


    private void StartNextLevel()
    {
        StartCoroutine(WaitBeforeNextLevel());
    }



    private IEnumerator WaitBeforeNextLevel()
    {
        yield return new WaitForSeconds(_timeDelayToNextLevel);

        _currentLevel++;
        if (_currentLevel == _levelSequence.LevelDescription.Length)
        {
            _endGameSequence.InitiateEndGameSequence();
        }
        else
        {
            StartLevel(_currentLevel);
        }
    }

}
