﻿using UnityEngine;
using System.Collections;

public class RIGHT : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && col.GetComponent<Animator>().GetBool("Move"))
        {
            print("RIGHT");
            Transform[] tmp = transform.parent.GetComponent<InfinitMove>().GetLeft();
            foreach (Transform tr in tmp)
            {
                if (tr.position.z == col.transform.position.z)
                {
                    col.transform.position = new Vector3(tr.position.x + 0.2f, 0, col.transform.position.z);
                }
            }
        }
    }
}
