using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private GameObject _boardStartingPos;
    [SerializeField] private BoardScriptable _boardData;

    [Header("Tiles")]
    [SerializeField] private Transform _tileParent;
    [SerializeField] private GameObject[] _tiles;

    private void Start()
    {
        _boardStartingPos.GetComponent<MeshRenderer>().enabled = false;

        GenerateBoard();
    }

    private void GenerateBoard()
    {
        Vector3 startingPos = _boardStartingPos.transform.position;
        Vector3 rowStartingPoint;

        for (int i = 0; i < _boardData.BoardSize.y; i++)
        {
            float tileOffsetY = i * _boardData.BoardSpacing.y;

            float offset = 0;
            if (i % 2 == 0) offset = _boardData.RowOffset;

            rowStartingPoint = new Vector3(startingPos.x + offset, 0, startingPos.z + tileOffsetY);

            FillRow(rowStartingPoint);
        }
    }

    private void FillRow(Vector3 startingPos)
    {
        Vector3 tilePos = startingPos;

        for (int i = 0; i < _boardData.BoardSize.x; i++)
        {
            float tileOffset = i * _boardData.BoardSpacing.x;
            tilePos = new Vector3(startingPos.x + tileOffset, 0, tilePos.z);

            GenerateRandomTile(tilePos);
        }
    }

    public void GenerateRandomTile(Vector3 tilePos)
    {
        int randTile = Random.Range(0, _tiles.Length);

        Instantiate(_tiles[randTile], tilePos, Quaternion.identity, _tileParent);
    }
}