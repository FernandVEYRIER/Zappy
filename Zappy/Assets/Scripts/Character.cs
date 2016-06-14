using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour {

    [System.Serializable]
    public class ItemInventory
    {
        public string   name;
        public int      count;
        public Sprite   sprite;
        public Color    color;
    }
    [System.Serializable]
    public class Talk
    {
        public SpriteRenderer renderer;
        public AudioSource sound;
        public float time = 0.5f;
    }
    public ItemInventory[] inventory;
    public float speed = 10;
    public Talk talkInfos;
    private int _level = 0;
    private string  _team;
    private int     _id;
    private int     _orientation;
    private Vector2 _pos;

    public int Level { get; set; }
    public string Team { get; set; }
    public int ID { get; set; }
    public int Orientation { get; set; }
    public Vector2 Pos { get; set; }

    public void Init(int id, int X, int Y, int orientation, int level, string team)
    {
        _id = id;
        _pos = new Vector2(X, Y);
        _orientation = orientation;
        _level = level;
        _team = team;
    }

    void Update()
    {
        if (_pos != (Vector2)transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pos, Time.deltaTime * speed);
        }
    }

    public void setPos(int x, int y, int orientation)
    {
        _pos = new Vector2(x, y);
        _orientation = orientation;
    }

    public void updateInventory(int x, int y, int[] items)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].count = items[i];
        }
    }

    public void talk(string message)
    {
        StartCoroutine("talkCoroutine");
        talkInfos.sound.Play();
        print(message);
    }

    IEnumerator talkCoroutine()
    {
        talkInfos.renderer.enabled = true;
        yield return new WaitForSeconds(talkInfos.time);
        talkInfos.renderer.enabled = false;
    }
}
