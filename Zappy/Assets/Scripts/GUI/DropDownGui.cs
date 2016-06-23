using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownGui : MonoBehaviour {

    private Dropdown dropdown;
    [SerializeField]
    private Sprite sprite;
    private string current_select;
    private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
    private List<string> options_str = new List<string>();
    // Use this for initialization
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        refresh();
    }

    public void refresh()
    {
        options.Clear();
        options_str.Clear();
        dropdown.ClearOptions();
        string dir_path = Application.dataPath + "/IA/Lua/";
        if (!Directory.Exists(dir_path))
            return;
        string[] fileEntries = Directory.GetFiles(dir_path);
        foreach (string fileName in fileEntries)
        {
            options_str.Add(fileName);
            Dropdown.OptionData tmp = new Dropdown.OptionData(fileName.Substring(dir_path.Length), sprite);
            options.Add(tmp);
        }
        dropdown.AddOptions(options);
    }

    public void addPlayer(InputField input)
    {
        if (GameManager.instance)
            GameManager.instance.createNewPlayer(input.text, options_str[dropdown.value]);
    }
}
