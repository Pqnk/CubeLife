using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIFunctionLibrary
{
    #region ########## FADE IN / FADE OUT ##########

    /// <summary>
    /// Animates the alpha (transparency) value of a Unity UI Image component from a starting value to an ending value over a specified duration.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The alpha value of the Image is
    /// updated each frame over the specified duration.</remarks>
    /// <param name="imageToLerp">The Image component whose alpha value will be interpolated. Cannot be null.</param>
    /// <param name="startValue">The initial alpha value to start the interpolation from. Typically between 0 (fully transparent) and 1 (fully opaque).</param>
    /// <param name="endValue">The target alpha value to interpolate to. Typically between 0 (fully transparent) and 1 (fully opaque).</param>
    /// <param name="duration">The duration, in seconds, over which the interpolation occurs. Must be greater than 0.</param>
    /// <returns>An enumerator that performs the alpha interpolation when used with a coroutine.</returns>
    public static IEnumerator LerpAlphaImage(Image imageToLerp, float startValue = 0f, float endValue = 1f, float duration = 1f)
    {
        float t = 0f;
        Color c = imageToLerp.color;

        startValue = Mathf.Clamp(startValue, 0f, 1f);
        endValue = Mathf.Clamp(endValue, 0f, 1f);
        duration = duration <= 0f ? 1.0f : duration;

        while (t < duration)
        {
            float alphaValue = Mathf.Lerp(startValue, endValue, t / duration);
            c.a = alphaValue;
            imageToLerp.color = c;

            t += Time.deltaTime;

            yield return null;
        }

        if (endValue <= 0f)
        {
            c.a = 0f;
            imageToLerp.color = c;
        }
    }

    /// <summary>
    /// Animates the alpha (transparency) of a Unity UI Button and its child TextMeshPro text from a starting value to
    /// an ending value over a specified duration.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The alpha values of both the
    /// Button's Image and its first child TMP_Text component are updated each frame during the animation.</remarks>
    /// <param name="button">The Button whose image and child TextMeshPro text alpha values will be animated. Must not be null and must have
    /// an Image component and a child with a TMP_Text component.</param>
    /// <param name="startValue">The initial alpha value to start the animation from. Typically between 0 (fully transparent) and 1 (fully
    /// opaque).</param>
    /// <param name="endValue">The target alpha value to animate to. Typically between 0 (fully transparent) and 1 (fully opaque).</param>
    /// <param name="duration">The duration, in seconds, over which to animate the alpha values. Must be greater than 0.</param>
    /// <returns>An enumerator that performs the alpha animation when used with a coroutine.</returns>
    public static IEnumerator LerpAlphaButtonTMPPro(Button button, float startValue = 0f, float endValue = 1f, float duration = 1f)
    {
        float t = 0f;

        startValue = Mathf.Clamp(startValue, 0f, 1f);
        endValue = Mathf.Clamp(endValue, 0f, 1f);
        duration = duration <= 0f ? 1.0f : duration;

        button.interactable = false;


        Image buttonImage = button.GetComponent<Image>();

        TMP_Text buttonText = null;
        if ((button.transform.childCount > 0) && (button.transform.GetChild(0).TryGetComponent<TMP_Text>(out TMP_Text txt)))
            buttonText = txt;

        Color buttonImageOldColor = buttonImage.color;
        Color buttonTextOldColor = buttonText != null ? buttonText.color : Color.white;

        while (t < duration)
        {
            float a = Mathf.Lerp(startValue, endValue, t / duration);

            buttonImageOldColor.a = a;
            buttonTextOldColor.a = a;

            buttonImage.color = buttonImageOldColor;

            if (buttonText != null)
                buttonText.color = buttonTextOldColor;

            t += Time.deltaTime;

            yield return null;

        }

        if (endValue <= 0f)
        {
            buttonImageOldColor.a = 0f;
            buttonTextOldColor.a = 0f;
            buttonImage.color = buttonImageOldColor;

            if (buttonText != null)
                buttonText.color = buttonTextOldColor;
        }

        bool activation = startValue < endValue;
        button.interactable = activation;
    }

    /// <summary>
    /// Animates the alpha value of a TMP_Text component from a starting value to an ending value over a specified
    /// duration.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The alpha value of the text is
    /// updated each frame over the specified duration. The method does not automatically handle null references for the
    /// text parameter; callers should ensure the component is valid before invoking.</remarks>
    /// <param name="text">The TMP_Text component whose alpha value will be animated. Cannot be null.</param>
    /// <param name="startValue">The initial alpha value at the start of the animation. Clamped between 0 and 1. Defaults to 0.</param>
    /// <param name="endValue">The target alpha value at the end of the animation. Clamped between 0 and 1. Defaults to 1.</param>
    /// <param name="duration">The duration, in seconds, over which the alpha value is interpolated. If less than or equal to 0, a default of 1
    /// second is used.</param>
    /// <returns>An enumerator that performs the alpha interpolation when used with a coroutine.</returns>
    public static IEnumerator LerpAlphaTMPPro(TMP_Text text, float startValue = 0f, float endValue = 1f, float duration = 1f)
    {
        float t = 0f;

        startValue = Mathf.Clamp(startValue, 0f, 1f);
        endValue = Mathf.Clamp(endValue, 0f, 1f);
        duration = duration <= 0f ? 1.0f : duration;

        while (t < duration)
        {
            float a = Mathf.Lerp(startValue, endValue, t / duration);
            text.alpha = a;
            t += Time.deltaTime;
            yield return null;
        }

        if (endValue <= 0f)
            text.alpha = 0f;
    }

    #endregion


    #region ########## CHANGE RECT TRANSFORM ##########

    /// <summary>
    /// Animates the movement of a UI RectTransform from a start position to an end position over a specified duration.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The RectTransform's anchored
    /// position is updated each frame to interpolate between the start and end positions over the specified
    /// duration.</remarks>
    /// <param name="rectTransform">The RectTransform to move. Cannot be null.</param>
    /// <param name="startPosition">The starting anchored position of the RectTransform.</param>
    /// <param name="endPosition">The target anchored position to move the RectTransform to.</param>
    /// <param name="duration">The duration, in seconds, over which to animate the movement. Must be greater than zero. Defaults to 1 second.</param>
    /// <returns>An enumerator that performs the animation when used with a coroutine.</returns>
    public static IEnumerator MoveUIRectT(RectTransform rectTransform, Vector2 startPosition, Vector2 endPosition, float duration = 1f)
    {
        float t = 0f;
        duration = duration <= 0f ? 1.0f : duration;

        while (t < duration)
        {
            float x = Mathf.Lerp(startPosition.x, endPosition.x, t / duration);
            float y = Mathf.Lerp(startPosition.y, endPosition.y, t / duration);
            rectTransform.anchoredPosition = new Vector2(x, y);

            t += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// Animates the scale of a UI RectTransform from a starting value to an ending value over a specified duration.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The RectTransform's localScale is
    /// updated each frame to interpolate between the start and end scale values. The animation is frame-rate dependent
    /// and will complete after the specified duration.</remarks>
    /// <param name="rectTransform">The RectTransform to scale. Cannot be null.</param>
    /// <param name="startScale">The initial scale value to start the animation from.</param>
    /// <param name="endScale">The target scale value to animate to.</param>
    /// <param name="duration">The duration, in seconds, over which the scaling animation occurs. Must be greater than zero.</param>
    /// <returns>An enumerator that performs the scaling animation when used with a coroutine.</returns>
    public static IEnumerator ScaleUIRectT(RectTransform rectTransform, float startScale = 0f, float endScale = 1f, float duration = 1f)
    {
        float t = 0f;
        duration = duration <= 0f ? 1.0f : duration;

        while (t < duration)
        {
            float newScaleValue = Mathf.Lerp(startScale, endScale, t / duration);
            rectTransform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);

            t += Time.deltaTime;

            yield return null;
        }
    }

    #endregion

    #region ########## AUTOMATED PROGRESSION BARS ##########
    public static IEnumerator ProgressValueLoadingBarFromImage(Image loadingBar, float loadingProgress)
    {
        float displayedProgress = 0f;
        float targetProgress = loadingProgress / 0.9f;

        while (displayedProgress < 1f)
        {
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime * 0.5f);
            loadingBar.fillAmount = displayedProgress;
            yield return null;
        }
    }
    #endregion

}
