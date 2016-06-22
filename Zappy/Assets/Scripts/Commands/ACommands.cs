using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

public abstract class ACommands : MonoBehaviour, ICommands {

    public delegate object cmd(object[] args);
    protected InfinitTerrain terrain;
    protected GameManager GM = null;
    protected Queue<string> cmds = new Queue<string>();

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PushQueue(string arg)
    {
        arg.Trim();
        string[] lines = arg.Split('\n');
        foreach (string line in lines)
        {
            cmds.Enqueue(line);
        }
    }
    // Call commands with reflection system (with function name)
    public object CallCommand(string arg)
    {
        cmd method;

        string[] args = arg.Split(' ');

        if (args.Length <= 0)
        {
            return null;
        }
        MethodInfo meth = GetType().GetMethod(args[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (meth == null)
            return null;
        method = (cmd)Delegate.CreateDelegate(typeof(cmd), this, meth);
        return method(args);
    }

    void Update()
    {
        if (cmds.Count != 0)
        {
            int limit = 5;
            int i = 0;
            while (i < limit && cmds.Count != 0)
            {
                CallCommand(cmds.Dequeue());
                ++i;
            }
        }
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
