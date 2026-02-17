using UnityEngine;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField] private UIGameSettings _gameSettings;
    [SerializeField] private UIAudioSettings _audioSettings;

    public void SaveSettings()
    {
        if (_gameSettings != null && _audioSettings != null)
        {
            SaveParametersManager.SaveSettingsParameters(
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
