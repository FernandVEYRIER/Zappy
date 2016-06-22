using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

public abstract class ACommands : MonoBehaviour, ICommands {

    public delegate object cmd(object[] args);
    protected InfinitTerrain terrain;
    protected GameManager GM;
    protected CanvasManager CM;

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CM = GameObject.Find("CanvasGame").GetComponent<CanvasManager>();
    }

    // Call commands with reflection system (with function name)
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

    public abstract object msz(params object[] args);

    public abstract object bct(params object[] args);

    public abstract object tna(params object[] args);

    public abstract object pnw(params object[] args);

    public abstract object ppo(params object[] args);

    public abstract object plv(params object[] args);

    public abstract object pin(params object[] args);

    public abstract object sgt(params object[] args);

    public abstract object time(params object[] args);
}
