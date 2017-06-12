using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Enemy
{
    public class EnemyHandler : MonoBehaviour
    {

        [SerializeField]
        private Util.FloatRange xRange;
        [SerializeField]
        private float totalTimePer = 3f;
        [SerializeField]
        private float minY = -100f;

        [Header("Explosion")]
        [SerializeField]
        private GameObject explosion;
        [SerializeField]
        private float explosionOutTime = 0.5f;
        [SerializeField]
        private float explosionInTime = 0.5f;

        private Transform expTransform;
        private SpriteRenderer expSR;
        private Vector2 orgScale;

        private float startTime = 0f;
        private float timeRatio = 0f;
        private float rangeDistance;

        private new Transform transform;
        private SpriteRenderer SR;
        private new Collider2D collider;

        // Use this for initialization
        void Start()
        {
            startTime = Time.time;
            rangeDistance = Mathf.Abs(xRange.max - xRange.min);
            transform = GetComponent<Transform>();
            SR = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
            expTransform = explosion.GetComponent<Transform>();
            expSR = explosion.GetComponent<SpriteRenderer>();
            orgScale = expTransform.localScale;
            explosion.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (timeRatio < 1f)
            {
                timeRatio = (Time.time - startTime) / totalTimePer;
                if (timeRatio > 1) timeRatio = 1;
                float temp = (0.5f) * Mathf.Sin(timeRatio * Mathf.PI * 2) + (0.5f);
                transform.position = new Vector2(
                    xRange.min + temp * rangeDistance,
                    transform.position.y);

                if (timeRatio == 1)
                { // Clean up here
                    timeRatio = 0;
                    startTime = Time.time;
                }
            }

            if (transform.position.y < minY) Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Bullet")
            {
                Debug.Log("Bullet");
                StartCoroutine(Explosion());
            }
            if (collision.transform.tag == "Player")
            {
                Debug.Log("Player");
            }
        }

        private IEnumerator Explosion()
        {
            // scale to original (1) from 0
            explosion.SetActive(true);
            collider.enabled = false;
            float startTime = Time.time;
            float timeRatio = 0f;
            bool inOut = true; // true for out, false for in
            while (timeRatio < 1)
            {
                timeRatio = 
                    (Time.time - startTime) / (inOut ? explosionOutTime : explosionInTime);
                if (timeRatio > 1) timeRatio = 1;
                float temp = (inOut ? -1 : 1) * (0.5f) * Mathf.Cos(timeRatio * Mathf.PI) + (0.5f);
                expTransform.localScale = new Vector2(temp * orgScale.x, temp * orgScale.y);
                expSR.color = new Color(expSR.color.r, expSR.color.g, expSR.color.b, temp);
                if (!inOut) SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, temp);

                if (timeRatio == 1)
                { // Clean up here
                    if (inOut)
                    {
                        startTime = Time.time;
                        timeRatio = 0f;
                        inOut = false;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }

                yield return null;
            }
            // set org obj fade to 0
            // scale from 1 to 0.5 and fade from 1 to 0

        }
    }
}
