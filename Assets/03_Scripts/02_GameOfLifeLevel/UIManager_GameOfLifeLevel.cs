using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager_GameOfLifeLevel : MonoBehaviour
{
    #region /////////////// SINGLETON INSTANCE UIMANAGER \\\\\\\\\\\\\\\\\
    private static UIManager_GameOfLifeLevel Instance { get; set; }
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


    #region /////////////////// EVENTS \\\\\\\\\\\\\\\\\\\\

    public static event Action OnClickUIStartPauseButton;

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
        OnClickUIStartPauseButton?.Invoke();
    }

    public void UpdateGameUI(GameState gameState, int numberStep, bool endStepReached)
    {
        switch (gameState)
        {
            case GameState.Running:
                startPauseButtonTEXT.text = "PAUSE";
                break;

            case GameState.Paused:
                startPauseButtonTEXT.text = "RESUME";
                break;

            case GameState.Stopped:
                startPauseButtonTEXT.text = endStepReached ? "REACHED" : "START";
                break;
        }

        numberStepUITEXT.text = numberStep.ToString();
    }

    #endregion


}
