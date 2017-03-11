using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private string carriageType = null;
    public int amount;
    private int maxCats = 3;
    private int maxHeavy = 1;
    public float speedTweakerNormal = 1;
    public float speedTweakerCarting = 0.5f;
    private bool isCarting = false;

    public void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        if (!isCarting)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speedTweakerNormal * Input.GetAxis("Horizontal"), gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2( speedTweakerCarting * Input.GetAxis("Horizontal"), gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void load(GameObject toLoad)
    {
        if (carriageType == null)
        {
            carriageType = toLoad.tag;
            print("loaded: " + carriageType);
            amount = 1;
        }
        else
        {
            if (toLoad.tag == carriageType)
            {
                if (carriageType == "cat" && amount < maxCats)
                {
                    amount++;
                }
                else if (carriageType == "heavy" && amount < maxHeavy)
                {
                    amount++;
                }
            }
            else if (toLoad.tag == "heavy" && carriageType == "cat")
            {
                amount = 0;
                carriageType = null;
            }
        }
        Destroy(toLoad);
    }

    public void unload(GameObject toUnloadInto)
    {
        if (carriageType == "heavy")
        {
            print("unloading heavy");
            toUnloadInto.GetComponent<Cart>().block();
        }
        else if (carriageType == "cat")
        {
            print("unloading cats");
            toUnloadInto.GetComponent<Cart>().unload(amount);
        }
        else if (carriageType == null)
        {
            print("nothing to unload");
        }
        amount = 0;
        carriageType = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cart")
        {
            unload(collision.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "cat" || collision.collider.tag == "heavy")
        {
            print("Im loading some stuff.");
            load(collision.collider.gameObject);
        }
    }
}
