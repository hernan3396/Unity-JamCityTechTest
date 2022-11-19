using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "JamCityTechTest/New Tile", order = 0)]
public class TileScriptable : ScriptableObject
{
    public string TileName;
    public int TileCost;
    public Material TileMaterial;
}