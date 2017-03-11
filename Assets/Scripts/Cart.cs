﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cart : MonoBehaviour
{
    public int amountCatsInCart = 0;
    public int amountNeededCatsForWin = 5;
    public GameObject theCartAnvil;
    public GameObject[] theCats;

    public float speedTweaker = 2.5f;
    private bool blocked = false;
    private Coroutine timeUntilFreeAgain;
    private Coroutine timerToLoadAgain;

    public void FixedUpdate()
    {
        if (!blocked)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speedTweaker * amountCatsInCart / 10f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void unload(int amount)
    {
        amountCatsInCart += amount;
        if (amountCatsInCart > amountNeededCatsForWin)
        {
            amountCatsInCart = amountNeededCatsForWin;
        }
        updateCatsSittingInCar();
        if (amountCatsInCart == amountNeededCatsForWin)
        {
            youWin();
        }
    }

    private void updateCatsSittingInCar()
    {
        for(int i = 0; i < theCats.Length; i++)
        {
            theCats[i].SetActive(false);
        }

        for (int i = 0; i < amountCatsInCart; i++)
        {
            theCats[i].SetActive(true);
        }
    }

    private void youWin()
    {
        print("Du hast die Kätzchen gerettet :3");
        timerToLoadAgain = StartCoroutine(nextTryLoadTimer());
    }

    private void youLoose()
    {
        print("Du hast die Kätzchen überfahren lassen o.O!");
        timerToLoadAgain = StartCoroutine(nextTryLoadTimer());
    }

    public IEnumerator nextTryLoadTimer()
    {
        GameObject.FindGameObjectWithTag("master").GetComponent<Gamesmaster>().shouldSpawn = false;
        yield return new WaitForSeconds(2);

        StopCoroutine(timeUntilFreeAgain);
        StopCoroutine(timerToLoadAgain);
        SceneManager.LoadScene("main");
    }

    public void block()
    {
        if (!blocked)
        {
            blocked = true;
            print("blocking");
            theCartAnvil.SetActive(true);
            timeUntilFreeAgain = StartCoroutine(timer());
        }
        else
        {
            StopCoroutine(timeUntilFreeAgain);
            timeUntilFreeAgain = StartCoroutine(timer());
        }
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(3);
        blocked = false;
        theCartAnvil.SetActive(false);
        StopCoroutine(timeUntilFreeAgain);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "death")
        {
            youLoose();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "cat")
        {
            unload(1);
            GameObject.Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "heavy")
        {
            if(amountCatsInCart % 4 == 0)
            {
                amountCatsInCart -= 4;
            }

            amountCatsInCart = amountCatsInCart - amountCatsInCart % 4;
            if (amountCatsInCart < 0)
            {
                amountCatsInCart = 0;
            }
            GameObject.Destroy(collision.collider.gameObject);
        }
        updateCatsSittingInCar();
    }
}
