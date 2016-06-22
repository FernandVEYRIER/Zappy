using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownGui : MonoBehaviour {

    private Dropdown dropdown;
    private bool init = false;
    [SerializeField]
    private Sprite sprite;
	// Use this for initialization
	void Start()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        dropdown = GetComponent<Dropdown>();
        string dir_path = Application.dataPath + "IA/";
        if (!Directory.Exists(dir_path))
            return;
        string[] fileEntries = Directory.GetFiles(dir_path);
        foreach (string fileName in fileEntries)
        {
            Dropdown.OptionData tmp = new Dropdown.OptionData(fileName, sprite);
            options.Add(tmp);
        }
        dropdown.AddOptions(options);
        init = true;
    }
}
