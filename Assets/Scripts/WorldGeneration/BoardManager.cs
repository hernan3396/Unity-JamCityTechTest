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
    /// sets tile neighbour
    /// </Summary>
    public void SetTileNeighbours()
    {
        for (int row = 0; row < _board.GetHeight; row++)
            for (int col = 0; col < _board.GetWidth; col++)
            {
                Tile selectedTile = _board.GetTile(col, row);

                if (!selectedTile.CanTravel) continue;

                if (row % 2 == 0) AddNeighboursEvenRow(col, row, selectedTile);
                else AddNeighboursUnevenRow(col, row, selectedTile);
            }
    }

    // needs refactoring

    private void AddNeighboursEvenRow(int col, int row, Tile selectedTile)
    {
        List<IAStarNode> _neighbours = new List<IAStarNode>();

        if (col - 1 > 0 && _board.GetTile(col - 1, row).CanTravel)
            _neighbours.Add(_board.GetTile(col - 1, row)); // if exists a tile to the left we add it

        for (int rowNeighbour = row - 1; rowNeighbour <= row + 1; rowNeighbour++)
            for (int colNeighbour = col; colNeighbour <= col + 1; colNeighbour++)
            {
                if (rowNeighbour < 0 || colNeighbour < 0) continue;
                if (rowNeighbour >= _board.GetHeight || colNeighbour >= _board.GetWidth) continue;
                if (rowNeighbour == row && colNeighbour == col) continue;
                if (!_board.GetTile(colNeighbour, rowNeighbour).CanTravel) continue;

                _neighbours.Add(_board.GetTile(colNeighbour, rowNeighbour));
            }

        selectedTile.SetNeighbours(_neighbours);
    }

    private void AddNeighboursUnevenRow(int col, int row, Tile selectedTile)
    {
        List<IAStarNode> _neighbours = new List<IAStarNode>();

        if (col + 1 < _board.GetWidth && _board.GetTile(col + 1, row).CanTravel)
            _neighbours.Add(_board.GetTile(col + 1, row)); // if exists a tile to the right we add it

        // checks for neighbours
        for (int rowNeighbour = row - 1; rowNeighbour <= row + 1; rowNeighbour++)
            for (int colNeighbour = col - 1; colNeighbour <= col; colNeighbour++)
            {
                // checking out of bounds
                if (rowNeighbour < 0 || colNeighbour < 0) continue;
                if (rowNeighbour >= _board.GetHeight || colNeighbour >= _board.GetWidth) continue;
                if (rowNeighbour == row && colNeighbour == col) continue;
                if (!_board.GetTile(colNeighbour, rowNeighbour).CanTravel) continue;

                _neighbours.Add(_board.GetTile(colNeighbour, rowNeighbour));
            }

        selectedTile.SetNeighbours(_neighbours);
    }
}