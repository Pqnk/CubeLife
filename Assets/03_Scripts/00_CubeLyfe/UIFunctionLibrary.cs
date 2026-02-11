using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UIFunctionLibrary
{
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
    }

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
    }
}
