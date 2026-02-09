using UnityEngine;

public class MenuLevelManager : MonoBehaviour
{
    #region ----- SINGLETON -----
    public static MenuLevelManager Instance { get; private set; }

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

    [Header("InputManager")]
    [SerializeField] public InputManagerMenuLevel inputManagerMenuLevel;

    [Header("UI Manager")]
    [SerializeField] public UIManagerMenuLevel uiManagerMenuLevel;

    [Header("AudioManager")]
    [SerializeField] public AudioManager audioManager;

}
