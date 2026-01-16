using System.IO;
using UnityEngine;

public static class SaveParametersManager
{
    private static string SaveDataPath { get { return Path.Combine(Application.persistentDataPath, "saveGridParameters.json");  } }

    public static void SaveGridParameters()
    {
        SaveData data = new SaveData
        {
            gridSize = GameManager.Instance.GridSize,
            desiredEndStep = GameManager.Instance.DesiredEndStep
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveDataPath, json);
    }

    public static SaveData ChargeSavedGridParametersFile()
    {
        SaveData data = null;

        if (File.Exists(SaveDataPath))
        {
            string json = File.ReadAllText(SaveDataPath);
            data = JsonUtility.FromJson<SaveData>(json);
        }

        return data;
    }

    public static bool DoesSaveFileAlreadyExists()
    {
        return File.Exists(SaveDataPath);
    }
}
