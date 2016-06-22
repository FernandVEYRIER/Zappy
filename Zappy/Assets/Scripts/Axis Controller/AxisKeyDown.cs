using UnityEngine;
using System.Collections.Generic;
using System;

public class AxisKeyDown : MonoBehaviour {

    public DictionaryStringBool axes = new DictionaryStringBool()
    {
        { "Fire1", false },
        { "Fire2", false },
        { "Fire3", false },
        { "Jump", false },
        { "Submit", false },
        { "Cancel", false }
    };
    public Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
    private List<string> keys;
    private List<bool> call;

    //private bool isUpdate = false;
    void Start()
    {
        keys = new List<string>(axes.Keys);
    }

	// Update is called once per frame
	void Update ()
    {
        int i = 0;
        foreach (string axis in keys)
        {
            if (Input.GetAxisRaw(axis) != 0)
            {
                if (axes[axis] == false)
                {
                    if (callbacks.ContainsKey(axis))
                        callbacks[axis]();
                    axes[axis] = true;
                }
            }
            if (Input.GetAxisRaw(axis) == 0)
            {
                axes[axis] = false;
            }
            ++i;
        }
	}
}
