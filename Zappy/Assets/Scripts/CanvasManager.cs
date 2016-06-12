using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {


    public void Show(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void FlipImage(RectTransform source)
    {
        source.localScale = new Vector3(-1 * source.localScale.x, 1, 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
