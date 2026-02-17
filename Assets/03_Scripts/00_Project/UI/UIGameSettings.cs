using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSettings : UISectionSettings
{
    public int GridSize { get; private set; }
    public int EndStep { get; private set; }
    public int Speed { get; private set; }

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
        GridSize = (int)truc;
    }

    public void OnSliderEndStepValueChanged(float truc)
    {
        endStepValueTxt.text = $"{truc}";
        EndStep = (int)truc;
    }

    public void OnSliderSpeedValueChanged(float truc)
    {
        speedValueTxt.text = $"{truc}";
        Speed = (int)truc;
    }

    public override void ChargeDataValue()
    {
        GridSize = SaveParametersManager.SaveDataGridParameters.gridSize;
        EndStep = SaveParametersManager.SaveDataGridParameters.desiredEndStep;
        Speed = SaveParametersManager.SaveDataGridParameters.speed;

        gridSizeValueTxt.text = GridSize.ToString();
        endStepValueTxt.text = EndStep.ToString();
        speedValueTxt.text = Speed.ToString();

        gridSizeSlider.value = GridSize;
        endStepSlider.value = EndStep;
        speedSlider.value = Speed;

    }

    public override void InitializeBaseSectionSettingsValue()
    {
        GridSize = (int)gridSizeSlider.value;
        EndStep = (int)endStepSlider.value;
        Speed = (int)speedSlider.value;

        gridSizeValueTxt.text = GridSize.ToString();
        endStepValueTxt.text = EndStep.ToString();
        speedValueTxt.text = Speed.ToString();
    }
}
