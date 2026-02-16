using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSettings : MonoBehaviour, IUISettings
{
    public int gridSize { get; private set; }
    public int endStep { get; private set; }
    public int speed { get; private set; }

    [Header("---- GRID SIZE ----")]
    [SerializeField] private Slider gridSizeSlider;
    [SerializeField] private TMP_Text gridSizeValueTxt;

    [Space]
    [Header("---- END STEP ----")]
    [SerializeField] private Slider endStepSlider;
    [SerializeField] private TMP_Text endStepValueTxt;

    [Space]
    [Header("---- SPEED ----")]
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_Text speedValueTxt;


    public void OnSliderGridSizeValueChanged(float truc)
    {
        gridSizeValueTxt.text = $"{truc}";
        gridSize = (int)truc;
    }

    public void OnSliderEndStepValueChanged(float truc)
    {
        endStepValueTxt.text = $"{truc}";
        endStep = (int)truc;
    }

    public void OnSliderSpeedValueChanged(float truc)
    {
        speedValueTxt.text = $"{truc}";
        speed = (int)truc;
    }

    public void InitializeAllValueText()
    {
        gridSizeValueTxt.text = gridSizeSlider.value.ToString();
        endStepValueTxt.text = endStepSlider.value.ToString();
        speedValueTxt.text = speedSlider.value.ToString();

        gridSize = (int)gridSizeSlider.value;
        endStep = (int)endStepSlider.value;
        speed = (int)speedSlider.value;
    }
}
