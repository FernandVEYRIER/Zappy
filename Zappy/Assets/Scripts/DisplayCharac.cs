using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCharac : MonoBehaviour {

    public Character character;
    public GameObject item;
    public GameObject textInfo;
    public GameObject parentIcons;
    public GameObject parentInfos;
    public Text level;

    private string[][] leveltxt;
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
        updateDisplay();
    }

    public void updateDisplay()
    {
        while (parentIcons.transform.childCount != 0)
        {
            Destroy(parentIcons.transform.GetChild(0));
        }
        while (parentInfos.transform.childCount != 0)
        {
            Destroy(parentInfos.transform.GetChild(0));
        }
        int i = 0;
        foreach (var itemInfos in character.inventory)
        {
            GameObject tmp = (GameObject)Instantiate(item, transform.position, Quaternion.identity);
            tmp.GetComponent<Image>().overrideSprite = itemInfos.sprite;
            tmp.transform.GetChild(1).GetComponent<Text>().text = "x " + itemInfos.count.ToString();
            tmp.transform.SetParent(parentIcons.transform);
            tmp.GetComponent<RectTransform>().localScale = Vector3.one;
            ++i;
        }
        for (int h = 0; h < leveltxt[character.level].Length; h++)
        {
            if (leveltxt[character.level][h] != null)
            {
                GameObject tmp = (GameObject)Instantiate(textInfo, transform.position, Quaternion.identity);
                tmp.GetComponent<Text>().text = leveltxt[character.level][h];
                if (h != 0)
                    tmp.GetComponent<Text>().color = character.inventory[h - 1].color;
                tmp.transform.SetParent(parentInfos.transform);
                tmp.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
        level.text = "Level " + (character.level + 1).ToString();
    }
}
