using UnityEngine;

namespace StructuresAndEnumerations
{
    [System.Serializable]
    public struct LevelMusicStructure
    {
        public LevelsInGame level;
        public AudioClip backgroundMusicLevel;
    }

    [System.Serializable]
    public struct UiSoundStructure
    {
        public UISoundType soundType;
        public AudioClip sound;
    }

    [System.Serializable]
    public struct LevelStructure
    {
        public LevelsInGame level;
        public string nameLevel;
    }
}