using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private GameObject _boardStartingPos;
    [SerializeField] private BoardScriptable _boardData;
    private Board _board;

    [Header("Tiles")]
    [SerializeField] private Transform _tileParent;
    [SerializeField] private GameObject[] _tiles;

    private void Start()
    {
        _boardStartingPos.GetComponent<MeshRenderer>().enabled = false;

        GenerateBoard();
    }

    /// <Summary>
    /// Generates board using data from _boardData
    /// </Summary>
    private void GenerateBoard()
    {
        // if tile size != 1, multiply row & column by tileSize
        _board = new Board((int)_boardData.BoardSize.x, (int)_boardData.BoardSize.y);

        Vector3 startingPos = _boardStartingPos.transform.position;

        for (int row = 0; row < _board.GetPosY; row++)
        {
            Vector3 rowPos = new Vector3(0, 0, row * _boardData.BoardSpacing.y) + startingPos;
            if (row % 2 == 0) rowPos += new Vector3(_boardData.RowOffset, 0, 0);

            for (int column = 0; column < _board.GetPosX; column++)
            {
                Vector3 tilePos = rowPos + new Vector3(column, 0, 0);
                GenerateRandomTile(tilePos);
            }
        }
    }

    /// <Summary>
    /// Picks a random tile from tile pool
    /// </Summary>
    public void GenerateRandomTile(Vector3 tilePos)
    {
        int randTile = Random.Range(0, _tiles.Length);

        GameObject go = Instantiate(_tiles[randTile], tilePos, Quaternion.identity, _tileParent);
    }
}