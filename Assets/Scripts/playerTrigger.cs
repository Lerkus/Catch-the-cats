using UnityEngine;
using System.Collections;

public class playerTrigger : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<Player>().OnTriggerEnter2D(collision);
    }
}
