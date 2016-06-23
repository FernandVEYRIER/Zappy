﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayCharac : MonoBehaviour {

    private Character character = null;
    public GameObject item;
    public GameObject textInfo;
    public GameObject parentIcons;
    public GameObject parentInfos;
    private AxisKeyDown axes;
    public Text level;
    public Text IANum;
    private List<Character> characters = new List<Character>();
    private string[][] leveltxt;
    private int index = 0;
    private List<GameObject> instances = new List<GameObject>();
    //private string[][] leveltxt = new string[][]
    //{
    //    new string[] { "joueur : 1", "lenemate : 1", "deraumère : 0", "sibur : 0", "mendiane : 0", "phiras : 0", "thystame : 0" },
    //    new string[] { "joueur : 2", "lenemate : 1", "deraumère : 1", "sibur : 1", "mendiane : 0", "phiras : 0", "thystame : 0" },
    //    new string[] { "joueur : 2", "lenemate : 2", "deraumère : 0", "sibur : 1", "mendiane : 0", "phiras : 2", "thystame : 0" },
    //    new string[] { "joueur : 4", "lenemate : 1", "deraumère : 1", "sibur : 2", "mendiane : 0", "phiras : 1", "thystame : 0" },
    //    new string[] { "joueur : 4", "lenemate : 1", "deraumère : 2", "sibur : 1", "mendiane : 3", "phiras : 0", "thystame : 0" },
    //    new string[] { "joueur : 6", "lenemate : 1", "deraumère : 2", "sibur : 3", "mendiane : 0", "phiras : 1", "thystame : 0" },
    //    new string[] { "joueur : 6", "lenemate : 2", "deraumère : 2", "sibur : 2", "mendiane : 2", "phiras : 2", "thystame : 1" }
    //};

    void Start()
    {
        leveltxt = new string[][]
        {
            new string[] { "joueur : 1", "lenemate : 1", null, null, null, null, null },
            new string[] { "joueur : 2", "lenemate : 1", "deraumère : 1", "sibur : 1", null, null, null },
            new string[] { "joueur : 2", "lenemate : 2", null, "sibur : 1", null,  "phiras : 2", null},
            new string[] { "joueur : 4", "lenemate : 1", "deraumère : 1", "sibur : 2", "phiras : 1", null },
            new string[] { "joueur : 4", "lenemate : 1", "deraumère : 2", "sibur : 1", "mendiane : 3", null, null },
            new string[] { "joueur : 6", "lenemate : 1", "deraumère : 2", "sibur : 3", null, "phiras : 1", null },
            new string[] { "joueur : 6", "lenemate : 2", "deraumère : 2", "sibur : 2", "mendiane : 2", "phiras : 2", "thystame : 1" }
        };
        axes = GetComponent<AxisKeyDown>();
        axes.callbacks.Add("Next", OnNext);
        axes.callbacks.Add("Prev", OnPrev);
        updateDisplay();
    }

    public void addCharacters(Character player)
    {
        characters.Add(player);
        character = player;
    }

    public void removeCharacters(Character player)
    {
        characters.Remove(player);
    }

    void Update()
    {
        if (character != null && !character._isUpdate)
        {
            updateDisplay();
        }
    }

    void OnNext()
    {
        
        if (character)
        {
            ++index;
            Character tmp = character;
            character = characters[index % characters.Count];
            tmp._isUpdate = false;
            updateDisplay();
        }
    }

    void OnPrev()
    {
        if (character)
        {
            --index;
            if (index < 0)
                index = characters.Count - 1;
            Character tmp = character;
            character = characters[index % characters.Count];
            tmp._isUpdate = false;
            updateDisplay();
        }
    }

    public void updateDisplay()
    {
        if (character)
        {
            instances.ForEach(chield => Destroy(chield));
            int i = 0;
            foreach (var itemInfos in character.inventory)
            {
                GameObject tmp = (GameObject)Instantiate(item, transform.position, Quaternion.identity);
                tmp.GetComponent<Image>().overrideSprite = itemInfos.sprite;
                tmp.transform.GetChild(1).GetComponent<Text>().text = "x " + itemInfos.count.ToString();
                tmp.transform.SetParent(parentIcons.transform);
                tmp.GetComponent<RectTransform>().localScale = Vector3.one;
                instances.Add(tmp);
                ++i;
            }
            for (int h = 0; h < leveltxt[character._level].Length; h++)
            {
                if (leveltxt[character._level][h] != null)
                {
                    GameObject tmp = (GameObject)Instantiate(textInfo, transform.position, Quaternion.identity);
                    tmp.GetComponent<Text>().text = leveltxt[character._level][h];
                    if (h != 0)
                        tmp.GetComponent<Text>().color = character.inventory[h - 1].color;
                    tmp.transform.SetParent(parentInfos.transform);
                    tmp.GetComponent<RectTransform>().localScale = Vector3.one;
                    instances.Add(tmp);
                }
            }
            level.text = "Level " + (character._level + 1).ToString();
            IANum.text = "IA N°" + character._id + " - " + character._team;
            character._isUpdate = true;
        }
    }
}
