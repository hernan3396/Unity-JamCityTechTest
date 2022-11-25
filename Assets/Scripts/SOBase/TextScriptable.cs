using UnityEngine;

[CreateAssetMenu(fileName = "NewText", menuName = "JamCityTechTest/New Text", order = 3)]
public class TextScriptable : ScriptableObject
{
    [TextArea(5, 20)] public string Text;
}