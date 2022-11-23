using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum Elements
    {
        StateText,
        Duration,
        PathLength
    }

    private void Awake()
    {
        EventManager.UpdateUIElement += UpdateElement;
    }

    private void Start()
    {
        EventManager.OnUILoaded();
    }

    [SerializeField] private TMP_Text[] _elements;

    private void UpdateElement(Elements element, string text)
    {
        _elements[(int)element].text = text;
    }

    private void OnDestroy()
    {
        EventManager.UpdateUIElement -= UpdateElement;
    }
}
