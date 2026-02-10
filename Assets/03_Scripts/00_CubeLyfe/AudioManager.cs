using System.Collections;
using UnityEngine;

public enum UISoundType
{
    Click01,
    Click02,
    Click03,
    Click04
}


public class AudioManager : MonoBehaviour
{
    [Header("---- MUSIC ----")]
    [SerializeField] private AudioSource _backgroundMusic;

    [Space]
    [Header("---- UI AUDIO ----")]
    [Header("UI - Clicks")]
    [SerializeField] private float _uiSoundVolume = 0.4f;
    [SerializeField] private AudioClip _uiClick01;
    [SerializeField] private AudioClip _uiClick02;
    [SerializeField] private AudioClip _uiClick03;
    [SerializeField] private AudioClip _uiClick04;

    public void PlayUISound(UISoundType soundType)
    {
        GameObject soundCaster = new GameObject();
        soundCaster.name = "---- UI SOUND : {0} ----";
        soundCaster.name = string.Format(soundCaster.name, soundType);
        soundCaster.transform.SetParent(transform, false);
        AudioSource soundCasterAudioSource = soundCaster.AddComponent<AudioSource>();
        soundCasterAudioSource.playOnAwake = false;
        soundCasterAudioSource.loop = false;
        soundCasterAudioSource.volume = _uiSoundVolume;

        AudioClip clipSound = _uiClick01;

        switch (soundType)
        {
            case UISoundType.Click01:
                clipSound = _uiClick01;
                break;

            case UISoundType.Click02:
                clipSound = _uiClick02;
                break;

            case UISoundType.Click03:
                clipSound = _uiClick03;
                break;

            case UISoundType.Click04:
                clipSound = _uiClick04;
                break;
        }
        soundCasterAudioSource.clip = clipSound;
        soundCasterAudioSource.Play();
        StartCoroutine(DestroyWhenSoundFinishedp(soundCasterAudioSource));
    }

    IEnumerator DestroyWhenSoundFinishedp(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source.gameObject);
    }
}
