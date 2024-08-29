using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class ConesCollision : MonoBehaviour {
    public RawImage[] Warnings;
    public static int warningCounter = 0;
    bool isHit = false;
    Color currentColor;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            Warnings[i] = GameObject.Find("Warning" + (i + 1).ToString()).GetComponent<RawImage>();
        }
        currentColor = Warnings[0].color;
	}

    void OnCollisionEnter(Collision col)
    {
        if (warningCounter >= 3 && !isHit)
        {
            Camera[] cameras = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].GetComponent<BlurOptimized>().enabled = true;
            }
            GameObject.Find("MainCamera").GetComponent<Camera>().GetComponent<BlurOptimized>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().Move(0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().ApplyBraking(-1, 0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<LightsManager>().LightsManagerInput(-1, GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().gearIndex);
        }
        else if (warningCounter < 3 && !isHit)
        {
            Warnings[warningCounter].color = currentColor + new Color(0, 0, 0, 1f);
            Debug.Log(Warnings[warningCounter].name + " is hit!");
        }
        if (!isHit)
            warningCounter++;
        isHit = true;
    }
}
