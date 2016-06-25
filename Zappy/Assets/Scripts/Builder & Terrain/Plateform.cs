using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plateform : MonoBehaviour {

    public Vector2 size = Vector2.one;
    public GameObject[] prefab;
    public GameObject gridLine;
    public float sizeBlockX = 1;
    public float sizeBlockZ = 1;
    public Vector3 centerCollider;
    public Vector3 sizeCollider = Vector3.one;
    public bool grid = false;
    public bool collision = false;
    protected List<GameObject> blocks;

    // Method Build can be call from Editor or Script : Instanciate all GameObject's map
    public void Build()
    {
        float totalSizeX = size.x * sizeBlockX;
        float totalSizeZ = size.y * sizeBlockZ;
        float halfX = totalSizeX / 2 - sizeBlockX / 2;
        float halfZ = totalSizeZ / 2 - sizeBlockZ / 2;
        Vector3 origin = transform.position - new Vector3(halfX, 0, halfZ);
        blocks = new List<GameObject>();
        if (transform.childCount != 0)
            Delete();
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                GameObject tmp = prefab[Random.Range(0, prefab.Length)];
                tmp = (GameObject)Instantiate(tmp, origin + new Vector3(x * sizeBlockX, 0, z * sizeBlockZ), tmp.transform.rotation);
                if (tmp != null)
                    tmp.transform.parent = transform;
                blocks.Add(tmp);
            }
        }
        if (grid)
            createGrid(totalSizeX, totalSizeZ, origin);
        if (collision)
        {
            GetComponent<BoxCollider>().size = new Vector3(totalSizeX, sizeCollider.y, totalSizeZ);
            GetComponent<BoxCollider>().center = centerCollider;
        }
        else
            Destroy(GetComponent<BoxCollider>());
    }

    // Create a grid
    private void createGrid(float totalSizeX, float totalSizeZ, Vector3 origin)
    {
        GameObject line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(-totalSizeX / 2, sizeCollider.y, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, sizeCollider.y, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, sizeCollider.y, -totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, sizeCollider.y, -totalSizeZ / 2));
        line.transform.parent = transform;
        // CREATE BORDER
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(-totalSizeX / 2, 0, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, 0, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, 0, -totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, 0, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, 0, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(totalSizeX / 2, 0, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, 0, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, 0, totalSizeZ / 2));
        line.transform.parent = transform;

        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(-totalSizeX / 2, 0, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, sizeCollider.y, totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, 0, -totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(totalSizeX / 2, sizeCollider.y, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(-totalSizeX / 2, 0, -totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeX / 2, sizeCollider.y, -totalSizeZ / 2));
        line.transform.parent = transform;
        line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
        line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, 0, totalSizeZ / 2));
        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(totalSizeX / 2, sizeCollider.y, totalSizeZ / 2));
        line.transform.parent = transform;
        // END CREATE BORDER
        for (int x = 0; x < size.x; x++)
        {
            line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
            line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(origin.x + (sizeBlockX / 2) + x * sizeBlockX, sizeCollider.y, totalSizeZ / 2));
            line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(origin.x + (sizeBlockX / 2) + x * sizeBlockX, sizeCollider.y, -totalSizeZ / 2));
            line.transform.parent = transform;
        }
        for (int z = 0; z < size.y; z++)
        {
            line = (GameObject)Instantiate(gridLine, Vector3.zero, Quaternion.identity); ;
            line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(totalSizeX / 2, sizeCollider.y, origin.z + (sizeBlockZ / 2) + z * sizeBlockZ));
            line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-totalSizeZ / 2, sizeCollider.y, origin.z + (sizeBlockZ / 2) + z * sizeBlockZ));
            line.transform.parent = transform;
        }
    }

    // Delete all GameObject's map Children
    public void Delete()
    {
        
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        GetComponent<BoxCollider>().size = Vector3.one;
    }
}
