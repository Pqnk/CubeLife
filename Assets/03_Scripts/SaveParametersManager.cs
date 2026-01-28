using System.IO;
using UnityEngine;

public static class SaveParametersManager
{
    /// <summary>
    /// Property for the path of the save file
    /// </summary>
    private static string SaveDataPath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "saveGridParameters.json");
        }
    }

    /// <summary>
    /// Methode to create file and save parameters of the Grid
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

    /// <summary>
    /// Method to get the datas from the save file - class SaveDataGridParameters
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
    /// Methode to get if a Savefie already exists
    /// </summary>
    /// <returns>bool</returns>
    public static bool DoesSaveFileAlreadyExists()
    {
        return File.Exists(SaveDataPath);
    }
}
