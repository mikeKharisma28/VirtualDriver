using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {
    public int level = 1;
    public float setTime = 5.0f;
    public Transform plane;
    public Transform text;
    
    float timer;
	// Use this for initialization
	void Start () {
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //cam.fieldOfView -= zoomSpeed;
        if (timer < setTime)
        {
            StartCoroutine(FadeTo(0.0f, setTime, text));
            StartCoroutine(FadeTo(0.0f, setTime, plane));
        }
        else if (timer > setTime)
            SceneManager.LoadScene((int)Instance.Levels.START_MENU);
	}

    IEnumerator FadeTo(float aValue, float aTime, Transform target)
    {
        float alpha = target.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            target.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}
