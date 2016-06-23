using UnityEngine;
using System;
using System.Collections.Generic;

public class ReceiveCommands : ACommands {

    // Taille de la carte. 
    public override object msz(params object[] args)
    {
        if (args.GetLength(0) < 3 || !GameManager.instance)
            return null;
        terrain.initMap(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
        GameManager.instance.StartGame();
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
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        GameManager.instance.addTeam((args[1] as string));
        return null;
    }

    // Connexion d’un nouveau joueur.
    public object pnw(params object[] args)
    {
        if (args.GetLength(0) < 7 || !GameManager.instance)
            return null;
        GameObject cubeMap = terrain.getMapPos(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]));
        GameObject player = GameManager.instance.addPlayer(Convert.ToInt32(args[1]), cubeMap.transform.position, Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), (args[6] as string));
        player.transform.SetParent(cubeMap.transform);
        return null;
    }

    // Position d’un joueur. 
    public override object ppo(params object[] args)
    {
        if (args.GetLength(0) < 5 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
        {
            GameObject cubeMap = terrain.getMapPos(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]));
            charac.setPos(cubeMap.transform.position, Convert.ToInt32(args[4]));
            charac.transform.SetParent(cubeMap.transform);
        }
        return null;
    }

    //Niveau d’un joueur.
    public override object plv(params object[] args)
    {
        if (args.GetLength(0) < 3 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac._level = Convert.ToInt32(args[2]);
        return null;
    }

    //Inventaire d’un joueur.
    public override object pin(params object[] args)
    {
        if (args.GetLength(0) < 10 ||  !GameManager.instance)
            return null;
        int[] inventory = new int[7]
        {
            Convert.ToInt32(args[4]), Convert.ToInt32(args[5]), Convert.ToInt32(args[5]),
            Convert.ToInt32(args[6]), Convert.ToInt32(args[7]), Convert.ToInt32(args[8]),
            Convert.ToInt32(args[9])
        };
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.updateInventory(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), inventory);
        return null;
    }

    // Récupère l'heure de démarrage du serveur
    public override object time(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        GameManager.instance.SetTimer(args[1].ToString());
        return null;
    }

    //Un joueur expulse.
    public object pex(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;

        return null;
    }

    //Un joueur fait un broadcast.
    public object pbc(params object[] args)
    {
        if (args.GetLength(0) < 3 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.talk((args[2] as string));
        return null;
    }

    //Premier joueur lance l’incantation pour tous les suivants sur la case.
    public object pic(params object[] args)
    {
        if (args.GetLength(0) < 6)
            return null;
		GameObject block = GameManager.instance.getMap ().getMapPos (Convert.ToInt32 (args [1]), Convert.ToInt32 (args [2]));
		if (block)
		{
			ParticleSystem.EmissionModule em = block.GetComponent<ParticleSystem> ().emission;
			em.enabled = true;
		}
		return null;
    }

    //Fin de l’incantation sur la case donnée avec le résultat R (0 ou 1).
    public object pie(params object[] args)
    {
        if (args.GetLength(0) < 4)
            return null;
		GameObject block = GameManager.instance.getMap ().getMapPos (Convert.ToInt32 (args [1]), Convert.ToInt32 (args [2]));
		if (block)
		{
			ParticleSystem.EmissionModule em = block.GetComponent<ParticleSystem> ().emission;
			em.enabled = false;
		}
		return null;
    }


    //Le joueur pond un œuf.
    public object pfk(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.lay = true;
        return null;
    }


    //Le joueur jette une ressource.
    public object pdr(params object[] args)
    {
        if (args.GetLength(0) < 3 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.throwResource(Convert.ToInt32(args[2]));
        return null;
    }

    //Le joueur prend une ressource.
    public object pgt(params object[] args)
    {
        if (args.GetLength(0) < 3 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.takeResource(Convert.ToInt32(args[2]));
        return null;
    }

    //Le joueur est mort de faim.
    public object pdi(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[1]));
        if (charac)
            charac.die();
        return null;
    }


    //L’œuf a été pondu sur la case par le joueur.
    public object enw(params object[] args)
    {
        if (args.GetLength(0) < 5 || !GameManager.instance)
            return null;
        Character charac = GameManager.instance.getCharacter(Convert.ToInt32(args[2]));
        if (charac)
            GameManager.instance.addEgg(Convert.ToInt32(args[1]), charac);
        return null;
    }

    //L’œuf éclot.
    public object eht(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        Egg egg = GameManager.instance.getEgg(Convert.ToInt32(args[1]));
        if (egg)
            egg.hatch();
        return null;
    }


    //Un joueur s’est connecté pour l’œuf.
    public object ebo(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
		Egg egg = GameManager.instance.getEgg (Convert.ToInt32 (args [1]));
		if (egg)
		{
			egg.die ();
		}
		return null;
    }


    //L’œuf éclos est mort de faim.
    public object edi(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        GameManager.instance.getEgg(Convert.ToInt32(args[1])).die();
        return null;
    }


    //Demande de l’unité de temps courante sur le serveur. / Modification de l’unité de temps sur le serveur.
    public override object sgt(params object[] args)
    {
        if (args.GetLength(0) < 2 || !GameManager.instance)
            return null;
        GameManager.instance.TimeScale = Convert.ToInt32(args[1]);
        return null;
    }

    //Fin du jeu.L’équipe donnée remporte la partie.
    public object seg(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
		GameManager.instance.DisplayVictoryScreen (args[1].ToString());
		return null;
	}

    //Message du serveur.
    public object smg(params object[] args)
    {
        if (args.GetLength(0) < 2)
            return null;
        if (GameManager.instance)
            GameManager.instance.WriteConsole("<color=orange>" + args[1].ToString() + "</color>");
        return null;
    }

    //Commande inconnue.
    public object suc(params object[] args)
    {
        if (GameManager.instance)
            GameManager.instance.WriteConsole("<color=red>(Unknow command !)</color>");
        return null;
    }


    //Mauvais paramètres pour la commande.
    public object sbp(params object[] args)
    {
        if (GameManager.instance)
            GameManager.instance.WriteConsole("<color=red>(Bad parameter !)</color>");
        return null;
    }

    void Update()
    {
        if (GameManager.instance && !GameManager.instance.Pause && cmds.Count != 0)
        {
            int i = 0;
            while (i < taskByFrame[QualitySettings.GetQualityLevel()] && cmds.Count != 0)
            {
                string tmp = cmds.Dequeue();
                CallCommand(tmp);
                    GameManager.instance.WriteConsole(tmp);
                ++i;
            }
        }
    }
}
