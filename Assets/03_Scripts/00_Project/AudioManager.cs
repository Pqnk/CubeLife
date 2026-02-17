using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructuresAndEnumerations;


public sealed class AudioManager : MonoBehaviour
{
    #region /////////////////// PRIVATE SINGLETON \\\\\\\\\\\\\\\\\\\\\
    [Header("########## SINGLETON - AUDIO MANAGER ###########")]
    private static AudioManager Instance { get; set; }
    #endregion


    #region /////////////////// SERIALIZEFIELD AUDIO \\\\\\\\\\\\\\\\\\\\\

    public float BackgroundVolume { get { return _backgroundMusicSource.volume; }  }
    public float EffectVolume { get; private set; }
    public float UIVolume { get; private set; }

    [Header("########## SOURCE FOR BACKGROUND MUSIC ##########")]
    private AudioSource _backgroundMusicSource;
    private float _backgroundmusicMaxVolume;

    [Space]
    [Header("########## BACKGROUND MUSICS ##########")]
    [SerializeField] private List<LevelMusicStructure> _backgroundMusicsByLevel;
    private Dictionary<LevelsInGame, AudioClip> _bckgrndMusicDictionary;

    [Space]
    [Header("########### UI SOUNDS ###########")]
    [SerializeField] private List<UiSoundStructure> _uiSounds;
    private Dictionary<UISoundType, AudioClip> _uiSoundsDictionary;

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _backgroundMusicSource = this.GetComponent<AudioSource>();
        _backgroundmusicMaxVolume = _backgroundMusicSource.volume;

        InitializeDictionaryBackgroundMusicOnAwake();
        InitializeDictionaryUISoundsOnAwake();
    }

    private void OnEnable()
    {
        LevelManager.OnLoadedScene += PlayBackgroundMusicByFadingIn;
    }

    private void OnDisable()
    {
        LevelManager.OnLoadedScene -= PlayBackgroundMusicByFadingIn;
    }

    private void Start()
    {
        if (SaveParametersManager.DoesSaveFileAlreadyExists)
        {
            ChangeMusicVolume(SaveParametersManager.SaveDataGridParameters.musicVolume);
            ChangeUISoundsVolume(SaveParametersManager.SaveDataGridParameters.uiVolume);
            EffectVolume = SaveParametersManager.SaveDataGridParameters.effectVolume;
        }
    }

    #region ########## INITIALIZING THE DICTIONARIES ############
    private void InitializeDictionaryBackgroundMusicOnAwake()
    {
        _bckgrndMusicDictionary = new Dictionary<LevelsInGame, AudioClip>();

        foreach (var entry in _backgroundMusicsByLevel)
        {
            if (!_bckgrndMusicDictionary.ContainsKey(entry.level))
            {
                _bckgrndMusicDictionary.Add(entry.level, entry.backgroundMusicLevel);
            }
            else
            {
                Debug.LogWarning($"Duplicate UISoundType: {entry.level}");
            }
        }
    }
    private void InitializeDictionaryUISoundsOnAwake()
    {
        _uiSoundsDictionary = new Dictionary<UISoundType, AudioClip>();

        foreach (var entry in _uiSounds)
        {
            if (!_uiSoundsDictionary.ContainsKey(entry.soundType))
            {
                _uiSoundsDictionary.Add(entry.soundType, entry.sound);
            }
            else
            {
                Debug.LogWarning($"Duplicate UISoundType: {entry.soundType}");
            }
        }
    }
    #endregion


    #region ############ BACKGROUND MUSIC ##########

    /// <summary>
    /// Gradually reduces the background music volume to zero, fading it out over time.
    /// </summary>
    /// <remarks>This method initiates a fade-out effect for the currently playing background music. It is
    /// typically used to smoothly transition audio when stopping or changing tracks. The fade duration and behavior
    /// depend on the implementation of the underlying coroutine.</remarks>
    public void StopBackgroundMusicByFadingAway()
    {
        StartCoroutine(LowerMusicVolumeToZero());
    }

    /// <summary>
    /// Gradually reduces the background music volume to zero over time.
    /// </summary>
    /// <remarks>This method is intended to be used as a coroutine in Unity. The volume decreases smoothly
    /// until it reaches zero, allowing for a fade-out effect. The operation is frame-based and should be started using
    /// Unity's coroutine system.</remarks>
    /// <returns>An enumerator that performs the volume reduction operation when iterated.</returns>
    private IEnumerator LowerMusicVolumeToZero()
    {
        while (_backgroundMusicSource.volume > 0f)
        {
            _backgroundMusicSource.volume = Mathf.MoveTowards(_backgroundMusicSource.volume, 0f, Time.deltaTime * 0.2f);
            yield return null;
        }
        _backgroundMusicSource.Stop();
    }

    /// <summary>
    /// Gradually increases the background music volume to its maximum level, creating a fade-in effect.
    /// </summary>
    /// <remarks>This method is typically used to smoothly start background music without abrupt changes in
    /// volume. It is safe to call multiple times; repeated calls will restart the fade-in process.</remarks>
    public void PlayBackgroundMusicByFadingIn(LevelsInGame levelLoaded)
    {
        if (_bckgrndMusicDictionary.TryGetValue(levelLoaded, out AudioClip clip))
        {
            _backgroundMusicSource.clip = clip;
            _backgroundMusicSource.Play();
            StartCoroutine(UpMusicVolumeToMaxSettings());
        }
    }

    /// <summary>
    /// Gradually increases the background music volume to its maximum level over time.
    /// </summary>
    /// <remarks>Use this method as a coroutine to smoothly transition the background music volume to its
    /// maximum value. The speed of the transition depends on the implementation details and may vary based on frame
    /// rate.</remarks>
    /// <returns>An enumerator that controls the progression of the volume increase operation. This can be used with a coroutine
    /// to perform the volume adjustment asynchronously.</returns>
    private IEnumerator UpMusicVolumeToMaxSettings()
    {
        while (_backgroundMusicSource.volume < SaveParametersManager.SaveDataGridParameters.musicVolume)
        {
            _backgroundMusicSource.volume = Mathf.MoveTowards(_backgroundMusicSource.volume, SaveParametersManager.SaveDataGridParameters.musicVolume, Time.deltaTime * 0.2f);
            yield return null;
        }
    }

    public void ChangeMusicVolume(float vol)
    {
        _backgroundMusicSource.volume = vol;
    }

    #endregion


    #region ########## UI SOUNDS / FEEDBACK ##########

    public void PlayUISound(UISoundType soundType)
    {
        if (_uiSoundsDictionary.TryGetValue(soundType, out AudioClip clip))
        {
            GameObject soundCaster = new GameObject();
            soundCaster.name = "---- UI SOUND : {0} ----";
            soundCaster.name = string.Format(soundCaster.name, soundType);
            soundCaster.transform.SetParent(transform, false);
            AudioSource soundCasterAudioSource = soundCaster.AddComponent<AudioSource>();
            soundCasterAudioSource.playOnAwake = false;
            soundCasterAudioSource.loop = false;
            soundCasterAudioSource.clip = clip;
            soundCasterAudioSource.volume = UIVolume;
            soundCasterAudioSource.Play();
            StartCoroutine(DestroyWhenSoundFinishedp(soundCasterAudioSource));
        }

    }

    /// <summary>
    /// Waits for the audio clip to finish playing on the specified AudioSource, then destroys the associated GameObject.
    /// </summary>
    /// <remarks>This method is intended for use with Unity coroutines. If the AudioSource's clip is null, the
    /// wait time will be zero and the GameObject will be destroyed immediately.</remarks>
    /// <param name="source">The AudioSource whose clip duration determines the wait time. Must have a valid, non-null clip assigned.</param>
    /// <returns>An enumerator that waits for the length of the audio clip before destroying the AudioSource's GameObject.</returns>
    IEnumerator DestroyWhenSoundFinishedp(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source.gameObject);
    }


    public void ChangeUISoundsVolume(float newVolume)
    {
        UIVolume = newVolume;
    }
    #endregion

}
