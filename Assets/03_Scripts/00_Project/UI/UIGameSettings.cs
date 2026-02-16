using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSettings : UISettings
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

    public override void InitializeAllValueText()
    {
        gridSizeValueTxt.text = gridSizeSlider.value.ToString();
        endStepValueTxt.text = endStepSlider.value.ToString();
        speedValueTxt.text = speedSlider.value.ToString();

        GridSize = (int)gridSizeSlider.value;
        EndStep = (int)endStepSlider.value;
        Speed = (int)speedSlider.value;

        Debug.Log($"Data : {data.musicVolume}");

        //if(!SaveParametersManager.DoesSaveFileAlreadyExists())
        //{
        //    gridSizeValueTxt.text = gridSizeSlider.value.ToString();
        //    endStepValueTxt.text = endStepSlider.value.ToString();
        //    speedValueTxt.text = speedSlider.value.ToString();

        //    gridSize = (int)gridSizeSlider.value;
        //    endStep = (int)endStepSlider.value;
        //    speed = (int)speedSlider.value;
        //}
        //else
        //{
        //    SaveDataGridParameters data = SaveParametersManager.ChargeSavedParametersFile();

        //    gridSizeSlider.value = data.gridSize;
        //    endStepSlider.value = data.desiredEndStep;
        //    speedSlider.value = data.speed;

        //    gridSizeValueTxt.text = gridSizeSlider.value.ToString();
        //    endStepValueTxt.text = endStepSlider.value.ToString();
        //    speedValueTxt.text = speedSlider.value.ToString();

        //    gridSize = (int)gridSizeSlider.value;
        //    endStep = (int)endStepSlider.value;
        //    speed = (int)speedSlider.value;
        //}

    }
}
