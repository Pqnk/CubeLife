using UnityEngine;

public class CubeLyfeManager : MonoBehaviour
{
    #region ########## SINGLETON - CUBELYFE MANAGER ###########
    public static CubeLyfeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    [Space]
    [Header("##### LEVEL MANAGER #####")]
    [Header("Level Manager")]
    public LevelManager levelManager;

    [Space]
    [Header("##### AUDIO MANAGERS #####")]
    [Header("Audio Manager")]
    public AudioManager audioManager;


}
