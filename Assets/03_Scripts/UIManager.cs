using UnityEngine;
using TMPro;



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

    #region ----UI ELEMENTS----
    [Header("UI Steps Number")]
    [SerializeField] private TextMeshProUGUI numberStepUITEXT;

    [Header("UI Start/Pause Button")]
    [SerializeField] private TextMeshProUGUI startPauseButtonTEXT;
    #endregion

    #region ----OnClickButtons----
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
    #endregion

    #region ----UPDATE UI----

    public void UpdateUI()
    {
        UpdateTextButtonStartStop();
        UpdateNumberStepUI();
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

    #endregion
}
