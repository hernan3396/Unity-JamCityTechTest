using UnityEngine;
using PathFinding;
using System.Collections.Generic;

public class Tile : MonoBehaviour, IAStarNode
{
    [SerializeField] private TileScriptable _data;
    private List<IAStarNode> _neighbours = new List<IAStarNode>();
    private TileHighlight _tileHighlight;

    private void Awake()
    {
        if (TryGetComponent(out TileHighlight tileHighlight))
            _tileHighlight = tileHighlight;
    }

    private void OnMouseDown()
    {
        if (_data.CanTravel)
            EventManager.OnTileSelected(this);
    }

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

    public TileHighlight GetTileHighlight
    {
        get { return _tileHighlight; }
    }

    public bool CanTravel
    {
        get { return _data.CanTravel; }
    }

    public int GetCost
    {
        get { return _data.TileCost; }
    }
}
