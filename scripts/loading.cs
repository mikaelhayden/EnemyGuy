using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("carregar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator carregar()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
