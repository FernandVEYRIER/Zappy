using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour {

    public Button dialogButton;
    public Button openButton;
    public EventSystem eventSystem;
    private GameObject open = null;
    private AxisKeyDown axes;
    public Timer timer;

    void Start()
    {
        axes = GetComponent<AxisKeyDown>();
        axes.callbacks.Add("Cancel", OnCancel);
        axes.callbacks.Add("Start", OnStart);
    }

    // Show/Hide a GameObject
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

    // Flip an Image 
    public void FlipImage(RectTransform source)
    {
        source.localScale = new Vector3(-1 * source.localScale.x, 1, 1);
    }

    // Quit game
    public void Quit()
    {
        Application.Quit();
    }

    // Show or Hide with a condition
    public void ShowIf(GameObject obj, int v1, int v2)
    {
        obj.SetActive(v1 == v2);
    }

    // Mute Game
    public void Mute()
    {
        Camera.main.GetComponent<AudioListener>().enabled = !Camera.main.GetComponent<AudioListener>().enabled;
    }

    // Function Call on cancel (for Controller)
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

    // Function Call on start (for Controller)
    void OnStart()
    {
        openButton.onClick.Invoke();
    }

    // Select object for an EventSystem
    public void selectEventSystem(GameObject obj)
    {
        if (open)
            eventSystem.SetSelectedGameObject(obj);
    }

    // Set the timer
    public void SetTimer(string time)
    {
        timer.Init(time);
    }
}
