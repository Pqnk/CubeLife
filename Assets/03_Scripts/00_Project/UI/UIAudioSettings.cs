using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSettings : UISectionSettings
{
    public float MusicVolume { get; private set; }
    public float EffectVolume { get; private set; }
    public float UIVolume { get; private set; }

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


    public void OnSliderMusicVolumeValueChanged(float value)
    {
        MusicVolume = value;
        ProjectManager.Instance.audioManager.ChangeMusicVolume(MusicVolume);
    }

    public void OnSliderEffectVolumeValueChanged(float value)
    {
        EffectVolume = value;
    }

    public void OnSliderUIVolumeValueChanged(float value)
    {
        UIVolume = value;
        ProjectManager.Instance.audioManager.ChangeUISoundsVolume(UIVolume);
    }

    public override void ChargeDataValue()
    {
        MusicVolume = SaveSettingsManager.DataSaveSettings.musicVolume;
        EffectVolume = SaveSettingsManager.DataSaveSettings.effectVolume;
        UIVolume = SaveSettingsManager.DataSaveSettings.uiVolume;

        musicVolSlider.value = MusicVolume;
        effectVolSlider.value = EffectVolume;
        uiVolSlider.value = UIVolume;

        ProjectManager.Instance.audioManager.ChangeMusicVolume(MusicVolume);
        ProjectManager.Instance.audioManager.ChangeUISoundsVolume(UIVolume);

    }

    public override void InitializeBaseSectionSettingsValue()
    {
        Debug.Log($"Music slider : {musicVolSlider.value}, Effect slider : {effectVolSlider.value} UI Slider : {uiVolSlider.value}");

        MusicVolume = musicVolSlider.value;
        EffectVolume = effectVolSlider.value;
        UIVolume = uiVolSlider.value;

        Debug.Log($"MusicVolume : {MusicVolume}, EffectVolume : {EffectVolume}, UIVolume: {UIVolume}");

        ProjectManager.Instance.audioManager.ChangeMusicVolume(MusicVolume);
        ProjectManager.Instance.audioManager.ChangeUISoundsVolume(UIVolume);
    }
}
