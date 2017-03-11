using UnityEngine;
using System.Collections;

public class Windows : MonoBehaviour
{

    private Coroutine closingTimer;

    public void open()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if (closingTimer == null)
        {
            closingTimer = StartCoroutine(close());
        }
        else
        {
            StopCoroutine(closingTimer);
            closingTimer = StartCoroutine(close());
        }
    }

    public IEnumerator close()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        if (closingTimer != null)
        {
            StopCoroutine(closingTimer);
            closingTimer = null;
        }
    }
}
