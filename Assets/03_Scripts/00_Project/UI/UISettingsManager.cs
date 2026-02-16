using UnityEngine;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField] private UIGameSettings _gameSettings;
    [SerializeField] private UIAudioSettings _audioSettings;

    public void SaveSettings()
    {
        if (_gameSettings != null && _audioSettings != null)
        {
            SaveParametersManager.SaveGridParameters(
                _gameSettings.gridSize,
                _gameSettings.endStep,
                _gameSettings.speed,
                _audioSettings.musicVolume,
                _audioSettings.effectVolume,
                _audioSettings.uiVolume
                );
        }
    }
}
