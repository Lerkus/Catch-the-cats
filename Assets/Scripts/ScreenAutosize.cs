using UnityEngine;
using System.Collections;

public class ScreenAutosize : MonoBehaviour
{
    public readonly static float baseWidth = 1500;
    public readonly static float baseHeight = 500;

    private float scaleX;
    private float scaleY;

    void Start()
    {
        print(Screen.height);
        print(Screen.width);
    }

    void Update()
    {

        scaleUpdate();
    }

    private void scaleUpdate()
    {
        scaleX = (float)Screen.width / (float)baseWidth;
        scaleY = (float)Screen.height / (float)baseHeight;

        print(new Vector3(scaleX, scaleY, 1));

        gameObject.transform.localScale = new Vector3(scaleX,scaleY,1);
    }

}
