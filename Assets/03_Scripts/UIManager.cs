using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    #region ----SINGLETON INSTANCE UIMANAGER----
    public static UIManager Instance { get; private set; }

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

    #region ----CANVAS----
    [Header("Canvas")]
    [SerializeField] private Canvas gameDisplayCanvas;
    [SerializeField] private Canvas gridParameterCanvas;
    #endregion

    #region ----CANVAS GAME DISPLAY - UI ELEMENTS----
    [Header("GAME DISPLAY UI: Texts Infos")]
    [SerializeField] private TextMeshProUGUI numberStepUITEXT;
    [SerializeField] private TextMeshProUGUI startPauseButtonTEXT;
    [SerializeField] private TextMeshProUGUI gridSizeInfo;
    [SerializeField] private TextMeshProUGUI DesiredEndStepInfo;
    [SerializeField] private Slider gameSpeedSlider;
    #endregion

    #region ----CANVAS GRID PARAMETER - UI ELEMENTS----

    [Header("GRID PARAMETER UI : Sliders & Values")]
    [SerializeField] private Slider gridSizeSlider;
    [SerializeField] private Slider desiredEndStepSlider;
    [SerializeField] private TextMeshProUGUI gridSizeSliderValue;
    [SerializeField] private TextMeshProUGUI desiredEndStepSliderValue;
    #endregion

    #region ----CANVAS GRID PARAMETER - LITHENERS----
    private void UpdateGridSizeSliderTextValue(float value)
    {
        gridSizeSliderValue.text = value.ToString();
    }

    private void UpdateDesiredEndStepSliderTextValue(float value)
    {
        desiredEndStepSliderValue.text = value.ToString();
    }
    #endregion

    #region ----CANVAS GRID PARAMETER - ON CLICK ON BUTTONS----
    public void OnClickOnSaveAndBeginButton()
    {
        int gridSize = (int)gridSizeSlider.value;
        int desiredEndStep = (int)desiredEndStepSlider.value;
        gameDisplayCanvas.gameObject.SetActive(true);
        gridParameterCanvas.gameObject.SetActive(false);

        GameManager.Instance.SaveGridParametersValueAndBegin(gridSize, desiredEndStep);
    }

    #endregion

    #region ----CANVAS GAME DISPLAY - ON CLICK ON BUTTONS----
    public void OnClickStartPauseButton()
    {
        GameManager.Instance.ToggleStartPauseGame();
        UpdateTextButtonStartStop();
    }

    public void OnClickResetGameButton()
    {
        GameManager.Instance.ResetGame();
        UpdateTextButtonStartStop();
        UpdateNumberStepUI();
    }

    public void OnClickOnChangeParameters()
    {
        gameDisplayCanvas.gameObject.SetActive(false);
        gridParameterCanvas.gameObject.SetActive(true);
        GameManager.Instance.ResetGame();
        GridManager.Instance.DeleteGrid();
    }
    #endregion

    #region ----CANVAS GAME DISPLAY - UPDATE UI----

    public void UpdateGameUI()
    {
        UpdateNumberStepUI();
        UpdateTextButtonStartStop();
    }

    private void UpdateTextButtonStartStop()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameState.Running:
                startPauseButtonTEXT.text = "PAUSE";
                break;

            case GameState.Paused:
                startPauseButtonTEXT.text = "RESUME";
                break;

            case GameState.Stopped:
                startPauseButtonTEXT.text = GameManager.Instance.DesiredEndStepReached ? "REACHED" : "START";
                break;

        }
    }

    private void UpdateNumberStepUI()
    {
        numberStepUITEXT.text = GameManager.Instance.NumberStep.ToString();
    }

    public void UpdateParametersInfos()
    {
        gridSizeInfo.text = SaveParametersManager.ChargeSavedGridParametersFile().gridSize.ToString();
        DesiredEndStepInfo.text = SaveParametersManager.ChargeSavedGridParametersFile().desiredEndStep.ToString();
    }

    #endregion

    #region ----CANVAS GAME DISPLAY - LITHENERS----
    public void UpdateSpeedGame(float value)
    {
        GameManager.Instance.UpdateSpeedGame((int)value);
    }
    #endregion

    private void Start()
    {
        gridSizeSlider.onValueChanged.AddListener(UpdateGridSizeSliderTextValue);
        desiredEndStepSlider.onValueChanged.AddListener(UpdateDesiredEndStepSliderTextValue);
        gridSizeSliderValue.text = gridSizeSlider.value.ToString();
        desiredEndStepSliderValue.text = desiredEndStepSlider.value.ToString();

        gameSpeedSlider.onValueChanged.AddListener(UpdateSpeedGame);

        if (SaveParametersManager.DoesSaveFileAlreadyExists())
        {
            gameDisplayCanvas.gameObject.SetActive(true);
            gridParameterCanvas.gameObject.SetActive(false);

            GameManager.Instance.ChargeGridParameterValuesAndBegin();
        }
        else
        {
            gameDisplayCanvas.gameObject.SetActive(false);
            gridParameterCanvas.gameObject.SetActive(true);
        }
    }
}
