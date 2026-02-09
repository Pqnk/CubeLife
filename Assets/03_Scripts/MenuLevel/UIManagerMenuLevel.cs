using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMenuLevel : MonoBehaviour
{
    [Header("Background Gradient Materiel")]
    [SerializeField] private Material _gradientMaterial;
    [SerializeField] private Color _blueColor = new Color(0.17f, 0.42f, 1.0f);
    [SerializeField] private Color _magentaColor = new Color(0.7f, 0.3f, 1.0f);
    [SerializeField] private float _colorChangingSpeed = 0.03f;

    [Header("Background -- Power Range & Speed")]
    [SerializeField] private float _minPower = 0.1f;
    [SerializeField] private float _maxPower = 0.4f;
    [SerializeField] private float _speed = 0.5f;

    [Header("Text -- Press Button")]
    [SerializeField] private float _speedAlphaText = 0.75f;
    [SerializeField] private TMP_Text _pressButtonText;

    [Header("Logo")]
    [SerializeField] private RectTransform _logoRectT;
    [SerializeField] private float _speedScaleLogo = 0.9f;
    [SerializeField] private float _minScaleLogo = 0.90f;
    [SerializeField] private float _maxScaleLogo = 1.1f;
    private Image _logoImage;

    #region ---- EVENT - When User press any button ----
    void OnEnable()
    {
        InputManagerMenuLevel.OnAnyInput += ChangeUIDisplay;
    }
    void OnDisable()
    {
        InputManagerMenuLevel.OnAnyInput -= ChangeUIDisplay;
    }
    void ChangeUIDisplay()
    {
        StartCoroutine(FadeOutTextPressButton());
        StartCoroutine(PlaceLogoAtTheTop());
    }
    #endregion

    private void Start()
    {
        _logoImage = _logoRectT.GetComponent<Image>();

        StartCoroutine(PingPongAlphaTextPressButton());
        StartCoroutine(PingPongGradientBackground());
        StartCoroutine(PingPongScaleLogo());
        StartCoroutine(AppearingLogo());
    }


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
    private IEnumerator PingPongAlphaTextPressButton()
    {
        while (!MenuLevelManager.Instance.inputManagerMenuLevel.hasAnyInputBeenPushed)
        {
            float alpha = Mathf.PingPong(Time.time * _speedAlphaText, 1f);
            _pressButtonText.alpha = alpha;
            yield return null;
        }
    }
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
    private IEnumerator FadeOutTextPressButton()
    {
        float t = 0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime;
            _pressButtonText.alpha = Mathf.Lerp(1f, 0f, t / 1.0f);
            yield return null;
        }

        _pressButtonText.alpha = 0f;
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
            _logoRectT.anchoredPosition = new Vector2( 0f, y);

            yield return null;
        }
    }

    private IEnumerator AppearingLogo()
    {
        float t = 0f;
        Color color = Color.white;
        while(t < 1.0f)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0.0f, 1.0f, t / 1.0f);
            _logoImage.color = color;
            yield return null;
        }
    }
}
