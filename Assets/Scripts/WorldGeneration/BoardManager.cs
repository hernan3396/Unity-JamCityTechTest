using UnityEngine;
using PathFinding;
using System.Collections.Generic;

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
        SetTileNeighbours();
    }

    /// <Summary>
    /// Generates board using data from _boardData
    /// </Summary>
    private void GenerateBoard()
    {
        // if tile size != 1, multiply row & column by tileSize
        _board = new Board((int)_boardData.BoardSize.x, (int)_boardData.BoardSize.y);

        Vector3 startingPos = _boardStartingPos.transform.position;

        for (int row = 0; row < _board.GetHeight; row++)
        {
            Vector3 rowPos = new Vector3(0, 0, row * _boardData.BoardSpacing.y) + startingPos;
            if (row % 2 == 0) rowPos += new Vector3(_boardData.RowOffset, 0, 0);

            for (int column = 0; column < _board.GetWidth; column++)
            {
                Vector3 tilePos = rowPos + new Vector3(column, 0, 0);
                _board.SetTile(column, row, GenerateRandomTile(tilePos));
            }
        }
    }

    /// <Summary>
    /// Picks a random tile from pool & instanciates it
    /// </Summary>
    public Tile GenerateRandomTile(Vector3 tilePos)
    {
        int randTile = Random.Range(0, _tiles.Length);

        GameObject go = Instantiate(_tiles[randTile], tilePos, Quaternion.identity, _tileParent);
        return go.GetComponent<Tile>();
    }

    /// <Summary>
    /// starts setting tile neighbour
    /// </Summary>
    public void SetTileNeighbours()
    {
        for (int row = 0; row < _board.GetHeight; row++)
            for (int col = 0; col < _board.GetWidth; col++)
            {
                Tile selectedTile = _board.GetTile(col, row);
                if (!selectedTile.CanTravel) continue;

                AddNeighbours(selectedTile, col, row);
            }
    }

    /// <Summary>
    /// Adds neighbours to tile neighbour list
    /// </Summary>
    private void AddNeighbours(Tile selectedTile, int currentCol, int currentRow)
    {
        List<IAStarNode> _neighbours = new List<IAStarNode>();

        int colOffset = currentCol;

        // (currentcol = 1, currentrow = 0)
        if (currentRow % 2 != 0)
        {
            colOffset -= 1;

            if (currentCol + 1 < _board.GetWidth && _board.GetTile(currentCol + 1, currentRow).CanTravel)
                _neighbours.Add(_board.GetTile(currentCol + 1, currentRow));
        }
        else
        {
            if (currentCol - 1 >= 0 && _board.GetTile(currentCol - 1, currentRow).CanTravel)
                _neighbours.Add(_board.GetTile(currentCol - 1, currentRow));
        }

        for (int rowNeighbour = currentRow - 1; rowNeighbour <= currentRow + 1; rowNeighbour++)
            for (int colNeighbour = colOffset; colNeighbour <= colOffset + 1; colNeighbour++)
            {
                if (rowNeighbour == currentRow && colNeighbour == currentCol) continue;
                if (!IsInBounds(colNeighbour, rowNeighbour)) continue;

                _neighbours.Add(_board.GetTile(colNeighbour, rowNeighbour));
            }

        selectedTile.SetNeighbours(_neighbours);
    }

    /// <Summary>
    /// Checks if adjacent tile is in bounds
    /// </Summary>
    private bool IsInBounds(int colNeighbour, int rowNeighbour)
    {
        if (colNeighbour < 0 || rowNeighbour < 0) return false;
        if (colNeighbour >= _board.GetWidth || rowNeighbour >= _board.GetHeight) return false;
        if (!_board.GetTile(colNeighbour, rowNeighbour).CanTravel) return false;

        return true;
    }
}