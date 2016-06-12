using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCharac : MonoBehaviour {

    public Character character;
    public GameObject item;
    public Text levelinfos;

    void Start()
    {
        updateDisplay();
    }

    private string[][] leveltxt = new string[][] 
    {
        new string[] { "joueur : 1", "lenemate : 1", "deraumère : 0", "sibur : 0", "mendiane : 0", "phiras : 0", "thystame : 0" },
        new string[] { "joueur : 2", "lenemate : 1", "deraumère : 1", "sibur : 1", "mendiane : 0", "phiras : 0", "thystame : 0" },
        new string[] { "joueur : 2", "lenemate : 2", "deraumère : 0", "sibur : 1", "mendiane : 0", "phiras : 2", "thystame : 0" },
        new string[] { "joueur : 4", "lenemate : 1", "deraumère : 1", "sibur : 2", "mendiane : 0", "phiras : 1", "thystame : 0" },
        new string[] { "joueur : 4", "lenemate : 1", "deraumère : 2", "sibur : 1", "mendiane : 3", "phiras : 0", "thystame : 0" },
        new string[] { "joueur : 6", "lenemate : 1", "deraumère : 2", "sibur : 3", "mendiane : 0", "phiras : 1", "thystame : 0" },
        new string[] { "joueur : 6", "lenemate : 2", "deraumère : 2", "sibur : 2", "mendiane : 2", "phiras : 2", "thystame : 1" }
    };

    public void updateDisplay()
    {
        while (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0));
        }
        int i = 0;
        foreach (var obj in character.inventory)
        {
            GameObject tmp = (GameObject)Instantiate(item, transform.position, Quaternion.identity);
            tmp.GetComponent<Image>().overrideSprite = character.textures[i];
            tmp.transform.GetChild(1).GetComponent<Text>().text = "x " + obj.Value.ToString();
            tmp.transform.SetParent(transform);
            tmp.GetComponent<RectTransform>().localScale = Vector3.one;
            ++i;
        }
        levelinfos.text = "";
        foreach (string txt in leveltxt[character.level])
        {
            levelinfos.text += txt + "\n";
        }
    }
}
