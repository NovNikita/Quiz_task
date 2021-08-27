using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class generating and repositioning cells for the game
public class CellSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellPf;

    private List<CellController> _cellPool = new List<CellController>();

    private float _cellWidth, _cellHeight;



    private void Awake()
    {
        _cellWidth = _cellPf.transform.Find("Background").GetComponent<SpriteRenderer>().bounds.size.x;
        _cellHeight = _cellPf.transform.Find("Background").GetComponent<SpriteRenderer>().bounds.size.y;
    }



    public List<CellController> SpawnCells(int rowsToSpawn, int columnsToSpawn, bool spawnCellsDeactivated)
    {

        for (int i = 0; i < rowsToSpawn; i++)
        {
            for (int j =0; j < columnsToSpawn; j++)
            {

                float xPosToSpawn = -_cellWidth / 2 * (columnsToSpawn - 1) + _cellWidth * j;
                float yPosToSpawn = _cellHeight / 2 * (rowsToSpawn - 1) - _cellHeight * i;
                Vector3 positionToSpawn = new Vector3(xPosToSpawn, yPosToSpawn, 0);


                if (_cellPool.Count <= i * columnsToSpawn + j)
                {
                    _cellPool.Add(InstantiateNewCell(positionToSpawn).GetComponent<CellController>());
                }

                else
                {
                    _cellPool[i * columnsToSpawn + j].StopAnimation();

                    RepositionOldCell(_cellPool[i * columnsToSpawn + j].transform, positionToSpawn);
                }

            }
        }

        //cells are deactivated in first round, to be actived by animation
        if (spawnCellsDeactivated)
            DeactivateCells(_cellPool);

        return _cellPool;
    }



    private void DeactivateCells(List<CellController> cellList)
    {
        foreach (CellController cell in cellList)
        {
            cell.gameObject.SetActive(false);
        }
    }



    private void RepositionOldCell(Transform oldCell, Vector3 newPosition)
    {
        oldCell.transform.position = newPosition;
    }



    private GameObject InstantiateNewCell(Vector3 position)
    {
        return Instantiate(_cellPf, position, Quaternion.identity);
    }



    public void ResetCellPool()
    {
        for (int i=0; i<_cellPool.Count; i++)
        {
            Destroy(_cellPool[i].gameObject);
        }
        _cellPool.Clear();
    }
}
