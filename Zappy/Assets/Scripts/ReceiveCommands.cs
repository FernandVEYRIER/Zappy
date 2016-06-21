using UnityEngine;
using System;

public class ReceiveCommands : Commands, ICommands {


    // Taille de la carte. 
    public override object msz(params object[] args)
    {
        if (args.GetLength(0) < 3)
            return null;
        terrain.initMap(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
        return null;
    }

    // Contenu d’une case de la carte. 
    public override object bct(params object[] args)
    {
        if (args.GetLength(0) < 9)
            return null;
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
        return null;
    }

    // Nom des équipes.
    public override object tna(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        GM.addTeam((args[1] as string));
        return null;
    }

    // Connexion d’un nouveau joueur.
    public override object pnw(params object[] args)
    {
        if (args.GetLength(0) < 7)
            return null;
        GM.addPlayer(Convert.ToInt32(args[1]), terrain.getMapPos(Convert.ToInt32(args[2]), Convert.ToInt32(args[3])), Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), (args[6] as string));
        return null;
    }

    // Position d’un joueur. 
    public override object ppo(params object[] args)
    {
        if (args.GetLength(0) < 5)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.setPos(terrain.getMapPos(Convert.ToInt32(args[2]), Convert.ToInt32(args[3])), Convert.ToInt32(args[4]));
        return null;
    }

    //Niveau d’un joueur.
    public override object plv(params object[] args)
    {
        if (args.GetLength(0) < 3)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac._level = Convert.ToInt32(args[2]);
        return null;
    }

    //Inventaire d’un joueur.
    public override object pin(params object[] args)
    {
        if (args.GetLength(0) < 10)
            return null;
        int[] inventory = new int[7]
        {
            Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), Convert.ToInt32(args[5]),
            Convert.ToInt32(args[6]), Convert.ToInt32(args[7]), Convert.ToInt32(args[8]),
            Convert.ToInt32(args[9])
        };
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.updateInventory(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), inventory);
        return null;
    }


    //Un joueur expulse.
    public object pex(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        //throw new NotImplementedException();
		return null;
    }

    //Un joueur fait un broadcast.
    public object pbc(params object[] args)
    {
        if (args.GetLength(0) < 3)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.talk((args[2] as string));
        return null;
    }

    //Premier joueur lance l’incantation pour tous les suivants sur la case.
    public object pic(params object[] args)
    {
        if (args.GetLength(0) < 6)
            return null;
        print(args[0]);
        throw new NotImplementedException();
    }

    //Fin de l’incantation sur la case donnée avec le résultat R (0 ou 1).
    public object pie(params object[] args)
    {
        if (args.GetLength(0) < 4)
            return null;
        print(args[0]);
        throw new NotImplementedException();
    }


    //Le joueur pond un œuf.
    public object pfk(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.lay = true;
        return null;
    }


    //Le joueur jette une ressource.
    public object pdr(params object[] args)
    {
        if (args.GetLength(0) < 3)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.throwResource(Convert.ToInt32(args[2]));
        return null;
    }

    //Le joueur prend une ressource.
    public object pgt(params object[] args)
    {
        if (args.GetLength(0) < 3)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.takeResource(Convert.ToInt32(args[2]));
        return null;
    }

    //Le joueur est mort de faim.
    public object pdi(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.die();
        return null;
    }


    //L’œuf a été pondu sur la case par le joueur.
    public object enw(params object[] args)
    {
        if (args.GetLength(0) < 5)
            return null;
        Character charac = GM.getCharacter(Convert.ToInt32(args[2]));
        if (charac)
            GM.addEgg(Convert.ToInt32(args[1]), charac);
        return null;
    }

    //L’œuf éclot.
    public object eht(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        Egg egg = GM.getEgg(Convert.ToInt32(args[1]));
        if (egg)
            egg.hatch();
        return null;
    }


    //Un joueur s’est connecté pour l’œuf.
    public object ebo(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        print(args[0]);
        throw new NotImplementedException();
    }


    //L’œuf éclos est mort de faim.
    public object edi(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        GM.getEgg(Convert.ToInt32(args[1])).die();
        return null;
    }


    //Demande de l’unité de temps courante sur le serveur. / Modification de l’unité de temps sur le serveur.
    public override object sgt(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        GM.TimeScale = Convert.ToInt32(args[1]);
        return null;
    }

    //Fin du jeu.L’équipe donnée remporte la partie.
    public object seg(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        print(args[0]);
        throw new NotImplementedException();
    }

    //Message du serveur.
    public object smg(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        print(args[0]);
        throw new NotImplementedException();
    }

    //Commande inconnue.
    public object suc(params object[] args)
    {
        print(args[0]);
        throw new NotImplementedException();
    }


    //Mauvais paramètres pour la commande.
    public object sbp(params object[] args)
    {
        print(args[0]);
        throw new NotImplementedException();
    }
}
