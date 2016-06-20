using UnityEngine;

public class DictionaryUnity : MonoBehaviour {

    public AbscractDictionary dico = new DictionaryIntInt();

    //private DictionaryIntInt IntInt = null;
    //private DictionaryIntString stringint = null;
    //private DictionaryIntVector2 IntVector2 = null;
    //private DictionaryIntVector3 IntVector3 = null;

    //private DictionaryStringInt StringInt = null;
    //private DictionaryStringString StringString = null;
    //private DictionaryStringVector2 StringVector2 = null;
    //private DictionaryStringVector3 StringVector3 = null;
    
    public int _key;
    public int _value;

    public AbscractDictionary createDictionary(int key, int value)
    {
        _key = key;
        _value = value;
        switch (key)
        {
            case 0:
                switch (value)
                {
                    case 0:
                        dico = new DictionaryIntInt();
                        break;
                    case 1:
                        dico = new DictionaryIntString();
                        break;
                    case 2:
                        dico = new DictionaryIntVector2();
                        break;
                    case 3:
                        dico = new DictionaryIntVector3();
                        break;
                    default:
                        dico = new DictionaryIntInt();
                        break;
                }
                break;
            case 1:
                switch (value)
                {
                    case 0:
                        dico = new DictionaryStringInt();
                        break;
                    case 1:
                        dico = new DictionaryStringString();
                        break;
                    case 2:
                        dico = new DictionaryStringVector2();
                        break;
                    case 3:
                        dico = new DictionaryStringVector3();
                        break;
                    default:
                        dico = new DictionaryIntInt();
                        break;
                }
                break;
            default:
                dico = new DictionaryIntInt();
                break;
        }
        return dico;
    } 
}
