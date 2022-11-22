using UnityEngine;

[CreateAssetMenu(fileName = "NewHighlight", menuName = "JamCityTechTest/New Highlights", order = 2)]
public class TileHighlightingScriptable : ScriptableObject
{
    public Vector3 HighlightPosition;
    public Vector3 HighlightSize;
    public Color UnselectedColor = Color.white;
    public Color SelectedColor = Color.green;
    public Color PathColor = Color.red;
}