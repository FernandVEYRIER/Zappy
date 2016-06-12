using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[Serializable]
 public class SerializableDictionary<TKey, TValue> : AbscractDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
 {
     [SerializeField]
     protected List<TKey> keys = new List<TKey>();

    [SerializeField]
    protected List<TValue> values = new List<TValue>();

    protected Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    public ICollection<TKey> Keys
    {
        get
        {
            return dictionary.Keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            return dictionary.Values;
        }
    }

    public int Count
    {
        get
        {
            return dictionary.Count;
        }
    }

    public bool IsReadOnly
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            return dictionary[key];
        }

        set
        {
            dictionary[key] = value;
        }
    }

    // save the dictionary to lists
    public void OnBeforeSerialize()
     {
         keys.Clear();
         values.Clear();
         foreach(KeyValuePair<TKey, TValue> pair in this)
         {
             keys.Add(pair.Key);
             values.Add(pair.Value);
         }
     }
     
     // load dictionary from lists
     public void OnAfterDeserialize()
     {
        dictionary.Clear();
 
         if(keys.Count != values.Count)
             throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        try
        {
            for (int i = 0; i < keys.Count; i++)
                dictionary.Add(keys[i], values[i]);
        }
        catch (ArgumentException)
        {
            Debug.LogWarning("An element with the same key already exists in the dictionary.");
            return;
        }
         
     }

    public bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }

    public bool Remove(TKey key)
    {
        return dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        dictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        dictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
    }

    public override AbscractDictionary getDico()
    {
        return this;
    }
}