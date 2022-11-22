using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    [SerializeField] private TileHighlightingScriptable _data;

    public void TileSelected(Transform model)
    {
        model.position += _data.HighlightPosition;
        model.localScale += _data.HighlightSize;
    }

    public void TileUnselected(Transform model)
    {
        model.position -= _data.HighlightPosition;
        model.localScale -= _data.HighlightSize;
    }
}
