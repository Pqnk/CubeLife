using System;
using System.IO;
using UnityEngine;

public static class SaveSettingsManager
{
    public static DataSaveSettings DataSaveSettings { get; private set; }

    public static bool DoesSaveFileAlreadyExists { get { return File.Exists(SaveDataPath); } }

    /// <summary>
    /// Property for the path of the save file.
    /// </summary>
    private static string SaveDataPath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "saveGridParameters.json");
        }
    }

    /// <summary>
    /// Saves the specified grid and audio settings parameters to persistent storage.
    /// </summary>
    /// <remarks>This method serializes the provided parameters and writes them to a file at the application's
    /// designated settings path, overwriting any existing settings. Call this method to persist user preferences or
    /// configuration changes.</remarks>
    /// <param name="gridSize">The size of the grid to be saved. Must be a positive integer.</param>
    /// <param name="endStep">The desired end step value for the grid. Must be a non-negative integer.</param>
    /// <param name="speed">The speed setting to be saved. Represents the playback or animation speed. Must be a non-negative integer.</param>
    /// <param name="musicVolume">The music volume level to be saved. Typically a value between 0.0 (muted) and 1.0 (maximum volume).</param>
    /// <param name="effectVolume">The effect volume level to be saved. Must be a non-negative integer, with the valid range depending on the
    /// application's audio system.</param>
    /// <param name="uiVolume">The user interface volume level to be saved. Must be a non-negative integer, with the valid range depending on
    /// the application's audio system.</param>
    public static void SaveSettingsDatas(int gridSize, int endStep, int speed, float musicVolume, float effectVolume, float uiVolume)
    {
        DataSaveSettings data = new DataSaveSettings
        {
            gridSize = gridSize,
            desiredEndStep = endStep,
            speed = speed,
            musicVolume = musicVolume,
            effectVolume = effectVolume,
            uiVolume = uiVolume
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveDataPath, json);
    }

    public static void ChargeDataSaveSettingsFile()
    {
        DataSaveSettings data = null;

        if (DoesSaveFileAlreadyExists)
        {
            string json = File.ReadAllText(SaveDataPath);
            data = JsonUtility.FromJson<DataSaveSettings>(json);
        }

        DataSaveSettings = data;
    }
}
