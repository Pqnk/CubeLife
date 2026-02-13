using System;
using UnityEngine;
using UnityEngine.InputSystem;
using StructuresAndEnumerations;

public class InputManagerMenuLevel : MonoBehaviour
{
    public static event Action OnAnyInput;

    private bool _hasAnyInputBeenPushed = false;

    void Update()
    {
        if (!_hasAnyInputBeenPushed && (Keyboard.current?.anyKey.wasPressedThisFrame == true ||
        Mouse.current?.press.wasPressedThisFrame == true ||
        Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            if (Time.time > 1.5f)
            {
                ProjectManager.Instance.audioManager.PlayUISound(UISoundType.Click01);
                _hasAnyInputBeenPushed = true;
                OnAnyInput?.Invoke();
            }
        }
    }
}
