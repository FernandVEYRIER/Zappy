using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

    public GameObject character;
    private GameManager GM;
    private int _id;
    private int _id_player;
    private Vector3 _pos;
    private string _team;
    private InfinitTerrain terrain;

    public int ID { get; set; }

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
    }

    public void Init(int id, Character player)
    {
        _id = id;
        _id_player = player.ID;
        _pos = player.Pos;
        _team = player.Team;
    }

    public void hatch()
    {
        GM.addPlayer(_id_player, _pos, 1, 1, _team);
    }

    public void die()
    {
        Destroy(gameObject);
    }
}
