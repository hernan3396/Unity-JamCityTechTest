using UnityEngine;

[RequireComponent(typeof(TileHighlight))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileScriptable _data;
    [SerializeField] private Transform _model;
    private TileHighlight _tileHightlight;

    private void Awake()
    {
        _tileHightlight = GetComponent<TileHighlight>();
    }

    private void OnMouseEnter()
    {
        _tileHightlight.TileSelected(_model);
    }

    private void OnMouseExit()
    {
        _tileHightlight.TileUnselected(_model);
    }
}
