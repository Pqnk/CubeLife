using UnityEngine;

public class MenuLevelManager : MonoBehaviour
{
    #region ########## SINGLETON - MENU LEVEL MANAGER ###########
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

    [Header("---- Menu Level - INPUT MANAGER ----")]
    public InputManagerMenuLevel inputManagerMenuLevel;

    [Header("---- Menu Level - UI MANAGER ----")]
    public UIManagerMenuLevel uiManagerMenuLevel;
}
