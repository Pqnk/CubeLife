using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSettings : UISettings
{
    public float musicVolume { get; private set; }
    public float effectVolume { get; private set; }
    public float uiVolume { get; private set; }

    [Header("---- MUSIC VOLUME ----")]
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private TMP_Text musicVolValueTxt;

    [Space]
    [Header("---- EFFECTS VOLUME ----")]
    [SerializeField] private Slider effectVolSlider;
    [SerializeField] private TMP_Text effectVolValueTxt;

    [Space]
    [Header("---- UI VOLUME ----")]
    [SerializeField] private Slider uiVolSlider;
    [SerializeField] private TMP_Text uiVolValueTxt;

    public void OnSliderMusicVolumeValueChanged(float truc)
    {
        musicVolValueTxt.text = $"{truc}";
        musicVolume = truc;

        ProjectManager.Instance.audioManager.ChangeMusicVolume(musicVolume);
    }

    public void OnSliderEffectVolumeValueChanged(float truc)
    {
        effectVolValueTxt.text = $"{truc}";
        effectVolume = truc;
    }

    public void OnSliderUIVolumeValueChanged(float truc)
    {
        uiVolValueTxt.text = $"{truc}";
        uiVolume = truc;

        ProjectManager.Instance.audioManager.ChangeUISoundsVolume(truc);
    }



    public override void InitializeAllValueText()
    {

        musicVolValueTxt.text = musicVolSlider.value.ToString();
        effectVolValueTxt.text = effectVolSlider.value.ToString();
        uiVolValueTxt.text = uiVolSlider.value.ToString();


        musicVolume = musicVolSlider.value;
        effectVolume = effectVolSlider.value;
        uiVolume = uiVolSlider.value;

        float musicvol = ProjectManager.Instance.audioManager.BackgroundVolume;

        //musicVolSlider.value = musicvol;
        //musicVolValueTxt.text = musicVolSlider.value.ToString();
        //effectVolValueTxt.text = effectVolSlider.value.ToString();
        //uiVolValueTxt.text = uiVolSlider.value.ToString();


        //musicVolume = (int)musicVolSlider.value;
        //effectVolume = (int)effectVolSlider.value;
        //uiVolume = (int)uiVolSlider.value;


        //if (!SaveParametersManager.DoesSaveFileAlreadyExists())
        //{
        //    float musicvol = ProjectManager.Instance.audioManager.BackgroundVolume;

        //    musicVolSlider.value = musicvol;
        //    musicVolValueTxt.text = musicVolSlider.value.ToString();
        //    effectVolValueTxt.text = effectVolSlider.value.ToString();
        //    uiVolValueTxt.text = uiVolSlider.value.ToString();


        //    musicVolume = (int)musicVolSlider.value;
        //    effectVolume = (int)effectVolSlider.value;
        //    uiVolume = (int)uiVolSlider.value;
        //}
        //else
        //{
        //    SaveDataGridParameters data = SaveParametersManager.ChargeSavedParametersFile();

        //    musicVolSlider.value = data.musicVolume;
        //    effectVolSlider.value = data.effectVolume;
        //    uiVolSlider.value = data.uiVolume;

        //    musicVolValueTxt.text = musicVolSlider.value.ToString();
        //    effectVolValueTxt.text = effectVolSlider.value.ToString();
        //    uiVolValueTxt.text = uiVolSlider.value.ToString();


        //    musicVolume = (int)musicVolSlider.value;
        //    effectVolume = (int)effectVolSlider.value;
        //    uiVolume = (int)uiVolSlider.value;
        //}
    }
}
