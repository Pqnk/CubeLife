
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum GameState
{
    Running,
    Paused,
    Stopped
}

public sealed class GameManager : MonoBehaviour
{
    #region ----SINGLETON INSTANCE GAMEMANAGER----
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    #region ----INSPECTOR PRIVATE VARIABLES----
    [Header("Cell Prefab")]
    [SerializeField] private GameObject _cellPrefab;
    #endregion

    #region ----GAMEMANAGER PROPERTIES and VARIABLES----
    private Coroutine _gameCoroutine = null;
    public  int GridSize { get; private set; } = 10;
    public int GameSpeed { get; private set; } = 1;
    public int NumberStep { get; private set; } = 0;
    public bool GameStarted { get { return GameState == GameState.Running || GameState == GameState.Paused; } }
    public GameState GameState { get; private set; } = GameState.Stopped;
    public int DesiredEndStep { get; private set; } = 0;
    public bool DesiredEndStepReached { get { return (DesiredEndStep > 0 && NumberStep >= DesiredEndStep); } }
    #endregion

    #region ----GAME RUNNING COROUTINE----
    private IEnumerator StartStepCounting()
    {
        while (GameState == GameState.Running && !DesiredEndStepReached)
        {
            yield return new WaitForSeconds((1f / GameSpeed) * 0.5f);
            NumberStep++;

            if (DesiredEndStepReached)
            {
                StopGame();
            }

            UIManager.Instance.UpdateGameUI();
        }
    }
    #endregion

    #region ----GAME CONTROL METHODS----
    public void ToggleStartPauseGame()
    {
        switch (GameState)
        {
            case GameState.Running:
                PauseGame();
                break;

            case GameState.Paused:
                ResumeGame();
                break;

            case GameState.Stopped:

                if (!DesiredEndStepReached)
                {
                    StartGame();
                }
                break;
        }
    }

    public void StartGame()
    {
        if (_gameCoroutine == null && !GameStarted)
        {
            GameState = GameState.Running;
            _gameCoroutine = StartCoroutine(StartStepCounting());
        }
    }

    public void PauseGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Paused;
            StopCoroutine(_gameCoroutine);
        }
    }

    public void ResumeGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Running;
            _gameCoroutine = StartCoroutine(StartStepCounting());
        }
    }

    public void StopGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Stopped;
            StopCoroutine(_gameCoroutine);
            _gameCoroutine = null;
        }
    }

    public void ResetGame()
    {
        if (_gameCoroutine != null)
        {
            StopCoroutine(_gameCoroutine);
            _gameCoroutine = null;
        }
        GameState = GameState.Stopped;
        NumberStep = 0;
        GameSpeed = 0;
        GridManager.Instance.ResetGridToAllDead();
    }

    #endregion

    private void InitialiseGridManager()
    {
        GameObject gridManagerContainer = new GameObject("GridManager");
        gridManagerContainer.transform.position = Vector3.zero;
        gridManagerContainer.transform.SetParent(this.gameObject.transform.parent, false);
        gridManagerContainer.AddComponent<GridManager>();
        GridManager.Instance.InitializeGridManager(_cellPrefab, GridSize);
    }

    public void SaveGridParametersValueAndBegin(int gridSize, int desiredEndStep)
    {
        GridSize = gridSize;
        DesiredEndStep = desiredEndStep;

        SaveParametersManager.SaveGridParameters();

        InitialiseGridManager();

        UIManager.Instance.UpdateParametersInfos();
    }

    public void ChargeGridParameterValuesAndBegin()
    {
        GridSize = SaveParametersManager.ChargeSavedGridParametersFile().gridSize;
        DesiredEndStep = SaveParametersManager.ChargeSavedGridParametersFile().desiredEndStep;

        InitialiseGridManager();

        UIManager.Instance.UpdateParametersInfos();
    }

    public void UpdateSpeedGame(int newGameSpeed)
    {
        GameSpeed = newGameSpeed;
    }

}
