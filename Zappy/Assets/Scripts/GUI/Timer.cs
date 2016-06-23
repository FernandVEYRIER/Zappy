using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

    private Text txt;
    private double time;
    private bool init = false;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "";
    }

    // Init time with server time
    public void Init(string t)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(Convert.ToDouble(t));
        time = (DateTime.UtcNow - dtDateTime).TotalSeconds;
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
