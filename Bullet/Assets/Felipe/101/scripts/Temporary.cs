using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temporary : MonoBehaviour
{
    public GameObject MenuIngame;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MenuIngame.activeInHierarchy && SceneManager.GetActiveScene().name != "101")
            {
                MenuIngame.SetActive(false);
                Cursor.visible = false;

            }
            else
                MenuIngame.SetActive(true);

        }

    }
}
