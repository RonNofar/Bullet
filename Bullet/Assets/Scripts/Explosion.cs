using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float totalTime = 0.5f;
    float startTime = 0f;
    float timeRatio = 0f;

    new Transform transform;
    Vector2 orgScale;

    SpriteRenderer sprenderer;
    Color orgColor;

    public bool isColored = false;

    public Color[] colorArr = { Color.red, Color.yellow, Color.green, Color.blue };
    public Color chosenColor;

    private void Awake()
    {
        sprenderer = GetComponent<SpriteRenderer>();
        orgColor = sprenderer.color;
        orgColor.a = 1f;

        if (isColored)
        {
            chosenColor = colorArr[Random.Range(0, colorArr.Length)];
            orgColor = chosenColor;
        }
    }

	void Start () {
        transform = GetComponent<Transform>();
        orgScale = transform.localScale;
        startTime = Time.time;

        transform.localScale = Vector2.zero;
        sprenderer.color = orgColor;
    }
	
	void FixedUpdate () {
		if (timeRatio < 1f)
        {
            timeRatio = (Time.time - startTime) / totalTime;
            if (timeRatio > 1f) timeRatio = 1f;

            float t = -0.5f * Mathf.Cos(timeRatio * 2 * Mathf.PI) + 0.5f;
            transform.localScale = new Vector2(orgScale.x * t, orgScale.y * t);

            if (timeRatio == 1f)
            { // Clean up
                Destroy(gameObject);
            }
        }
	}
}
