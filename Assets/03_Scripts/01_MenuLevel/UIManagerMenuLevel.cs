using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Vector2 _startPosLogo;

    [Space]
    [Header("Panels")]
    [SerializeField] private Button[] _buttonsMenuArray;

    [Space]
    [Header("Loading Bar Cube-shaped")]
    [SerializeField] private Image _loadingCubeBar;
    [SerializeField] private TMP_Text _loadingText;

    #endregion

    private void Awake()
    {
        //  Saving the Image component from the UI object containing the Logo
        _logoImage = _logoRectT.GetComponent<Image>();
        _startPosLogo = new Vector2(_logoRectT.localPosition.x, _logoRectT.localPosition.y);

        //  Initializing buttons appearance - Invisible and not able to interact
        foreach (Button button in _buttonsMenuArray)
        {
            Image buttonImage = button.GetComponent<Image>();
            TMP_Text buttonText = button.transform.GetChild(0).GetComponent<TMP_Text>();
            Color alphaZeroWhite = new Color(1, 1, 1, 0);
            Color alphaZeroColor = buttonText.color;
            alphaZeroColor.a = 0;

            // Cannot Interact with buttons and buttons invisibles (Image and Text)
            button.interactable = false;
            buttonImage.color = alphaZeroWhite;
            buttonText.color = alphaZeroColor;

            //  Setting the loading bar to 0
            _loadingCubeBar.fillAmount = 0f;
        }
    }

    private void Start()
    {
        //  Starting the background gradient animation from Shader
        StartCoroutine(PingPongGradientBackground());

        //  Starting the ping-pong effects on Logo and Text
        StartCoroutine(PingPongAlphaTextPressButton());
        StartCoroutine(PingPongScaleLogo());

        //  Logo fades-in
        FadeInorFadeOut_Logo(true);
    }


    #region ########## EVENT - ANY KEY PRESSED ###########
    void OnEnable()
    {
        InputManagerMenuLevel.OnAnyInput += ChangeUIDisplayWhenAnyKeyPressed;
        LevelManager.OnLoadingScene += ChangeValueLoadingBar;
    }
    void OnDisable()
    {
        InputManagerMenuLevel.OnAnyInput -= ChangeUIDisplayWhenAnyKeyPressed;
        LevelManager.OnLoadingScene -= ChangeValueLoadingBar;
    }
    void ChangeUIDisplayWhenAnyKeyPressed()
    {
        StartCoroutine(FadeInorFadeOut_Buttons(true, 1.0f, 0.2f));
        FadeOutAndMoveTextWhenAnyKeyPressed();
        MoveAndScaleToTheTopScreen_Logo();
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


    #region ########## TEXT - PRESS ANY KEY ###########
    private IEnumerator PingPongAlphaTextPressButton()
    {
        while (!MenuLevelManager.Instance.inputManagerMenuLevel.hasAnyInputBeenPushed)
        {
            float alpha = Mathf.PingPong(Time.time * _speedAlphaText, 1f);
            _pressAnyKeyText.alpha = alpha;
            yield return null;
        }
    }
    private void FadeOutAndMoveTextWhenAnyKeyPressed()
    {
        StartCoroutine(UIFunctionLibrary.LerpAlphaTMPPro(_pressAnyKeyText, 1f, 0f));

        Vector2 startPos = new Vector2(0f, 215f);
        Vector2 endPos = new Vector2(0f, 100f);
        StartCoroutine(UIFunctionLibrary.MoveUIRectT(_pressAnyKeyText.transform.GetComponent<RectTransform>(), startPos, endPos));
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
    private void MoveAndScaleToTheTopScreen_Logo()
    {
        StartCoroutine(UIFunctionLibrary.MoveUIRectT(_logoRectT, new Vector2(0f, 78f), new Vector2(0f, 330f)));
        StartCoroutine(UIFunctionLibrary.ScaleUIRectT(_logoRectT, 1.0f, 0.6f));
    }
    private void FadeInorFadeOut_Logo(bool isFadeIn, float fadeDuration = 1f)
    {
        float startValue = isFadeIn ? 0f : 1f;
        float endValue = isFadeIn ? 1f : 0f;
        StartCoroutine(UIFunctionLibrary.LerpAlphaImage(_logoImage, startValue, endValue, fadeDuration));
    }
    #endregion


    #region ######### BUTTONS MENU ##########
    public void OnClickButton_Play()
    {
        EventSystem.current.SetSelectedGameObject(null);

        CubeLyfeManager.Instance.audioManager.PlayUISound(UISoundType.Click01);
        CubeLyfeManager.Instance.audioManager.FadeAwayBackgroundMusic();

        StartCoroutine(FadeInorFadeOut_Buttons(false, 0.3f, 0.1f));
        FadeIn_LoadingBar(true);
        StartCoroutine(UIFunctionLibrary.LerpAlphaTMPPro(_loadingText, 0f, 1f));

        Vector2 currentPosLogo = new Vector2(_logoRectT.localPosition.x, _logoRectT.localPosition.y);
        StartCoroutine(UIFunctionLibrary.MoveUIRectT(_logoRectT, currentPosLogo, _startPosLogo));


        StartCoroutine(CubeLyfeManager.Instance.levelManager.LoadLevelAsync(CubeLyfeLevels.L_01_GameOfLifeLevel));
    }
    public void OnClickButton_Settings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        CubeLyfeManager.Instance.audioManager.PlayUISound(UISoundType.Click01);
        StartCoroutine(FadeInorFadeOut_Buttons(false, 0.3f, 0.1f));
    }
    public void OnClickButton_Quit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        CubeLyfeManager.Instance.audioManager.PlayUISound(UISoundType.Click01);

        Application.Quit();
    }

    private IEnumerator FadeInorFadeOut_Buttons(bool isFadeIn, float fadeDuration = 1f, float delayBetweenButtons = 0.2f)
    {
        float startValue = isFadeIn ? 0f : 1f;
        float endValue = isFadeIn ? 1f : 0f;
        
        foreach(Button button in _buttonsMenuArray)
        {
            button.interactable = false;
        }

        foreach (Button button in _buttonsMenuArray)
        {
            StartCoroutine(UIFunctionLibrary.LerpAlphaButtonTMPPro(button, startValue, endValue, fadeDuration));
            yield return new WaitForSeconds(delayBetweenButtons);
        }
    }

    #endregion


    #region ########## LOADING BAR CUBE-SHAPED ##########

    private void ChangeValueLoadingBar(float loadingProgress)
    {
        StartCoroutine(UIFunctionLibrary.ProgressValueLoadingBarFromImage(_loadingCubeBar, loadingProgress));
    }

    private void FadeIn_LoadingBar(bool isFadeIn)
    {
        StartCoroutine(UIFunctionLibrary.LerpAlphaImage(_loadingCubeBar));
    }

    #endregion
}
