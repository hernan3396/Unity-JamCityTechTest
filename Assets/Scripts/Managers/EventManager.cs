using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<Tile> TileSelected;
    public static void OnTileSelected(Tile selectedTile) => TileSelected?.Invoke(selectedTile);

    public static event UnityAction<UIManager.Elements, string> UpdateUIElement;
    public static void OnUpdateUIElement(UIManager.Elements element, string text) => UpdateUIElement?.Invoke(element, text);

    public static event UnityAction UILoaded;
    public static void OnUILoaded() => UILoaded?.Invoke();
}
