﻿using UnityEngine;
using System.Collections;

public class LEFT : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && col.GetComponent<Animator>().GetBool("Move"))
        {
            Transform[] tmp = transform.parent.GetComponent<InfinitMove>().GetRight();
            foreach (Transform tr in tmp)
            {
                if (tr.position.x == col.transform.position.x)
                {
                    col.transform.position = new Vector3(tr.position.x - 0.2f, 0, col.transform.position.z);
                }
            }
        }
    }
}
