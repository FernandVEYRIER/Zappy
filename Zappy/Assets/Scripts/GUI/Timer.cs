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
        time = (DateTime.Now - dtDateTime).TotalSeconds;
        init = true;
    }

    void Update () {

        if (init)
        {
            time += Time.deltaTime;

            TimeSpan t = TimeSpan.FromSeconds(time);
            //update the label value
            txt.text = "Time " + string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
    }
}
