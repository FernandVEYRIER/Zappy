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
}
