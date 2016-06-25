using UnityEngine;

public class InfinitTerrain : Plateform {

    private MapBlock[,] map = null;
    public bool status = false;

    // Init Map and Create all cubeMap
    public void initMap(int X, int Y)
    {
        size = new Vector2(Y, X);
        map = new MapBlock[Y, X];
        Build();
        status = true;

		GetComponent<InfinitMove> ().StartTerrain (size);
		GetComponent<InfinitMove> ().Init ();
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
        status = false;
    }
}
