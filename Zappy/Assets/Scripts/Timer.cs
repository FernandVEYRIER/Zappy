using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    private Text txt;
    private float time;
    void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update () {
        time += Time.deltaTime;

        var minutes = time / 60;
        var seconds = time % 60;

        //update the label value
        txt.text = "Time " + string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
