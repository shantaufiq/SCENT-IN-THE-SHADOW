using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScentInTheShadow.Global.Manager;

public class ChapterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    private void Awake()
    {
        Debug.Log($"{Time.time} player has character: {GameManager.instance.Player.CharacterName}");

        GameObject go = characters.Find((x) => x.name == GameManager.instance.Player.CharacterName);
        go.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
