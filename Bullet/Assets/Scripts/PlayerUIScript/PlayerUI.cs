using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerUI : MonoBehaviour {

    public GameObject GameOverOBJ;
    public GameObject[] hearts;
    public int life;
    public bool hit;
    private int MaxLifes;
    // Use this for initialization
    void Start () {
        GameOverOBJ.SetActive(false);
        MaxLifes = Bullet.PlayerMaster.Instance.MaxLifes;
        life = Bullet.PlayerMaster.Instance.Lifes;
        hit = false;
		for(int i = 0; i < 10; i++)
        {
            if (i < life && hearts[i].activeInHierarchy)
            {
                continue;
            }
            hearts[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (hit)
        {
            Hit();
        }
		
	}
    void Hit()
    {
        hit = false;
        life--;
        for (int i = 0; i < MaxLifes; i++)
        {
            if (i < life && hearts[i].activeInHierarchy)
            {
                continue;
            }
            else if (i >= life && !hearts[i].activeInHierarchy)
            {
                continue;
            }
            else
                hearts[i].SetActive(false);

        }
        if (life <= 0)
        {
                Destroy(GameObject.Find("Player"));
                GameOverOBJ.SetActive(true);
                Cursor.visible = true;
        }
    }
    public void ButtonExit()
    {
        SceneManager.LoadScene(2);
    }
}
