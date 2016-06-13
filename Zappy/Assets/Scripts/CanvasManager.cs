using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    private GameObject open = null;
    public void Show(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
        if (obj.activeSelf)
            open = obj;
        else
            open = null;
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

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) ||  Input.GetAxis("Cancel") != 0) && open != null)
        {
            open.SetActive(false);
            open = null;
        }
    }
}
