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
		public AudioClip[] soundClips;
        public float time = 0.5f;
    }
    public ItemInventory[] inventory;
    public Material[] levels;
    public float speed = 10;
    public Talk talkInfos;
    private int level = 0;
    public string  _team;
    public int     _id;
    public int     _orientation;
    public Vector3 _pos;
    public bool _isUpdate = false;
    public bool lay = true;
    public GameObject upFx;
    private float offsetY = 0;
    //private Transform tr = null;
    //private bool target = false;

    private Animator animator;

	private bool isYelling = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpLevel(int lvl)
    {
        level = lvl;
        upFx.SetActive(true);
    }

    public int GetLevel()
    {
        return level;
    }

    // Init Character's properties
    public void Init(int id, float X, float Y, int orientation, int lvl, string team)
    {
        _id = id;
        _pos = new Vector3(X, offsetY, Y);
        _orientation = orientation;
        level = lvl;
        _team = team;
        transform.rotation = getOrientation();
        if (GameManager.instance)
            GameManager.instance.SendServer(GameManager.CMD.pin, _id);
        if (level > 0)
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = levels[level - 1];
    }

    void Update()
    {

        //if (target && tr && Vector3.Distance(tr.position, transform.position) > 0.2f)
        //{
        //    if (GameManager.instance)
        //        transform.Translate(Vector3.forward * Time.deltaTime * speed * GameManager.instance.TimeScale * 0.1f);
        //    else
        //        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //    animator.SetBool("Move", true);
        //}
        //else
        //{
        //    if (animator.GetBool("Move"))
        //    {
        //        transform.position = tr.position;
        //        transform.SetParent(tr);
        //    }
        //    animator.SetBool("Move", false);
        //    target = false;
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
    public void setPos(Transform pos, int orientation)
    {
        _pos = new Vector3(pos.position.x, offsetY, pos.position.z);
        _orientation = orientation;
        //tr = pos;
        //target = true;
        transform.SetParent(pos);
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
        _isUpdate = false;
    }

    // Call when server send a broadcast command
    public void talk(string message)
    {
        StartCoroutine("talkCoroutine");
		//talkInfos.sound.clip = talkInfos.soundClips [Random.Range (0, talkInfos.soundClips.Length - 1)];
		//talkInfos.sound.Play ();
		//GetComponent<AudioSource> ().PlayOneShot (talkInfos.soundClips [Random.Range (0, talkInfos.soundClips.Length - 1)]);
		if (!isYelling)
			StartCoroutine (SoundCoroutine (talkInfos.soundClips [Random.Range (0, talkInfos.soundClips.Length - 1)]));
    }

    // Display bubble while talkInfos time
    IEnumerator talkCoroutine()
    {
        talkInfos.renderer.enabled = true;
        yield return new WaitForSeconds(talkInfos.time);
        talkInfos.renderer.enabled = false;
    }

	IEnumerator SoundCoroutine(AudioClip clip)
	{
		isYelling = true;
		GetComponent<AudioSource> ().PlayOneShot (clip);

		yield return new WaitForSeconds (clip.length);
		isYelling = false;
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

	public void SetLevel(int _level)
	{
		level = _level;
        if (level > 0)
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = levels[level - 1];
        _isUpdate = false;
	}
}
