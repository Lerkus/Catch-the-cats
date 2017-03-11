using UnityEngine;
using System.Collections;

public class timeOut : MonoBehaviour {

    private Coroutine bla;

	void Start () {
        bla = StartCoroutine(da());
	}

    public IEnumerator da()
    {
        yield return new WaitForSeconds(15);
        gameObject.SetActive(false);
        StopCoroutine(bla);
    }
}
