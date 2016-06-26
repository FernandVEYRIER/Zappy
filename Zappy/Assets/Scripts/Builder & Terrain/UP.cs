using UnityEngine;
using System.Collections;

public class UP : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && col.GetComponent<Animator>().GetBool("Move"))
        {
            print("UP");
            Transform[] tmp = transform.parent.GetComponent<InfinitMove>().GetDown();
            foreach (Transform tr in tmp)
            {
                if (tr.position.x == col.transform.position.x)
                {
                    col.transform.position = new Vector3(col.transform.position.x, 0, tr.position.z - 0.2f);
                }
            }
        }
    }
}
