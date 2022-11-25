using UnityEngine;
using PathFinding;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    enum PathState
    {
        Idle,
        StartTileSelected,
        GoalTileSelected,
        InvalidPath
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
        SwitchState(PathState.Idle);

        UpdatePathCounter(string.Empty);
        UpdateDayCounter();
    }

    #region States
    private void SwitchState(PathState state)
    {
        _pathState = state;
        EventManager.OnUpdateUIElement(UIManager.Elements.StateText, _texts[(int)state].Text);
    }

    /// <Summary>
    /// Works similar to a state machine but simpler
    /// </Summary>
    private void TileSelected(Tile selectedTile)
    {
        switch (_pathState)
        {
            case PathState.Idle:
                Idle(selectedTile);
                break;

            case PathState.StartTileSelected:
                StartTileSelected(selectedTile);
                break;

            case PathState.GoalTileSelected:
                GoalTileSelected(selectedTile);
                break;

            case PathState.InvalidPath:
                InvalidPath(selectedTile);
                break;
        }
    }

    private void Idle(Tile selectedTile)
    {
        _startTile = selectedTile;
        _startTile.GetTileHighlight.TileSelected();
        UpdatePathCounter(string.Empty);

        SwitchState(PathState.StartTileSelected);
    }

    private void StartTileSelected(Tile selectedTile)
    {
        _goalTile = selectedTile;
        _goalTile.GetTileHighlight.GoalSelected();

        _pathList = AStar.GetPath(_startTile, _goalTile);

        if (_pathList == null)
        {
            UpdatePathCounter("Invalid");
            SwitchState(PathState.InvalidPath);
            return;
        }

        ShowPath();
        SwitchState(PathState.GoalTileSelected);
    }

    private void GoalTileSelected(Tile selectedTile)
    {
        UnselectPath();

        if (_goalTile != selectedTile)
        {
            InvalidPath(selectedTile);
            return;
        }

        MovePlayer();
    }

    private void InvalidPath(Tile selectedTile)
    {
        _goalTile.GetTileHighlight.ResetSelection();
        StartTileSelected(selectedTile);
    }
    #endregion

    /// <Summary>
    /// Simulates a player "movement" in the board when you confirm
    /// </Summary>
    private void MovePlayer()
    {
        UpdateDayCounter();

        _startTile.GetTileHighlight.ResetSelection();

        Idle(_goalTile);
        _goalTile = null;
    }

    #region UIUpdates
    /// <Summary>
    /// Cost of every move made by the player
    /// </Summary>
    private void UpdateDayCounter()
    {
        _daysPassed += _pathDuration;
        EventManager.OnUpdateUIElement(UIManager.Elements.Duration, _daysPassed.ToString());
    }

    /// <Summary>
    /// Cost of the selected move
    /// </Summary>
    private void UpdatePathCounter(string text)
    {
        EventManager.OnUpdateUIElement(UIManager.Elements.PathLength, text);
    }
    #endregion

    #region PathHighlighting
    private void ShowPath()
    {
        _pathDuration = 0;

        for (int index = 1; index < _pathList.Count; index++)
        {
            Tile tile = (Tile)_pathList[index];

            if (index < _pathList.Count - 1)
                tile.GetTileHighlight.TilePath();

            _pathDuration += tile.GetCost;
        }

        UpdatePathCounter(_pathDuration.ToString());
    }

    private void UnselectPath()
    {
        if (_pathList == null) return;

        for (int index = 1; index < _pathList.Count; index++)
        {
            Tile tile = (Tile)_pathList[index];
            tile.GetTileHighlight.ResetSelection();
        }

        _pathList.Clear();
    }
    #endregion

    private void OnDestroy()
    {
        EventManager.TileSelected -= TileSelected;
        EventManager.UILoaded -= OnUILoaded;
    }
}
