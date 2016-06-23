public interface ICommands {

    // Taille de la carte.
    object msz(params object[] args);

    // Contenu d’une case de la carte. 
    object bct(params object[] args);

    // Nom des équipes.
    object tna(params object[] args);

    // Position d’un joueur. 
    object ppo(params object[] args);

    // Niveau d’un joueur.
    object plv(params object[] args);

    // Inventaire d’un joueur.
    object pin(params object[] args);

    // Demande de l’unité de temps courante sur le serveur. / Modification de l’unité de temps sur le serveur.
    object sgt(params object[] args);

    // Demande la temps écoulé / Reçoit le temps écoulé depuis le lancement du serveur
    object time(params object[] args);
}
