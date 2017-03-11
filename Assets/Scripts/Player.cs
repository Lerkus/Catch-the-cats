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
    public GameObject[] CarringCats;
    public GameObject CarringAnvil;

    private bool canHoldCart = false;
    private bool holdsCart = false;

    public GameObject cart;

    public GameObject[] gameObjectsToSee;

    public void FixedUpdate()
    {
        updateCarHandler();
        movePlayer();
    }

    private void updateCarHandler()
    {
        if (canHoldCart && Input.GetAxis("Jump") > 0)
        {
            print("try to hold the cart");
            if (!holdsCart)
            {
                holdsCart = true;
                cart.transform.SetParent(gameObject.transform, true);
            }
        }
        else
        {
            if (holdsCart)
            {
                holdsCart = false;
                cart.transform.SetParent(null, true);
            }
        }
    }

    private void movePlayer()
    {
        if (!holdsCart)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speedTweakerNormal * Input.GetAxis("Horizontal"), gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speedTweakerCarting * Input.GetAxis("Horizontal"), gameObject.GetComponent<Rigidbody2D>().velocity.y);
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
        updateThingsInPlayer();
        Destroy(toLoad);
    }

    public void updateThingsInPlayer()
    {
        if(carriageType == "cat")
        {
            showCats();
        }
        if (carriageType == "heavy")
        {
            clearCats();
            showAnvil();
        }
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
            clearCats();
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

        if (collision.tag == "handle")
        {
            canHoldCart = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "handle")
        {
            canHoldCart = false;
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

    private void showCats()
    {
        for(int i = 0; i<=amount; i++)
        {
            print(i);
            CarringCats[i].SetActive(true);
        }
    }

    private void clearCats()
    {
        for (int i = 0; i < 3; i++)
        {
            CarringCats[i].SetActive(false);
        }
    }

    private void showAnvil()
    {
        CarringAnvil.SetActive(true);
    }

    private void clearAnvil()
    {
        CarringAnvil.SetActive(false);
    }
}
