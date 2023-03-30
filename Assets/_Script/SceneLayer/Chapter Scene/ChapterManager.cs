using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScentInTheShadow.Global.Manager;

public class ChapterManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"{Time.time} player has character: {GameManager.instance.Player.CharacterName}");
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
