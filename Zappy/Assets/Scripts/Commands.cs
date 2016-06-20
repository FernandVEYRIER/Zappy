using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

public class Commands : MonoBehaviour {

    public delegate object cmd(object[] args);
    protected InfinitTerrain terrain;
    protected GameManager GM;

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public List<object> CallCommand(string arg)
    {
        List<object> ret = new List<object>();
        cmd method;
        arg.Trim();
        string[] lines = arg.Split('\n');
        foreach (string line in lines)
        {
            string[] args = line.Split(' ');

            if (args.Length <= 0)
            {
                return null;
            }
            MethodInfo meth = GetType().GetMethod(args[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (meth == null)
                return null;
            method = (cmd)Delegate.CreateDelegate(typeof(cmd), this, meth);
            ret.Add(method(args));
        }
        return ret;
    }
}
