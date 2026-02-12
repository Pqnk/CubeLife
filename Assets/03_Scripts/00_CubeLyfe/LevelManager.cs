using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CubeLyfeLevels
{
    L_00_MenuLevel,
    L_01_GameOfLifeLevel
}

public class LevelManager : MonoBehaviour
{
    public static event Action<float> OnLoadingScene;

    public void LoadLevel(CubeLyfeLevels levelToLoad)
    {
        SceneManager.LoadScene($"{levelToLoad}");
    }

    public IEnumerator LoadLevelAsync(CubeLyfeLevels levelToLoad)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync($"{levelToLoad}");
        op.allowSceneActivation = false;

        OnLoadingScene?.Invoke(0.9f);

        yield return new WaitForSeconds(4f);

        op.allowSceneActivation = true;
    }
}
