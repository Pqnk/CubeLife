using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    #region ########## SINGLETON - CUBELYFE MANAGER ###########
    public static ProjectManager Instance { get; private set; }

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

    [Header("##### AUDIO MANAGER #####")]
    public AudioManager audioManager;

    [Space]
    [Header("##### LEVEL MANAGER #####")]
    public LevelManager levelManager;
}
