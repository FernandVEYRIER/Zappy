using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class DictionaryStringInt : SerializableDictionary<string, int>
{
    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryIntString : SerializableDictionary<int, string>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryIntInt : SerializableDictionary<int, int>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryStringString : SerializableDictionary<string, string>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryStringVector2 : SerializableDictionary<string, Vector2>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryStringVector3 : SerializableDictionary<string, Vector3>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryIntVector2 : SerializableDictionary<int, Vector2>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

[Serializable]
public class DictionaryIntVector3 : SerializableDictionary<int, Vector3>
{

    public AbscractDictionary GetDico()
    {
        return this;
    }
}

