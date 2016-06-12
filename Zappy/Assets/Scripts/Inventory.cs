using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    [System.Serializable]
    public class SerializeDico
    {
        public string name;
        public int value;
    }
    public SerializeDico[] inventory;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
