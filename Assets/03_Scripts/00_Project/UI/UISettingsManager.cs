using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField] private List<UISectionSettings> _sectionSettings;

    private void Start()
    {
        if (_sectionSettings.Count > 0)
        {
            _sectionSettings.First().transform.parent.GetComponent<UIButtonSectionSettings>().OnClickButtonSectionSettings();
        }
    }


    public void SaveSettings()
    {
        if (_sectionSettings.Count > 0)
        {
            UIAudioSettings _audioSettings = _sectionSettings.OfType<UIAudioSettings>().FirstOrDefault();
            UIGameSettings _gameSettings = _sectionSettings.OfType<UIGameSettings>().FirstOrDefault();

            SaveSettingsManager.SaveSettingsDatas(
                        _gameSettings.GridSize,
                        _gameSettings.EndStep,
                        _gameSettings.Speed,
                        _audioSettings.MusicVolume,
                        _audioSettings.EffectVolume,
                        _audioSettings.UIVolume
                        );
        }
    }
}
