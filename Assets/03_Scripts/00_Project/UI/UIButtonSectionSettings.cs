using System;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonSectionSettings : MonoBehaviour
{
    protected static event Action OnButtonSettingsClick;

    [SerializeField] private CanvasGroup _panelSectionSettings;
    private Image _imageButtonSectionSettings;
    private Button _buttonSectionSettings;
    private UISectionSettings _uiSectionSettings;

    private void Awake()
    {
        _imageButtonSectionSettings = this.transform.GetChild(1).GetComponent<Image>();
        _buttonSectionSettings = GetComponent<Button>();
        _uiSectionSettings = _panelSectionSettings.transform.GetComponent<UISectionSettings>();

        DisableCurrentButtonSectionSettings();
    }

    private void OnEnable()
    {
        OnButtonSettingsClick += DisableCurrentButtonSectionSettings;
    }
    private void OnDisable()
    {
        OnButtonSettingsClick -= DisableCurrentButtonSectionSettings;
    }

    public void OnClickButtonSectionSettings()
    {
        OnButtonSettingsClick?.Invoke();
        _imageButtonSectionSettings.enabled = true;
        _panelSectionSettings.interactable = true;
        _panelSectionSettings.blocksRaycasts = true;
        _panelSectionSettings.alpha = 1f;
    }

    public void DisableCurrentButtonSectionSettings()
    {
        _imageButtonSectionSettings.enabled = false;
        _panelSectionSettings.interactable = false;
        _panelSectionSettings.blocksRaycasts = false;
        _panelSectionSettings.alpha = 0f;
    }
}
