using UnityEngine;
using System.Collections;

public class CartTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<Cart>().OnTriggerEnter2D(collision);
    }
}
