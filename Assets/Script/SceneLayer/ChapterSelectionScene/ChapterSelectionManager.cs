using UnityEngine;

namespace ScentInTheShadow.Scene.ChapterSelection
{
    public class ChapterSelectionManager : MonoBehaviour
    {
        [SerializeField] private ChapterData data;
        [SerializeField] private ChapterButtonController chapter;

        private void Start()
        {
            SpawnChapterSelection();
        }

        private void SpawnChapterSelection()
        {
            for(int i = 0; i < data.Chapters.Count; i++)
            {
                var temp = Instantiate(chapter, parent: this.transform);
                string chapterName = data.Chapters[i].ChapterName;
                bool isUnlocked = data.Chapters[i].IsUnlocked;
                temp.LoadChapterButtonInfo(chapterName, isUnlocked);
            }
        }
    }
}