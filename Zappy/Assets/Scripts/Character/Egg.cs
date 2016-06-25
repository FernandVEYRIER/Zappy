using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

    public GameObject character;
    public int _id;
    private int _id_player;
    private Vector3 _pos;
    private string _team;

    // Init Egg properties
    public void Init(int id, Character player)
    {
        _id = id;
        _id_player = player._id;
        _pos = player._pos;
        _team = player._team;
    }

    // Pond un oeuf
    public void hatch()
    {
        /*if (GameManager.instance)
        {
            GameObject player = GameManager.instance.addPlayer(_id_player, _pos, 1, 1, _team);
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }*/
		// TODO: changer la texture
    }

    // Call when server send edi commands
    public void die()
    {
        Destroy(gameObject);
    }
}
