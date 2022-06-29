using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject inforObj;
    private bool informa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(informa == false)
            {
                Application.Quit();
            }

            else
            {
                inforObj.SetActive(false);
                informa = false;
            }
            
        }
            
    }
    public void startGame()
    {
        informa = false;
        SceneManager.LoadScene(4);
    }
    public void info()
    {
        inforObj.SetActive(true);
        informa = true;
    }

}
