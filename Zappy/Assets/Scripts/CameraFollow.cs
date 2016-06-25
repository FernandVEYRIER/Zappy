using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private DisplayCharac displ;
    public float speed = 10;
    private bool free = true;
	// Update is called once per frame
	void Update () {
        if (!free && displ.character)
        {
            
        }
	}
}
