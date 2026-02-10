using UnityEngine;
using UnityEngine.SceneManagement;

public enum CubeLyfeLevels
{
    L_00_MenuLevel,
    L_01_GameOfLifeLevel
}

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(CubeLyfeLevels levelToLoad)
    {
        SceneManager.LoadScene($"{levelToLoad}");
    }
}
