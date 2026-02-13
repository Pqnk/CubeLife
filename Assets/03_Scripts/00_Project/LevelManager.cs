using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using StructuresAndEnumerations;


public sealed class LevelManager : MonoBehaviour
{
    [Header("########## LEVEL PARAMETRING ENTRY ##########")]
    [SerializeField] private List<LevelStructure> _levelsParametersList;
    private Dictionary<LevelsInGame, string> _levelsDictinary;

    private static LevelManager Instance { get; set; }

    public static event Action<float> OnLoadingScene;

    public static event Action<LevelsInGame> OnLoadedScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeLevelsDictionaryOnAwake();
    }

    private void InitializeLevelsDictionaryOnAwake()
    {
        _levelsDictinary = new Dictionary<LevelsInGame, string>();

        foreach (var entry in _levelsParametersList)
        {
            if (!_levelsDictinary.ContainsKey(entry.level))
            {
                _levelsDictinary.Add(entry.level, entry.nameLevel);
            }
            else
            {
                Debug.LogWarning($"Duplicate UISoundType: {entry.level}");
            }
        }
    }

    public void LoadLevel(LevelsInGame levelToLoad)
    {
        if (_levelsDictinary.TryGetValue(levelToLoad, out string levelNameToLoad))
        {
            StartCoroutine(LoadLevelAsync(levelToLoad, levelNameToLoad));
        }
    }

    private IEnumerator LoadLevelAsync(LevelsInGame levelToLoad, string levelNameToLoad)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync($"{levelNameToLoad}");
        op.allowSceneActivation = false;

        OnLoadingScene?.Invoke(0.9f);

        yield return new WaitForSeconds(4f);

        op.allowSceneActivation = true;

        OnLoadedScene?.Invoke(levelToLoad);

    }

}
