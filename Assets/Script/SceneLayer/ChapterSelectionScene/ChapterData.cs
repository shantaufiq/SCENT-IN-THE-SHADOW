using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            public string TargetScene;
            public Image BackgroundImage;
            public bool IsUnlocked;
        }

        public List<Chapter> Chapters;
    }
}