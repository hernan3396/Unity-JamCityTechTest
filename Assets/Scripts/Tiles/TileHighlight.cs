using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    enum HighlightState
    {
        Idle,
        Hovered,
        Selected,
        Path,
        Goal
    }
    [SerializeField] private HighlightState _state;

    [SerializeField] private TileHighlightingScriptable _data;
    private Transform _modelTransform;
    private Material _modelMaterial;
    private Vector3 _initPos;

    private void Awake()
    {
        _modelTransform = GetComponentInChildren<Transform>();
        _modelMaterial = GetComponentInChildren<MeshRenderer>().material;

        _initPos = _modelTransform.position;
        SwitchState(HighlightState.Idle);
    }

    private void Idle()
    {
        _modelTransform.position = _initPos;
        _modelMaterial.SetColor(_data.ColorProp, _data.UnselectedColor);
    }

    private void Hovered()
    {
        _modelTransform.position = _initPos + _data.HighlightPosition;
    }

    private void Selected()
    {
        _modelTransform.position = _initPos + _data.HighlightPosition;
        _modelMaterial.SetColor(_data.ColorProp, _data.SelectedColor);
    }

    private void Path()
    {
        _modelTransform.position = _initPos + _data.PathPosition;
        _modelMaterial.SetColor(_data.ColorProp, _data.PathColor);
    }

    private void Goal()
    {
        _modelTransform.position = _initPos + _data.HighlightPosition;
        _modelMaterial.SetColor(_data.ColorProp, _data.GoalColor);
    }

    private void SwitchState(HighlightState nextState)
    {
        _state = nextState;

        switch (_state)
        {
            case HighlightState.Idle:
                Idle();
                break;

            case HighlightState.Hovered:
                Hovered();
                break;

            case HighlightState.Selected:
                Selected();
                break;

            case HighlightState.Path:
                Path();
                break;

            case HighlightState.Goal:
                Goal();
                break;
        }
    }

    public void TileSelected()
    {
        SwitchState(HighlightState.Selected);
    }

    public void GoalSelected()
    {
        SwitchState(HighlightState.Goal);
    }

    public void TilePath()
    {
        if (_state == HighlightState.Idle)
            SwitchState(HighlightState.Path);
    }

    public void ResetSelection()
    {
        SwitchState(HighlightState.Idle);
    }

    private void OnMouseEnter()
    {
        if (_state == HighlightState.Idle)
            SwitchState(HighlightState.Hovered);
    }

    private void OnMouseExit()
    {
        if (_state == HighlightState.Hovered)
            SwitchState(HighlightState.Idle);
    }
}
