using UnityEngine;

public class InfinitTerrain : Plateform {

    private MapBlock[,] map = null;
    public bool status = false;
    private GameObject[] colliders = new GameObject[4];
    public GameObject prefab_test;

    void Start()
    {
        initMap(3, 3);
    }

    // Init Map and Create all cubeMap
    public void initMap(int X, int Y)
    {
        size = new Vector2(Y, X);
        map = new MapBlock[Y, X];
        Build();
        status = true;
		GetComponent<InfinitMove> ().StartTerrain (size);
		GetComponent<InfinitMove> ().Init ();
        Invoke("test", 1);
    }
    void test()
    {
        GameObject tmp = Instantiate(prefab_test, getMapPos(0, 0).transform.position, Quaternion.identity) as GameObject;
        tmp.transform.SetParent(getMapPos(0, 0).transform);
        tmp.GetComponent<Character>().setPos(getMapPos(2, 0).transform, 0);
    }

    public override void Build()
    {
        base.Build();
        colliders[0] = new GameObject();
        colliders[0].transform.SetParent(transform);
        colliders[0].AddComponent<BoxCollider>();
        colliders[0].GetComponent<BoxCollider>().isTrigger = true;
        colliders[0].GetComponent<BoxCollider>().size = new Vector3(totalSizeX, 0.5f, 0.5f);
        colliders[0].GetComponent<BoxCollider>().center = new Vector3(0, 0, totalSizeZ / 2 + 0.25f);
        colliders[0].AddComponent<UP>();

        colliders[1] = new GameObject();
        colliders[1].transform.SetParent(transform);
        colliders[1].AddComponent<BoxCollider>();
        colliders[1].GetComponent<BoxCollider>().isTrigger = true;
        colliders[1].GetComponent<BoxCollider>().size = new Vector3(totalSizeX, 0.5f, 0.5f);
        colliders[1].GetComponent<BoxCollider>().center = new Vector3(0, 0, -totalSizeZ / 2 - 0.25f -1);
        colliders[1].AddComponent<DOWN>();

        colliders[2] = new GameObject();
        colliders[2].transform.SetParent(transform);
        colliders[2].AddComponent<BoxCollider>();
        colliders[2].GetComponent<BoxCollider>().isTrigger = true;
        colliders[2].GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, totalSizeZ);
        colliders[2].GetComponent<BoxCollider>().center = new Vector3(totalSizeX / 2 + 0.25f, 0, 0);
        colliders[2].AddComponent<LEFT>();

        colliders[3] = new GameObject();
        colliders[3].transform.SetParent(transform);
        colliders[3].AddComponent<BoxCollider>();
        colliders[3].GetComponent<BoxCollider>().isTrigger = true;
        colliders[3].GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, totalSizeZ);
        colliders[3].GetComponent<BoxCollider>().center = new Vector3(-totalSizeX / 2 - 0.25f - 1, 0);
        colliders[3].AddComponent<RIGHT>();
    }

    // Set a block properties with this X and Y coord
    public void setBlock(int X, int Y, ref MapBlock block)
    {
        if (map == null)
            return;
        if (map.GetLength(0) <= Y || map.GetLength(1) <= X)
            return;
        map[Y, X] = block;
        Transform block_transform = blocks[X + Y * map.GetLength(1)].transform.GetChild(0);
        block_transform.GetChild(0).gameObject.SetActive(block.Food > 0);
        block_transform.GetChild(1).gameObject.SetActive(block.Lenemate > 0);
        block_transform.GetChild(2).gameObject.SetActive(block.Deraumere > 0);
        block_transform.GetChild(3).gameObject.SetActive(block.Sibur > 0);
        block_transform.GetChild(4).gameObject.SetActive(block.Mendiane > 0);
        block_transform.GetChild(5).gameObject.SetActive(block.Phiras > 0);
        block_transform.GetChild(6).gameObject.SetActive(block.Thystame > 0);
    }

    // Return the cubeMap GameObject at X Y
    public GameObject getMapPos(int x, int y)
    {
        return blocks[x + y * map.GetLength(1)];
    }

    public void DeleteOnGame()
    {

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        GetComponent<BoxCollider>().size = Vector3.one;
        Destroy(colliders[0]);
        Destroy(colliders[1]);
        Destroy(colliders[2]);
        Destroy(colliders[3]);
        status = false;
    }
}
