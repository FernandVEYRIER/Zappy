﻿using UnityEngine;
using System.Collections;

public class Turntable : MonoBehaviour {

    public Transform target;
    public float deg;
    public Vector3 axis = Vector3.up;

    void Start()
    {
        transform.LookAt(target);
    }

	void Update () {
        transform.RotateAround(target.position, axis, deg * Time.deltaTime);
	}
}
