using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class SendCommands : MonoBehaviour
{
    private InfinitTerrain terrain;

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
    }

    public void CallCommand(string arg)
    {
        arg.Trim();

        string[] args = arg.Split(' ');

        if (args.Length <= 0)
        {
            return;
        }

        MethodInfo meth = this.GetType().GetMethod(args[0]);

        if (meth == null)
            return;

        meth.Invoke(this, args);
    }

    void msz(string[] args)
    {
        if (args.Length != 2)
            return;
        terrain.initMap(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
    }

    void bct(string[] args)
    {
        if (args.Length != 9)
            return;
        MapBlock block = new MapBlock(
            Convert.ToInt32(args[2]),
            Convert.ToInt32(args[3]),
            Convert.ToInt32(args[4]),
            Convert.ToInt32(args[5]),
            Convert.ToInt32(args[6]),
            Convert.ToInt32(args[7]),
            Convert.ToInt32(args[8])
            );
        terrain.setBlock(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]), ref block);
    }

    void tna(string[] args)
    {

    }

    void pnw(string[] args)
    {

    }

    void ppo(string[] args)
    {

    }

    void plv(string[] args)
    {

    }

    void pin(string[] args)
    {

    }

    void pex(string[] args)
    {

    }

    void pbc(string[] args)
    { }
    void pic(string[] args)
    { }
    void pie(string[] args)
    { }
    void pfk(string[] args)
    { }
    void pdr(string[] args)
    { }
    void pgt(string[] args)
    { }
    void pdi(string[] args)
    { }
    void enw(string[] args)
    { }
    void eht(string[] args)
    { }
    void ebo(string[] args)
    { }
    void edi(string[] args)
    { }
    void sgt(string[] args)
    { }
    void seg(string[] args)
    { }
    void smg(string[] args)
    { }
    void suc(string[] args)
    { }
    void sbp(string[] args)
    { }
}
