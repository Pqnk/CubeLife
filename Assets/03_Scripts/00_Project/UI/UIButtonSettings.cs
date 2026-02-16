using System;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonSettings : MonoBehaviour
{
    protected static event Action OnButtonSettingsClick;

    [SerializeField] private CanvasGroup _panelSettings;
    private Image _imageButtonSettings;
    private Button _buttonSettings;
    private IUISettings _uiSettings;

    private void Awake()
    {
        _imageButtonSettings = this.transform.GetChild(1).GetComponent<Image>();
        _buttonSettings = GetComponent<Button>();
        _uiSettings = _panelSettings.transform.GetComponent<IUISettings>();

        DisableCurrentSettings();
    }

    private void OnEnable()
    {
        OnButtonSettingsClick += DisableCurrentSettings;
    }
    private void OnDisable()
    {
        OnButtonSettingsClick -= DisableCurrentSettings;
    }

    public void OnClickButtonSettings()
    {
        OnButtonSettingsClick?.Invoke();
        _imageButtonSettings.enabled = true;
        _panelSettings.interactable = true;
        _panelSettings.blocksRaycasts = true;
        _panelSettings.alpha = 1f;

        if (_uiSettings != null)
            _uiSettings.InitializeAllValueText();
    }

    public void DisableCurrentSettings()
    {
        _imageButtonSettings.enabled = false;
        _panelSettings.interactable = false;
        _panelSettings.blocksRaycasts = false;
        _panelSettings.alpha = 0f;
    }
}
