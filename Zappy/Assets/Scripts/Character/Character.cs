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
    public int _level = 0;
    public string  _team;
    public int     _id;
    public int     _orientation;
    public Vector3 _pos;
    public bool _isUpdate = false;
    public bool lay = true;
    private float offsetY = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Init Character's properties
    public void Init(int id, float X, float Y, int orientation, int level, string team)
    {
        _id = id;
        _pos = new Vector3(X, offsetY, Y);
        _orientation = orientation;
        _level = level;
        _team = team;
        transform.rotation = getOrientation();
        if (GameManager.instance)
            GameManager.instance.SendServer(GameManager.CMD.pin, _id);
    }

    void Update()
    {
        //if (_pos != transform.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _pos, Time.deltaTime * speed);
        //    animator.SetBool("Move", true);
        //}
        //else
        //{
        //    animator.SetBool("Move", false);
        //}
    }

    // Get the Quaternion from _orientation
    private Quaternion getOrientation()
    {
        switch (_orientation)
        {
            case 1:
                return Quaternion.Euler(new Vector3(0, -90, 0));
            case 2:
                return Quaternion.Euler(new Vector3(0, 0, 0));
            case 3:
                return Quaternion.Euler(new Vector3(0, 90, 0));
            case 4:
                return Quaternion.Euler(new Vector3(0, 180, 0));
            default:
                break;
        }
        return Quaternion.Euler(Vector3.forward);
    }

    // Set current position of character
    public void setPos(Vector3 pos, int orientation)
    {
        _pos = new Vector3(pos.x, offsetY, pos.z);
        _orientation = orientation;
        transform.position = _pos;
        transform.rotation = getOrientation();
    }

    // Update Inventory of Character
    public void updateInventory(int x, int y, int[] items)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].count = items[i];
        }
        _isUpdate = true;
    }

    // Call when server send a broadcast command
    public void talk(string message)
    {
        StartCoroutine("talkCoroutine");
        talkInfos.sound.Play();
    }

    // Display bubble while talkInfos time
    IEnumerator talkCoroutine()
    {
        talkInfos.renderer.enabled = true;
        yield return new WaitForSeconds(talkInfos.time);
        talkInfos.renderer.enabled = false;
    }

    // Call when server send die command
    public void die()
    {
        animator.SetBool("Die", true);
    }

    // Destroy this GameObject
    public void destroyPlayer()
    {
        if (GameManager.instance)
            GameManager.instance.removeCharacter(this);
        Destroy(gameObject);
    }

    // Call when server send pgt
    public void takeResource(int index)
    {
        ++inventory[index].count;
        _isUpdate = false;
    }

    // Call when server send pdr
    public void throwResource(int index)
    {
        if (inventory[index].count > 0)
        {
            --inventory[index].count;
            _isUpdate = false;
        }
    }

    // Demand an Update from server
    public void updateInventoryInfos()
    {
        if (GameManager.instance)
            GameManager.instance.SendServer(GameManager.CMD.pin, _id);
    }

	public void SetLevel(int level)
	{
		_level = level;
		_isUpdate = false;
	}
}
