using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SliderUI : MonoBehaviour {

    public GameObject obj;
    private int[] times = null;
    public int server_time = 0;

    void Start()
    {
        GetComponent<Slider>().value = 2;
    }
    // Show/Hide Slider
    public void Show()
    {
        obj.SetActive(GetComponent<Slider>().maxValue == GetComponent<Slider>().value);
    }

    public int GetTime()
    {
        if (times == null)
            return -1;
        return times[Convert.ToInt32(GetComponent<Slider>().value)];
    }

    public void InitServerTime()
    {
        if (GameManager.instance && times == null)
        {
            int t = GameManager.instance.TimeScale;
            times = new int[] { Mathf.Clamp(t - 100, 0, t), Mathf.Clamp(t - 50, 0, t), t, t + 50, t + 100};
        }
    }

    public void OnChange()
    {
        GameManager.instance.SendServer(GameManager.CMD.sst, GetTime());
    }

    public void Reset()
    {
        times = null;
    }
}
