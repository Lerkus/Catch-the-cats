using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartClickl : MonoBehaviour {

    public GameObject catLoading;
    public GameObject butt;


	public void click()
    {
        catLoading.SetActive(true);
        butt.SetActive(true);
        SceneManager.LoadScene("main");
        gameObject.SetActive(false);
    }
}
