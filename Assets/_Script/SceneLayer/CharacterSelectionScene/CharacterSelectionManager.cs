using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScentInTheShadow.Scene.CharacterSelection
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        [SerializeField] private List<string> characterName;
        [SerializeField] private CharacterSelectionButton obj;

        private void Start()
        {
            SpawnCharacterList();
        }

        private void SpawnCharacterList()
        {
            for(int i = 0; i < characterName.Count; i++)
            {
                var temp = Instantiate(obj, parent: this.transform);
                temp.LoadCharacterButtonInfo(characterName[i]);
            }
        }
    }
}