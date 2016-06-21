using UnityEngine;
using System.Collections;

public class InfinitMove : MonoBehaviour {

    public Vector2 size = Vector2.one;
    public Vector3 cubeSize = Vector3.one;
    public float speed = 20;

    private float halfX;
    private float halfZ;

    private float totalX;
    private float totalZ;

    private Transform[] right;
    private float rightValue;

    private Transform[] left;
    private float leftValue;

    private Transform[] up;
    private float upValue;

    private Transform[] down;
    private float downValue;

    private Transform[] children;

    private bool init = false;

	public void StartTerrain(Vector2 s)
    {
		size = s;
		halfX = cubeSize.x / 2.0f;
		halfZ = cubeSize.z / 2.0f;
        totalX = size.x * cubeSize.x;
		totalZ = size.y * cubeSize.z;
		right = new Transform[(int)size.y];
		left = new Transform[(int)size.y];
		up = new Transform[(int)size.x];
		down = new Transform[(int)size.x];
		rightValue = totalX / 2.0f - halfX;
        leftValue = -rightValue;
		upValue = totalZ / 2.0f - halfZ;
        downValue = -upValue;
    }

    public void Init()
    {
        int r = 0;
        int l = 0;
        int u = 0;
        int d = 0;
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            if (children[i].position.x == rightValue)
            {
                right[r] = children[i];
                r++;
            }
            if (children[i].position.x == leftValue)
            {
                left[l] = children[i];
                l++;
            }
            if (children[i].position.z == upValue)
            {
                up[u] = children[i];
                u++;
            }
            if (children[i].position.z == downValue)
            {
                down[d] = children[i];
                d++;
            }
        }
        init = true;
    }

    void MoveChildrendVertical()
    {
        Vector3 move = Vector3.forward * (Input.GetAxis("Vertical") * speed * Time.deltaTime);
        bool change = false;
        float limitUp = 0;
        float limitDown = 0;
        foreach (Transform child in children)
            child.Translate(move);
        if (Input.GetAxis("Vertical") > 0)
        {
            for (int i = 0; i < up.Length; i++)
            {
                if (up[i].position.z > upValue)
                {
                    limitUp = up[i].position.z - cubeSize.z - 0.1f;
                    up[i].position = down[i].position - new Vector3(0, 0, cubeSize.z);
                    down[i] = up[i];
                    change = true;
                }
            }
            if (change)
            {
                int i = 0;
                foreach (Transform child in children)
                {
                    if (child.position.z >= limitUp)
                    {
                        up[i++] = child;
                        if (i == up.Length)
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < down.Length; i++)
            {
                float limZ = downValue + -cubeSize.z;
                if (down[i].position.z < limZ)
                {
                    limitDown = down[i].position.z + cubeSize.z + 0.1f;
                    down[i].position = up[i].position + new Vector3(0, 0, cubeSize.z);
                    up[i] = down[i];
                    change = true;
                }
            }
            if (change)
            {
                int i = 0;
                foreach (Transform child in children)
                {
                    if (child.position.z <= limitDown)
                    {
                        down[i++] = child;
                        if (i == down.Length)
                            break;
                    }
                }
            }
        }
        //debugBorder();
    }

    void MoveChildrendHorizontal()
    {
        Vector3 move = Vector3.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        bool change = false;
        float limitRight = 0;
        float limitLeft = 0;
        foreach (Transform child in children)
            child.Translate(move);
        if (Input.GetAxis("Horizontal") > 0)
        {
            for (int i = 0; i < right.Length; i++)
            {
                if (right[i].position.x > rightValue)
                {
                    limitRight = right[i].position.x - cubeSize.x - 0.1f;
                    right[i].position = left[i].position - new Vector3(cubeSize.x, 0, 0);
                    left[i] = right[i];
                    change = true;
                }
            }
            if (change)
            {
                int i = 0;
                foreach (Transform child in children)
                {
                    if (child.position.x >= limitRight)
                    {
                        right[i++] = child;
                        if (i == right.Length)
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < left.Length; i++)
            {
                float limX = leftValue + -cubeSize.x;
                if (left[i].position.x < limX)
                {
                    limitLeft = left[i].position.x + cubeSize.x + 0.1f;
                    left[i].position = right[i].position + new Vector3(cubeSize.x, 0, 0);
                    right[i] = left[i];
                    change = true;
                }
            }
            if (change)
            {
                int i = 0;
                foreach (Transform child in children)
                {
                    if (child.position.x <= limitLeft)
                    {
                        left[i++] = child;
                        if (i == left.Length)
                            break;
                    }
                }
            }
        }
        //debugBorder();
    }


    void debugBorder()
    {
        foreach (var child in children)
        {
            child.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        foreach (var item in right)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        foreach (var item in left)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    void Update()
    {
        if (init)
        {
			if (Input.GetAxis("Horizontal") != 0)
            {
                MoveChildrendHorizontal();
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                MoveChildrendVertical();
            }
        }
    }
}
