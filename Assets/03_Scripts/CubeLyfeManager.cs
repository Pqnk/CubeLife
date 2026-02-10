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


    [Header("##### LEVEL MANAGERS #####")]
    [Header("Menu Level Manager")]
    public MenuLevelManager menuLevelManager;
    [Header("Game Of Life Level Manager")]
    public GameManager mainLevelManager;

    [Space]
    [Header("##### AUDIO MANAGERS #####")]
    [Header("Audio Manager")]
    [SerializeField] public AudioManager audioManager;

}
