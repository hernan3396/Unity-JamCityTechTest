using UnityEngine;
using PathFinding;
using System.Collections.Generic;

[RequireComponent(typeof(TileHighlight))]
public class Tile : MonoBehaviour, IAStarNode
{
    [Header("Data")]
    [SerializeField] private TileScriptable _data;
    [SerializeField] private Transform _model;

    private List<IAStarNode> _neighbours = new List<IAStarNode>();
    private TileHighlight _tileHightlight;
    private bool _isSelected = false;

    private void Awake()
    {
        if (TryGetComponent(out TileHighlight tileHighlight))
            _tileHightlight = tileHighlight;
    }

    private void OnMouseDown()
    {
        _isSelected = !_isSelected;
    }

    private void OnMouseEnter()
    {
        if (_tileHightlight != null && !_isSelected)
            _tileHightlight.TileSelected(_model);
    }

    private void OnMouseExit()
    {
        if (_tileHightlight != null && !_isSelected)
            _tileHightlight.TileUnselected(_model);
    }

    public void Hightlight()
    {
        _tileHightlight.TileSelected(_model);
    }

    #region IAStarNodeRegion
    public IEnumerable<IAStarNode> Neighbours => _neighbours;

    public float CostTo(IAStarNode neighbour)
    {
        return ((Tile)neighbour)._data.TileCost;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        return ((Tile)target)._data.TileCost;
    }

    public void SetNeighbours(List<IAStarNode> neighbours)
    {
        _neighbours.AddRange(neighbours);
    }
    #endregion

}
