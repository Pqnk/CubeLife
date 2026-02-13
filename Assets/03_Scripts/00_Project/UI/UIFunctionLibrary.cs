using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIFunctionLibrary
{

    #region ########## FADE IN / FADE OUT / CHANGE ALPHA UI ELEMENTS##########

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

    /// <summary>
    /// Animates the alpha value of a TMP_Text component to create a ping-pong fade effect while the specified condition is true.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The alpha value oscillates
    /// smoothly between 0 and 1, creating a fade-in and fade-out effect. To stop the animation, set the condition to
    /// <see langword="false"/> or stop the coroutine.</remarks>
    /// <param name="text">The TMP_Text component whose alpha value will be animated.</param>
    /// <param name="condition">A value indicating whether the animation should continue. The animation runs while this value is true. Defaults
    /// to <see langword="true"/>.</param>
    /// <returns>An enumerator that animates the alpha value of the specified text component. This can be used with a coroutine
    /// to perform the animation over time.</returns>
    public static IEnumerator PingPongAlphaText(TMP_Text text, bool condition = true)
    {
        while (condition)
        {
            text.alpha = Mathf.PingPong(Time.time * 0.75f, 1f);

            yield return null;
        }
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

    /// <summary>
    /// Animates the scale of a RectTransform in a ping-pong pattern between specified minimum and maximum values over time.
    /// </summary>
    /// <remarks>This method is intended for use with Unity's coroutine system. The scale is updated every
    /// frame, creating a smooth oscillating effect. To stop the animation, set <paramref name="condition"/> to <see
    /// langword="false"/>.</remarks>
    /// <param name="rectTransform">The RectTransform to be scaled during the animation. Cannot be null.</param>
    /// <param name="minScale">The minimum scale factor applied to the RectTransform. Must be less than or equal to <paramref
    /// name="maxScale"/>.</param>
    /// <param name="maxScale">The maximum scale factor applied to the RectTransform. Must be greater than or equal to <paramref
    /// name="minScale"/>.</param>
    /// <param name="frequency">The frequency, in cycles per second, at which the scale oscillates between the minimum and maximum values. Must
    /// be positive.</param>
    /// <param name="condition">A Boolean value indicating whether the animation should continue. The animation runs while this value is <see
    /// langword="true"/>.</param>
    /// <returns>An enumerator that animates the RectTransform's scale in a ping-pong pattern while <paramref name="condition"/>
    /// is <see langword="true"/>.</returns>
    public static IEnumerator PingPongScaleRectT(RectTransform rectTransform, float minScale = 0.95f, float maxScale = 1.05f, float frequency = 1f, bool condition = true)
    {
        while (condition)
        {
            float alpha = Mathf.PingPong(Time.time * frequency, 1f);
            float scaleValue = Mathf.Lerp(minScale, maxScale, alpha);
            Vector3 scale = new Vector3(scaleValue, scaleValue, scaleValue);
            rectTransform.localScale = scale;
            yield return null;
        }
    }

    #endregion


    #region ########## AUTOMATED PROGRESSION BARS ##########

    /// <summary>
    /// Animates a loading bar's fill amount to visually represent the specified loading progress.
    /// </summary>
    /// <remarks>The animation progresses smoothly toward the target fill amount based on the provided loading
    /// progress. The method is intended for use with Unity's coroutine system. The loading bar's fillAmount property is
    /// updated each frame until the target progress is reached.</remarks>
    /// <param name="loadingBar">The Image component representing the loading bar to update. Must not be null.</param>
    /// <param name="loadingProgress">The current loading progress, typically a value between 0.0 and 1.0, where 1.0 indicates completion.</param>
    /// <returns>An enumerator that animates the loading bar's fill amount over time. This can be used with a coroutine to
    /// perform the animation asynchronously.</returns>
    public static IEnumerator ProgressValueLoadingBarFromImage(Image loadingBar, float loadingProgress)
    {
        float displayedProgress = 0f;
        float targetProgress = loadingProgress / 0.9f;

        while (displayedProgress < 1f)
        {
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime * 0.25f) ;
            loadingBar.fillAmount = displayedProgress;
            yield return null;
        }
    }
    #endregion

}
