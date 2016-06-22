using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderUI : MonoBehaviour {

    public GameObject obj;

    // Show/Hide Slider
    public void Show(Slider slider)
    {
        obj.SetActive(slider.maxValue == slider.value);
    }
}
