using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelSequence", menuName = "Level Sequence", order = 11)]
public class LevelSequence : ScriptableObject
{
    [SerializeField]
    private LevelDescription[] _levelDescription;

    public LevelDescription[] LevelDescription => _levelDescription;
}
