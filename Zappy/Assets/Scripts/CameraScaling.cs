using UnityEngine;
using System.Collections;

public class CameraScaling : MonoBehaviour {

	[SerializeField] private LayerMask layer;

	private Camera cam;
	private Vector3[] dirs = new Vector3[4];
	private int count = 0;
    private InfinitTerrain terrain;
    private bool init = false;
    private float offsetX;
	void Awake()
	{
		cam = Camera.main;
        offsetX = 50;
        dirs [0] = new Vector3 (-offsetX, 0, 0);
		dirs [1] = new Vector3 (Screen.width + offsetX, 0, 0);
		dirs [2] = new Vector3 (-offsetX, Screen.height, 0);
		dirs [3] = new Vector3 (Screen.width + offsetX, Screen.height, 0);
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
	}

	void Update ()
	{
        if (terrain.status && !init)
        {
            for (int i = 0; count != 4 && i < 4; ++i)
            {
                RaycastHit rayHit;
                if (Physics.Raycast(cam.transform.position, Camera.main.ScreenPointToRay(dirs[i]).direction, out rayHit, layer))
                {
                    ++count;
                }
            }
            if (count != 4)
            {
                count = 0;
                cam.transform.Translate(Vector3.forward * 0.5f);
            }
        }
	}
}
