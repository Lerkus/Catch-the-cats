using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "cat" || collision.collider.tag == "heavy")
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
