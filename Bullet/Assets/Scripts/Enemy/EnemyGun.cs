using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyBulletPrefab;

	// Use this for initialization
	void Start () {
        Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletPrefab);
            bullet.transform.position = transform.position;
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
        else
            Debug.Log("playerShip is null");
    }
}
