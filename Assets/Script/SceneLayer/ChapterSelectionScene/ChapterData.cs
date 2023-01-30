using System.Collections.Generic;
using UnityEngine;

namespace ScentInTheShadow.Scene.ChapterSelection
{
    [CreateAssetMenu]
    [System.Serializable]
    public class ChapterData : ScriptableObject
    {
        [System.Serializable]
        public struct Chapter
        {
            public string ChapterName;
            public bool IsUnlocked;
        }

        public List<Chapter> Chapters;
    }
}