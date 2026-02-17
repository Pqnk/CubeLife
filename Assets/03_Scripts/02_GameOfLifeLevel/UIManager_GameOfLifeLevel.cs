using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager_GameOfLifeLevel : MonoBehaviour
{
    #region /////////////// SINGLETON INSTANCE UIMANAGER \\\\\\\\\\\\\\\\\
    public static UIManager_GameOfLifeLevel Instance { get; private set; }
    #endregion


    #region /////////////////// INSPECTOR VARIABLES \\\\\\\\\\\\\\\\\\\\
    [Header("----- CANVAS ------")]
    [SerializeField] private Canvas _gameDisplayCanvas;

    [Space]
    [Header("----- UI GAME DISPLAY -----")]
    [Header("-- UI Texts Infos --")]
    [SerializeField] private TextMeshProUGUI numberStepUITEXT;
    [SerializeField] private TextMeshProUGUI startPauseButtonTEXT;
    [SerializeField] private TextMeshProUGUI gridSizeInfo;
    [SerializeField] private TextMeshProUGUI DesiredEndStepInfo;
    [Header("-- Game Spleed Slidder --")]
    [SerializeField] private Slider gameSpeedSlider;
    #endregion


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }


    #region ########## METHODS ##########
    public void OnClickStartPauseButton()
    {
        Manager_GameOfLifeLevel.Instance.ToggleStartPauseGame();
        UpdateTextButtonStartStop();
    }

    public void UpdateGameUI()
    {
        UpdateNumberStepUI();
        UpdateTextButtonStartStop();
    }

    private void UpdateTextButtonStartStop()
    {
        switch (Manager_GameOfLifeLevel.Instance.GameState)
        {
            case GameState.Running:
                startPauseButtonTEXT.text = "PAUSE";
                break;

            case GameState.Paused:
                startPauseButtonTEXT.text = "RESUME";
                break;

            case GameState.Stopped:
                startPauseButtonTEXT.text = Manager_GameOfLifeLevel.Instance.DesiredEndStepReached ? "REACHED" : "START";
                break;

        }
    }

    private void UpdateNumberStepUI()
    {
        numberStepUITEXT.text = Manager_GameOfLifeLevel.Instance.NumberStep.ToString();
    }

    public void OnGridSizeValueChanged(float value)
    {
        int intValue = (int)value;
        GridManager.Instance.DeleteGrid();
        GridManager.Instance.InitializeGridManager(intValue);
    }
    #endregion


}
