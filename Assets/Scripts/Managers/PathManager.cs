using UnityEngine;
using PathFinding;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    enum PathState
    {
        Starting,
        SelectingFirstTile,
        SelectingGoalTile,
        WaitingForConfirmation,
        ChoosingNewPath
    }

    [Header("Tiles")]
    [SerializeField] private Tile _startTile;
    [SerializeField] private Tile _goalTile;
    private IList<IAStarNode> _pathList;

    [Header("State")]
    [SerializeField] private PathState _pathState;
    private int _pathDuration = 0;
    private int _daysPassed = 0;

    [Header("States Text")]
    [SerializeField] private TextScriptable[] _texts;

    private void Awake()
    {
        EventManager.TileSelected += TileSelected;
        EventManager.UILoaded += OnUILoaded;
    }

    private void OnUILoaded()
    {
        _pathState = PathState.Starting;
        ChangePathState(PathState.SelectingFirstTile);
        EventManager.OnUpdateUIElement(UIManager.Elements.PathLength, string.Empty);
        EventManager.OnUpdateUIElement(UIManager.Elements.Duration, _daysPassed.ToString());
    }

    private void TileSelected(Tile selectedTile)
    {
        switch (_pathState)
        {
            case PathState.Starting:
                break;

            case PathState.SelectingFirstTile:
                SetStartTile(selectedTile);
                break;

            case PathState.SelectingGoalTile:
                SetGoalTile(selectedTile);
                break;

            case PathState.WaitingForConfirmation:
                CheckingGoal(selectedTile);
                break;

            case PathState.ChoosingNewPath:
                ChooseNewPath(selectedTile);
                break;
        }
    }

    private void ChangePathState(PathState state)
    {
        _pathState = state;
        EventManager.OnUpdateUIElement(UIManager.Elements.StateText, _texts[(int)state].Text);
    }

    private void SetStartTile(Tile selectedTile)
    {
        _startTile = selectedTile;
        _startTile.GetTileHighlight.TileSelected();
        EventManager.OnUpdateUIElement(UIManager.Elements.PathLength, string.Empty);

        ChangePathState(PathState.SelectingGoalTile);
    }

    private void SetGoalTile(Tile selectedTile)
    {
        _goalTile = selectedTile;

        _pathList = AStar.GetPath(_startTile, _goalTile);

        if (_pathList == null)
        {
            _goalTile.GetTileHighlight.GoalSelected();
            ChangePathState(PathState.ChoosingNewPath);
            return;
        }

        ShowPath();
        ChangePathState(PathState.WaitingForConfirmation);
    }

    private void CheckingGoal(Tile selectedTile)
    {
        UnselectPath();

        if (_goalTile != selectedTile)
        {
            _startTile.GetTileHighlight.TileSelected();
            SetGoalTile(selectedTile);
            return;
        }

        // aca setear los dias pasados
        _daysPassed += _pathDuration;
        EventManager.OnUpdateUIElement(UIManager.Elements.Duration, _daysPassed.ToString());

        SetStartTile(_goalTile);
        _goalTile = null;
    }

    private void ChooseNewPath(Tile selectedTile)
    {
        _startTile.GetTileHighlight.TileSelected();
        _goalTile.GetTileHighlight.ResetSelection();
        EventManager.OnUpdateUIElement(UIManager.Elements.PathLength, string.Empty);

        SetGoalTile(selectedTile);
    }

    private void ShowPath()
    {
        _pathDuration = 0;

        for (int index = 1; index < _pathList.Count; index++)
        {
            Tile tile = (Tile)_pathList[index];

            tile.GetTileHighlight.TilePath();
            _pathDuration += tile.GetCost;
        }

        EventManager.OnUpdateUIElement(UIManager.Elements.PathLength, "Path Cost: " + _pathDuration.ToString());
        _startTile.GetTileHighlight.TileSelected();
        _goalTile.GetTileHighlight.GoalSelected();
    }

    private void UnselectPath()
    {
        if (_pathList == null) return;

        foreach (Tile tile in _pathList)
            tile.GetTileHighlight.ResetSelection();

        _pathList.Clear();
    }

    private void OnDestroy()
    {
        EventManager.TileSelected -= TileSelected;
        EventManager.UILoaded -= OnUILoaded;
    }
}
