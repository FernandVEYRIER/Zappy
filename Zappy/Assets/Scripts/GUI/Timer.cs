using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

    private Text txt;
    private float time;
    private bool init = false;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "";
    }

    // Init time with server time
    public void Init(string t)
    {
        time = Convert.ToSingle(t);
        init = true;
    }

    void Update () {

        if (init)
        {
            time += Time.deltaTime;
            var minutes = time / 60;
            var seconds = time % 60;

            //update the label value
            txt.text = "Time " + string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}
