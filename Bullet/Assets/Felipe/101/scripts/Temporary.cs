using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temporary : MonoBehaviour
{
    public GameObject MenuIngame;

    bool isPaused = false;
    // Use this for initialization
    void Start()
    {

    }

    //destroy
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "_main" && !isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
            } else if (SceneManager.GetActiveScene().name == "_main" && isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            if (MenuIngame.activeInHierarchy)
            {
                if (SceneManager.GetActiveScene().name != "101")
                    Cursor.visible = false;
                MenuIngame.SetActive(false);
            }
            else
                MenuIngame.SetActive(true);

        }

    }
}
