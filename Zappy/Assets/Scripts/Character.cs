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
    public ItemInventory[] inventory;
    //public DictionaryStringInt inventory = new DictionaryStringInt();
    //public Sprite[] textures;
    public int level = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
