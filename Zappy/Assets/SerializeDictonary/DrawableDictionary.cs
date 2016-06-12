using UnityEngine;
using System.Collections;

public class DrawableDictionary<TKey, TValue> : MonoBehaviour {

    public SerializableDictionary<TKey, TValue> dictionary = new SerializableDictionary<TKey, TValue>();
}
