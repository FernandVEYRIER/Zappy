using UnityEngine;
using System.Collections;

public class CameraScaling : MonoBehaviour {

	[SerializeField] private LayerMask layer;

	private Camera cam;
	private Vector3[] dirs = new Vector3[4];
	private int count = 0;
    private InfinitTerrain terrain;

	void Awake()
	{
		cam = Camera.main;

		dirs [0] = new Vector3 (0, 0, 0);
		dirs [1] = new Vector3 (Screen.width, 0, 0);
		dirs [2] = new Vector3 (0, Screen.height, 0);
		dirs [3] = new Vector3 (Screen.width, Screen.height, 0);
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<InfinitTerrain>();
	}

	void Update ()
	{
        if (terrain.status)
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
                cam.transform.Translate(Vector3.forward * 0.05f);
            }
        }
		
	}
}
