using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionOutAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TransitionOutAfterDelay()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("GameScene");

    }
}
