using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "JamCityTechTest/New Tile", order = 0)]
public class TileScriptable : ScriptableObject
{
    public string TileName;
    [Range(0, 10)] public int TileCost;
    public bool CanTravel = true;
}