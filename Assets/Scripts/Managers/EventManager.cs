using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<Tile> TileSelected;
    public static void OnTileSelected(Tile selectedTile) => TileSelected?.Invoke(selectedTile);
}
