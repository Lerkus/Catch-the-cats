using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatCounter : MonoBehaviour
{

    public Cart data;
    public Text display;

    void Update()
    {
        display.text = data.amountCatsInCart + "/" + data.amountNeededCatsForWin;
    }
}

