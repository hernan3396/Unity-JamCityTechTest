using UnityEngine;
using PathFinding;
using System.Collections.Generic;
public class PathManager : MonoBehaviour
{
    [SerializeField] private Tile _startTile;
    [SerializeField] private Tile _goalTile;
    private IList<IAStarNode> _pathList;

    private void Awake()
    {
        EventManager.TileSelected += TileSelected;
    }

    // needs refactoring

    private void TileSelected(Tile selectedTile)
    {
        if (_startTile == null)
        {
            _startTile = selectedTile;
            _startTile.GetTileHighlight.TileSelected();
            return;
        }

        if (_goalTile == null)
        {
            _goalTile = selectedTile;
            _pathList = AStar.GetPath(_startTile, _goalTile);

            SelectPath();

            _startTile.GetTileHighlight.TileSelected();
            _goalTile.GetTileHighlight.TileSelected();
            return;
        }

        if (_goalTile != selectedTile)
        {
            _goalTile.GetTileHighlight.ResetSelection();

            UnselectPath();

            _goalTile = selectedTile;

            _pathList = AStar.GetPath(_startTile, _goalTile);

            SelectPath();

            _goalTile.GetTileHighlight.TileSelected();
            _startTile.GetTileHighlight.TileSelected();
            return;
        }

        if (_goalTile == selectedTile)
        {
            _startTile.GetTileHighlight.ResetSelection();

            UnselectPath();

            _startTile = _goalTile;
            _startTile.GetTileHighlight.TileSelected();
            _goalTile = null;
        }
    }

    private void SelectPath()
    {
        foreach (Tile tile in _pathList)
            tile.GetTileHighlight.TilePath();
    }

    private void UnselectPath()
    {
        foreach (Tile tile in _pathList)
            tile.GetTileHighlight.ResetSelection();
    }

    private void OnDestroy()
    {
        EventManager.TileSelected -= TileSelected;
    }
}
