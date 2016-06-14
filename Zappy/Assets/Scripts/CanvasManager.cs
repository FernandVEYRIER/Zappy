using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour {

    public Button dialogButton;
    public Button openButton;
    public EventSystem eventSystem;
    private GameObject open = null;
    private AxisKeyDown axes;
    private bool rightOpen = false;

    void Start()
    {
        axes = GetComponent<AxisKeyDown>();
        axes.callbacks.Add("Cancel", OnCancel);
        axes.callbacks.Add("Start", OnStart);
    }

    public void Show(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
        if (obj.activeSelf)
            open = obj;
        else
        {
            eventSystem.SetSelectedGameObject(null);
            open = null;
        }
    }

    public void FlipImage(RectTransform source)
    {
        source.localScale = new Vector3(-1 * source.localScale.x, 1, 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowIf(GameObject obj, int v1, int v2)
    {
        obj.SetActive(v1 == v2);
    }

    public void Mute()
    {
        Camera.main.GetComponent<AudioListener>().enabled = !Camera.main.GetComponent<AudioListener>().enabled;
    }

    void OnCancel()
    {
        if (open == null)
        {
            dialogButton.onClick.Invoke();
        }
        else
        {
            open.SetActive(false);
            open = null;
        }
    }

    void OnStart()
    {
        openButton.onClick.Invoke();
    }

    public void selectEventSystem(GameObject obj)
    {
        if (open)
            eventSystem.SetSelectedGameObject(obj);
    }
}
