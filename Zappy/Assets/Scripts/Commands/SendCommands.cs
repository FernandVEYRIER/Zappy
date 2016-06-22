using System.Reflection;
using System;

public class SendCommands : ACommands
{

    // Send msz command to server
    public override object msz(params object[] args)
    {
        return "msz\n";
    }

    // Send bct command to server
    public override object bct(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        return "bct " + args[0] + args[1] + "\n";
    }

    // Send pin command to server
    public override object pin(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "pin " + args[0] + "\n";
    }

    // Send plv command to server
    public override object plv(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "plv " + args[0] + "\n";
    }

    // Send ppo command to server
    public override object ppo(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "ppo " + args[0] + "\n";
    }

    // Send sgt command to server
    public override object sgt(params object[] args)
    {
        return "sgt\n";
    }

    // Send tna command to server
    public override object tna(params object[] args)
    {
        return "tna\n";
    }

    // Send mct command to server
    public object mct(params object[] args)
    {
        return "mct\n";
    }

    // Send sst command to server
    public object sst(params object[] args)
    {
        if (args.GetLength(0) < 1)
            return null;
        return "sst " + args[0] + "\n";
    }

    public override object pnw(params object[] args)
    {
        return null;
    }

    // Demande l'heure de démarrage du serveur
    public override object time(params object[] args)
    {
        return "time\n";
    }

    // Call commands with function name in string
    public object CallCommand(string arg, params object[] p)
    {
        cmd method;

        MethodInfo meth = GetType().GetMethod(arg, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (meth == null)
            return null;
        print(arg);
        method = (cmd)Delegate.CreateDelegate(typeof(cmd), this, meth);
        return method(p);
    }
}
