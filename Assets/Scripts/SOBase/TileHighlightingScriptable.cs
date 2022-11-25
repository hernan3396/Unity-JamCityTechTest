using UnityEngine;

[CreateAssetMenu(fileName = "NewHighlight", menuName = "JamCityTechTest/New Highlights", order = 2)]
public class TileHighlightingScriptable : ScriptableObject
{
    [Header("Positions")]
    public Vector3 HighlightPosition;
    public Vector3 PathPosition;

    [Header("Colors")]
    public Color UnselectedColor = Color.white;
    public Color SelectedColor = Color.green;
    public Color GoalColor = Color.yellow;
    public Color PathColor = Color.red;

    [Header("Shader props")]
    public string ColorProp = "_Color";
}