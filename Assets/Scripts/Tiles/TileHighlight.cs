using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    [SerializeField] private TileHighlightingScriptable _data;
    private Transform _modelTransform;
    private Material _modelMaterial;

    private void Awake()
    {
        _modelTransform = GetComponentInChildren<Transform>();
        _modelMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    private void TileHovered()
    {
        _modelTransform.position += _data.HighlightPosition;
    }

    private void TileUnhovered()
    {
        _modelTransform.position -= _data.HighlightPosition;
    }

    public void TileSelected()
    {
        _modelMaterial.SetColor("_Color", _data.SelectedColor);
    }

    public void TilePath()
    {
        _modelMaterial.SetColor("_Color", _data.PathColor);
    }

    public void ResetSelection()
    {
        _modelMaterial.SetColor("_Color", _data.UnselectedColor);
    }

    private void OnMouseEnter()
    {
        TileHovered();
    }

    private void OnMouseExit()
    {
        TileUnhovered();
    }
}
