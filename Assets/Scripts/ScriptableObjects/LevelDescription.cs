using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelDescription 
{
    [SerializeField]
    private int _rowCount;

    [SerializeField]
    private int _columnCount;

    public int RowCount => _rowCount;

    public int ColumnCount => _columnCount;
}
