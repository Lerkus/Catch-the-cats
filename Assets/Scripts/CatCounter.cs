using UnityEngine;
using System.Collections;

public class CatCounter : MonoBehaviour {

    public Cart data;
    public GUIText display;
	
	void Update () {
        display.text = data.amountCatsInCart + "/" + data.amountNeededCatsForWin;
	}
}
