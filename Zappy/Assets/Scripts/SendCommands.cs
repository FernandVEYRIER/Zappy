using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class SendCommands : Commands, ICommands
{

    public object msz(params object[] args)
    {
        return "msz\n";
    }

    public object bct(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        return "bct " + args[0] + args[1] + "\n";
    }

    public object pin(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "pin " + args.GetLength(0) + "\n";
    }

    public object plv(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "plv " + args.GetLength(0) + "\n";
    }

    public object ppo(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "ppo " + args[0] + "\n";
    }

    public object sgt(params object[] args)
    {
        return "sgt\n";
    }

    public object tna(params object[] args)
    {
        return "tna\n";
    }

    public object mct(params object[] args)
    {
        return "mct\n";
    }

    public object sst(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "sst " + args.GetLength(0) + "\n";
    }

    public object pnw(params object[] args)
    {
        return null;
    }
}
