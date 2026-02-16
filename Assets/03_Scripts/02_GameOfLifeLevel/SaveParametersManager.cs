using System.IO;
using UnityEngine;

public static class SaveParametersManager
{
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
    /// Method to create a file and save parameters of the Grid in it.
    /// </summary>
    public static void SaveGridParameters()
    {
        SaveDataGridParameters data = new SaveDataGridParameters
        {
            gridSize = GameManager.Instance.GridSize,
            desiredEndStep = GameManager.Instance.DesiredEndStep
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveDataPath, json);
    }

    public static void SaveGridParameters(int gridSize, int endStep, int speed, float musicVolume, int effectVolume, int uiVolume)
    {
        SaveDataGridParameters data = new SaveDataGridParameters
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

    /// <summary>
    /// Method to get the datas from the save file.
    /// Datas are saved from the class SaveDataGridParameters.
    /// </summary>
    /// <returns>SaveDataGridParameters</returns>
    public static SaveDataGridParameters ChargeSavedGridParametersFile()
    {
        SaveDataGridParameters data = null;

        if (File.Exists(SaveDataPath))
        {
            string json = File.ReadAllText(SaveDataPath);
            data = JsonUtility.FromJson<SaveDataGridParameters>(json);
        }

        return data;
    }

    /// <summary>
    /// Method to get if a save file already exists.
    /// </summary>
    /// <returns>bool</returns>
    public static bool DoesSaveFileAlreadyExists()
    {
        return File.Exists(SaveDataPath);
    }
}
