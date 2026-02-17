using UnityEngine;

public sealed class ProjectManager : MonoBehaviour
{
    #region ########## SINGLETON - PROJECT MANAGER ###########
    public static ProjectManager Instance { get; private set; }
    #endregion

    [Header("##### AUDIO MANAGER #####")]
    public AudioManager audioManager;

    [Space]
    [Header("##### LEVEL MANAGER #####")]
    public LevelManager levelManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        SaveSettingsManager.ChargeDataSaveSettingsFile();
    }

}
