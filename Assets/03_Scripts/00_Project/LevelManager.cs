using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LevelStructure
{
    public LevelsInGame level;
    public string nameLevel;
}

public enum LevelsInGame
{
    MaimMenu,
    Level01,
    Level02,
    LoadingScreen
}

public class LevelManager : MonoBehaviour
{
    public static event Action<float> OnLoadingScene;

    [SerializeField] private List<LevelStructure> entries;

    public void LoadLevel(LevelsInGame levelToLoad)
    {
        SceneManager.LoadScene($"{levelToLoad}");
    }

    public IEnumerator LoadLevelAsync(LevelsInGame levelToLoad)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync($"{levelToLoad}");
        op.allowSceneActivation = false;

        OnLoadingScene?.Invoke(0.9f);

        yield return new WaitForSeconds(4f);

        op.allowSceneActivation = true;
    }
}
