using ScentInTheShadow.Global.Manager;
using ScentInTheShadow.Global.PlayerData;
using ScentInTheShadow.Scene.LoadingScene;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.CharacterSelection
{
    public class CharacterSelectionButton : MonoBehaviour
    {
        public Button button;

        [SerializeField] private TMP_Text characterNameText;

        private string characterName;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SaveCharacter);
        }

        private void SaveCharacter()
        {
            StartCoroutine(SaveCharacterCoroutine());
        }

        IEnumerator SaveCharacterCoroutine()
        {
            var Data = GameManager.instance.Player;
            var request = new PlayerDataModel()
            {
                Health = Data.Health,
                Experience = Data.Experience,
                Skor = Data.Skor,
                Level = Data.Level,
                CharacterName = characterName,
                items = Data.items
            };

            GameManager.instance.SetPlayerData(request);
            yield return new WaitForSeconds(0.5f);
            GameManager.instance.GetPlayerData();
            yield return new WaitForSeconds(0.7f);
            LoadingManager.instance.StartLoading("OpeningScene");
        }

        public void LoadCharacterButtonInfo(string _characterName)
        {
            characterName = _characterName;
            characterNameText.text = characterName;
        }
    }
}