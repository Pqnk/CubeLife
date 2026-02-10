using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerMenuLevel : MonoBehaviour
{
    public static event Action OnAnyInput;

    public bool hasAnyInputBeenPushed = false;

    void Update()
    {
        if (!hasAnyInputBeenPushed && (Keyboard.current?.anyKey.wasPressedThisFrame == true ||
        Mouse.current?.press.wasPressedThisFrame == true ||
        Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            if (Time.time > 1.5f)
            {
                CubeLyfeManager.Instance.audioManager.PlayUISound(UISoundType.Click01);
                hasAnyInputBeenPushed = true;
                OnAnyInput?.Invoke();
            }
        }
    }
}
