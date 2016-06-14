using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class SendCommands : MonoBehaviour
{
    private InfinitTerrain terrain;
    public delegate void cmd(string[] args);
    private GameManager GM;

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Taille de la carte. 
    void msz(string[] args)
    {
        if (args.GetLength(0) < 3)
            return;
        terrain.initMap(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
    }

    // Contenu d’une case de la carte. 
    void bct(string[] args)
    {
        if (args.GetLength(0) < 9)
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

    // Nom des équipes.
    void tna(string[] args)
    {
        if (args.GetLength(0) < 2)
            return;
        GM.addTeam(args[1]);
    }

    // Connexion d’un nouveau joueur.
    void pnw(string[] args)
    {
        if (args.GetLength(0) < 7)
            return;
        Character player = new Character();
        player.Init(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), args[6]);
        GM.addPlayer(args[6], player);
    }

    // Position d’un joueur. 
    void ppo(string[] args)
    {
        if (args.GetLength(0) < 5)
            return;
        GM.getCharacter(Convert.ToInt32(args[1])).setPos(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), Convert.ToInt32(args[4]));
    }

    //Niveau d’un joueur.
    void plv(string[] args)
    {
        if (args.GetLength(0) < 3)
            return;
        GM.getCharacter(Convert.ToInt32(args[1])).Level = Convert.ToInt32(args[2]);
    }

    //Inventaire d’un joueur.
    void pin(string[] args)
    {
        if (args.GetLength(0) < 10)
            return;
        int[] inventory = new int[7] 
        {
            Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), Convert.ToInt32(args[5]),
            Convert.ToInt32(args[6]), Convert.ToInt32(args[7]), Convert.ToInt32(args[8]),
            Convert.ToInt32(args[9])
        };
        GM.getCharacter(Convert.ToInt32(args[1])).updateInventory(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), inventory);
    }


    //Un joueur expulse.
    void pex(string[] args)
    {
        if (args.GetLength(0) < 2)
            return;
        throw new  NotImplementedException();
    }

    //Un joueur fait un broadcast.
    void pbc(string[] args)
    {
        if (args.GetLength(0) < 3)
            return;
        GM.getCharacter(Convert.ToInt32(args[1])).talk(args[2]);
    }

    //Premier joueur lance l’incantation pour tous les suivants sur la case.
    void pic(string[] args)
    {
        if (args.GetLength(0) < 6)
            return;
        throw new NotImplementedException();
    }

    //Fin de l’incantation sur la case donnée avec le résultat R (0 ou 1).
    void pie(string[] args)
    {
        if (args.GetLength(0) < 4)
            return;
        throw new NotImplementedException();
    }


    //Le joueur pond un œuf.
    void pfk(string[] args)
    {
        if (args.GetLength(0) < 2)
            return;
        throw new NotImplementedException();
    }


    //Le joueur jette une ressource.
    void pdr(string[] args)
    {
        if (args.GetLength(0) < 3)
            return;
        throw new NotImplementedException();
    }

    //Le joueur prend une ressource.
    void pgt(string[] args)
    {
        if (args.GetLength(0) < 3)
            return;
        throw new NotImplementedException();
    }

    //Le joueur est mort de faim.
    void pdi(string[] args)
    {
        if (args.GetLength(0) < 2)
            return;
        throw new NotImplementedException();
    }


    //L’œuf a été pondu sur la case par le joueur.
    void enw(string[] args)
    {
        throw new NotImplementedException();
    }

    //L’œuf éclot.
    void eht(string[] args)
    {
        throw new NotImplementedException();
    }


    //Un joueur s’est connecté pour l’œuf.
    void ebo(string[] args)
    {
        throw new NotImplementedException();
    }


    //L’œuf éclos est mort de faim.
    void edi(string[] args)
    {
        throw new NotImplementedException();
    }


    //Demande de l’unité de temps courante sur le serveur. / Modification de l’unité de temps sur le serveur.
    void sgt(string[] args)
    {
        throw new NotImplementedException();
    }

    //Fin du jeu.L’équipe donnée remporte la partie.
    void seg(string[] args)
    {
        throw new NotImplementedException();
    }

    //Message du serveur.
    void smg(string[] args)
    {
        throw new NotImplementedException();
    }

    //Commande inconnue.
    void suc(string[] args)
    {
        throw new NotImplementedException();
    }


    //Mauvais paramètres pour la commande.
    void sbp(string[] args)
    {
        throw new NotImplementedException();
    }

    public void CallCommand(string arg)
    {
        cmd method;
        arg.Trim();
        string[] lines = arg.Split('\n');
        foreach (string line in lines)
        {
            string[] args = line.Split(' ');

            if (args.Length <= 0)
            {
                return;
            }
            MethodInfo meth = GetType().GetMethod(args[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (meth == null)
                return;
            method = (cmd)Delegate.CreateDelegate(typeof(cmd), this, meth);
            method(args);
        }
    }
}
