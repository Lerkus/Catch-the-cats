using UnityEngine;
using System.Collections;

public class CatCounter : MonoBehaviour {

    public Cart data;
	
	void Update () {
        gameObject.GetComponent<GUIText>().text = data.amountCatsInCart + "/" + data.amountNeededCatsForWin;
	}
}
