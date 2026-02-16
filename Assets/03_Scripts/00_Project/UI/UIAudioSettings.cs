using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSettings : MonoBehaviour, IUISettings
{
    public float musicVolume { get; private set; }
    public int effectVolume { get; private set; }
    public int uiVolume { get; private set; }

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
        effectVolume = (int)truc;
    }

    public void OnSliderUIVolumeValueChanged(float truc)
    {
        uiVolValueTxt.text = $"{truc}";
        uiVolume = (int)truc;
    }



    public void InitializeAllValueText()
    {
        float musicvol = ProjectManager.Instance.audioManager.BackgroundVolume;

        musicVolSlider.value = musicvol;
        musicVolValueTxt.text = musicVolSlider.value.ToString();
        effectVolValueTxt.text = effectVolSlider.value.ToString();
        uiVolValueTxt.text = uiVolSlider.value.ToString();


        musicVolume = (int)musicVolSlider.value;
        effectVolume = (int)effectVolSlider.value;
        uiVolume = (int)uiVolSlider.value;
    }
}
