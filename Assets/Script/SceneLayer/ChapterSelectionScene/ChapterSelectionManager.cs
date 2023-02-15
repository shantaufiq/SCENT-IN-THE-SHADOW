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
                var Datas = new ChapterData.Chapter();
                Datas = data.Chapters[i];
                temp.LoadChapterButtonInfo(Datas);
            }
        }
    }
}