using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManagerMenuLevel : MonoBehaviour
{
    #region //////////////////// VARIABLES \\\\\\\\\\\\\\\\\\\\\\

    [Header("Background Gradient Materiel")]
    [SerializeField] private Material _gradientMaterial;
    [SerializeField] private Color _blueColor = new Color(0.17f, 0.42f, 1.0f);
    [SerializeField] private Color _magentaColor = new Color(0.7f, 0.3f, 1.0f);
    [SerializeField] private float _colorChangingSpeed = 0.03f;

    [Space]
    [Header("Background -- Power Range & Speed")]
    [SerializeField] private float _minPower = 0.1f;
    [SerializeField] private float _maxPower = 0.4f;
    [SerializeField] private float _speed = 0.5f;

    [Space]
    [Header("Text -- Press Button")]
    [SerializeField] private float _speedAlphaText = 0.75f;
    [SerializeField] private TMP_Text _pressAnyKeyText;

    [Space]
    [Header("Logo")]
    [SerializeField] private RectTransform _logoRectT;
    [SerializeField] private float _speedScaleLogo = 0.9f;
    [SerializeField] private float _minScaleLogo = 0.90f;
    [SerializeField] private float _maxScaleLogo = 1.1f;
    private Image _logoImage;

    [Space]
    [Header("Panels")]
    [SerializeField] private Button[] _buttonsMenuArray;
    [SerializeField] private List<TMP_Text> _buttonsMenuTextsList;

    #endregion

    private void Awake()
    {
        //  Saving the Image component from the UI object containing the Logo
        _logoImage = _logoRectT.GetComponent<Image>();

        //  Initializing buttons appearance - Invisible and not able to interact
        foreach (Button button in _buttonsMenuArray)
        {
            //Debug.Log($"Mon bouton : {button.transform.name}" );

            Image buttonImage = button.GetComponent<Image>();
            TMP_Text buttonText = button.transform.GetChild(0).GetComponent<TMP_Text>();
            Color alphaZeroWhite = new Color(1, 1, 1, 0); // White transparent
            Color alphaZeroPurple = buttonText.color;
            alphaZeroPurple.a = 0;

            // Cannot Interact with buttons and buttons invisibles (Image and Text)
            button.interactable = false;
            buttonImage.raycastTarget = false;
            buttonImage.color = alphaZeroWhite;
            buttonText.color = alphaZeroPurple;

            _buttonsMenuTextsList.Add(buttonText);
        }
    }

    private void Start()
    {
        StartCoroutine(PingPongAlphaTextPressButton());
        StartCoroutine(PingPongGradientBackground());
        StartCoroutine(PingPongScaleLogo());
        StartCoroutine(Appearing_Logo());
    }


    #region ########## EVENT - ANY KEY PRESSED ###########
    void OnEnable()
    {
        InputManagerMenuLevel.OnAnyInput += ChangeUIDisplayWhenAnyKeyPressed;
    }
    void OnDisable()
    {
        InputManagerMenuLevel.OnAnyInput -= ChangeUIDisplayWhenAnyKeyPressed;
    }
    void ChangeUIDisplayWhenAnyKeyPressed()
    {
        StartCoroutine(FadeOutTextPressAnyKey());
        StartCoroutine(PlaceLogoAtTheTop());
        StartCoroutine(Appearing_ButtonsMenu());
    }
    #endregion


    #region ########## BACKGROUND ##########
    private IEnumerator PingPongGradientBackground()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * _speed, 1f);
            float power = Mathf.Lerp(_minPower, _maxPower, t);
            _gradientMaterial.SetFloat("_Power", power);

            float colorT = Mathf.PingPong(Time.time * _colorChangingSpeed, 1f);
            Color newColor = Color.Lerp(_blueColor, _magentaColor, colorT);
            _gradientMaterial.SetColor("_BottomColor", newColor);
            yield return null;
        }
    }
    #endregion


    #region ########## PRESS ANY KEY TEXT ###########
    private IEnumerator PingPongAlphaTextPressButton()
    {
        while (!MenuLevelManager.Instance.inputManagerMenuLevel.hasAnyInputBeenPushed)
        {
            float alpha = Mathf.PingPong(Time.time * _speedAlphaText, 1f);
            _pressAnyKeyText.alpha = alpha;
            yield return null;
        }
    }
    private IEnumerator FadeOutTextPressAnyKey()
    {
        float t = 0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime;
            _pressAnyKeyText.alpha = Mathf.Lerp(1f, 0f, t / 1.0f);

            float y = Mathf.Lerp(215f, 100f, t / 1.0f);
            _pressAnyKeyText.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, y);

            yield return null;
        }

        _pressAnyKeyText.alpha = 0f;
    }
    #endregion


    #region ########## LOGO ##########
    private IEnumerator PingPongScaleLogo()
    {
        while (!MenuLevelManager.Instance.inputManagerMenuLevel.hasAnyInputBeenPushed)
        {
            float alpha = Mathf.PingPong(Time.time * _speedScaleLogo, 1f);
            float scaleValue = Mathf.Lerp(_minScaleLogo, _maxScaleLogo, alpha);
            Vector3 scale = new Vector3(scaleValue, scaleValue, scaleValue);
            _logoRectT.localScale = scale;
            yield return null;
        }

    }
    private IEnumerator PlaceLogoAtTheTop()
    {
        float t = 0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime;

            float scaleValue = Mathf.Lerp(1.0f, 0.6f, t / 1.0f);
            Vector3 newScale = new Vector3(scaleValue, scaleValue, scaleValue);
            _logoRectT.localScale = newScale;

            float y = Mathf.Lerp(78.0f, 330f, t / 1.0f);
            _logoRectT.anchoredPosition = new Vector2(0f, y);

            yield return null;
        }
    }
    private IEnumerator Appearing_Logo()
    {
        float t = 0f;
        Color color = Color.white;
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0.0f, 1.0f, t / 1.0f);
            _logoImage.color = color;
            yield return null;
        }
    }
    #endregion


    #region ######### BUTTONS MENU ##########
    public void OnClickButton_Play()
    {
        Debug.Log("Play !");
        EventSystem.current.SetSelectedGameObject(null);
        ToggleInteractableButtons(false);
    }
    public void OnClickButton_Settings()
    {
        Debug.Log("Settings !");
        EventSystem.current.SetSelectedGameObject(null);
        ToggleInteractableButtons(false);
    }
    public void OnClickButton_Quit()
    {
        Debug.Log("Quit !");
        EventSystem.current.SetSelectedGameObject(null);
        ToggleInteractableButtons(false);
        Application.Quit();
    }
    private IEnumerator Appearing_ButtonsMenu()
    {
        int i = 0;
        foreach (Button button in _buttonsMenuArray)
        {
            Image buttonImage = button.GetComponent<Image>();
            TMP_Text buttonText = _buttonsMenuTextsList[i];

            ToggleInteractableButtons(true, 1f);
            buttonImage.raycastTarget = true;

            StartCoroutine(FadeIn_ButtonsMenu(buttonImage, buttonText));

            i++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator FadeIn_ButtonsMenu(Image buttonImage, TMP_Text buttonText)
    {
        float t = 0f;
        float duration = 1f;

        Color buttonImageOldColor = buttonImage.color;
        Color buttonTextOldColor = buttonText.color;

        while (t < duration)
        {
            t += Time.deltaTime;

            float a = Mathf.Lerp(0f, 1f, t / duration);

            buttonImageOldColor.a = a;
            buttonTextOldColor.a = a;

            buttonImage.color = buttonImageOldColor;
            buttonText.color = buttonTextOldColor;

            yield return null;

        }


    }
    private void ToggleInteractableButtons(bool activation)
    {
        foreach (Button button in _buttonsMenuArray)
        {
            button.interactable = activation;
        }
    }
    private void ToggleInteractableButtons(bool activation, float delay = 0f)
    {
        StartCoroutine(ToggleInteactableButtonDelayed(activation, delay));
    }
    private IEnumerator ToggleInteactableButtonDelayed(bool activation, float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        foreach (Button button in _buttonsMenuArray)
        {
            button.interactable = activation;
        }
    }
    #endregion
}
