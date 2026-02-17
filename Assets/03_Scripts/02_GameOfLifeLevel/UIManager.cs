using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private CanvasGroup _canvasGridParametersPanel;
    [SerializeField] private CanvasGroup _canvasGameDisplay;
    [SerializeField] private RectTransform _rectTransfGridParametersPanel;
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
        ActivateCanvasGameDisplay(true);
        ActivateCanvasGridParameters(false);
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
        ActivateCanvasGameDisplay(false);
        ActivateCanvasGridParameters(true);
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
        //gridSizeInfo.text = SaveParametersManager.ChargeSavedParametersFile().gridSize.ToString();
        //DesiredEndStepInfo.text = SaveParametersManager.ChargeSavedParametersFile().desiredEndStep.ToString();
    }

    #endregion

    #region ----CANVAS GAME DISPLAY - LITHENERS----
    public void UpdateSpeedGame(float value)
    {
        GameManager.Instance.UpdateSpeedGame((int)value);
    }
    #endregion

    #region ----DOTween Methods----
    private void SlideOnXAxis_PanelGridParameters(float targetPos, float time)
    {
        _rectTransfGridParametersPanel.DOAnchorPosX(targetPos, time).SetEase(Ease.OutCubic);
    }
    private void FadeInOrOut_PanelGridParameters(bool isFadeIn)
    {
        _canvasGridParametersPanel.DOKill();

        if (isFadeIn)
        {
            _canvasGridParametersPanel.interactable = true;
            _canvasGridParametersPanel.blocksRaycasts = true;
            _canvasGridParametersPanel.DOFade(1f, 0.25f);
        }
        else
        {
            _canvasGridParametersPanel.DOFade(0f, 0.25f)
                          .OnComplete(() =>
                          {
                              _canvasGridParametersPanel.interactable = false;
                              _canvasGridParametersPanel.blocksRaycasts = false;
                          });
        }
    }
    private void FadeInOrOut_PanelGameDisplay(bool isFadeIn)
    {
        _canvasGameDisplay.DOKill();

        if (isFadeIn)
        {
            _canvasGameDisplay.interactable = true;
            _canvasGameDisplay.blocksRaycasts = true;
            _canvasGameDisplay.DOFade(1f, 0.25f);
        }
        else
        {
            _canvasGameDisplay.DOFade(0f, 0.25f)
                          .OnComplete(() =>
                          {
                              _canvasGameDisplay.interactable = false;
                              _canvasGameDisplay.blocksRaycasts = false;
                          });
        }
    }
    #endregion

    /// <summary>
    /// Method referenced in the inspector of "----UI---- > Canvas-GridParameter>Panel > Slider-GridParameter-GridSize", in the "On Value Changed" field. 'Dynamic float'.
    /// Gets called everytime user change the 'Grid Size' slider value.
    /// </summary>
    /// <param name="value"> Float value of the Slider </param>
    public void OnGridSizeValueChanged(float value)
    {
        int intValue = (int)value;
        GridManager.Instance.DeleteGrid();
        GridManager.Instance.InitializeGridManager(intValue);
    }
    public void ActivateCanvasGridParameters(bool activate)
    {
        if (activate)
        {
            SlideOnXAxis_PanelGridParameters(0f, 0.3f);
            FadeInOrOut_PanelGridParameters(true);
        }
        else
        {
            SlideOnXAxis_PanelGridParameters(-700f, 0.3f);
            FadeInOrOut_PanelGridParameters(false);
        }

    }
    public void ActivateCanvasGameDisplay(bool activate)
    {
        if (activate)
            FadeInOrOut_PanelGameDisplay(true);
        else
            FadeInOrOut_PanelGameDisplay(false);
    }

    private void Start()
    {
        gridSizeSlider.onValueChanged.AddListener(UpdateGridSizeSliderTextValue);
        desiredEndStepSlider.onValueChanged.AddListener(UpdateDesiredEndStepSliderTextValue);
        gridSizeSliderValue.text = gridSizeSlider.value.ToString();
        desiredEndStepSliderValue.text = desiredEndStepSlider.value.ToString();
        gameSpeedSlider.onValueChanged.AddListener(UpdateSpeedGame);

        if (SaveParametersManager.DoesSaveFileAlreadyExists)
        {
            ActivateCanvasGameDisplay(true);
            ActivateCanvasGridParameters(false);

            GameManager.Instance.ChargeGridParameterValuesAndBegin();
        }
        else
        {
            ActivateCanvasGameDisplay(false);
            ActivateCanvasGridParameters(true);
        }
    }

}
