using UnityEngine;

[CreateAssetMenu(fileName = "NewBoard", menuName = "JamCityTechTest/New Board", order = 1)]
public class BoardScriptable : ScriptableObject
{
    public Vector2 BoardSize;
    public Vector2 BoardSpacing;
    public float RowOffset;
}