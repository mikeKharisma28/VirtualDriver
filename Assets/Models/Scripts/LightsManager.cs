using UnityEngine;

public class LightsManager : MonoBehaviour {
    public GameObject[] brakeLights;
    public GameObject[] reverseLights;

    void Awake()
    {
        CheckComponentsIfAvailable();
    }
	// Use this for initialization
	void Start () {
        for (int i = 0; i < brakeLights.Length; i++)
        {
            brakeLights[i].GetComponent<Light>().intensity = 0.5f;
            brakeLights[i].GetComponent<LensFlare>().brightness = 0.1f;
        }

        for (int i = 0; i < reverseLights.Length; i++)
        {
            reverseLights[i].GetComponent<Light>().intensity = 0.2f;
            reverseLights[i].GetComponent<LensFlare>().brightness = 0.1f;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LightsManagerInput(float brake, int gearIndex)
    {
        CheckComponentsIfAvailable();
        brake = -1 * Mathf.Clamp(brake, -1f, 0);
        if (brake > 0)
        {
            for (int i = 0; i < brakeLights.Length; i++)
            {
                brakeLights[i].GetComponent<Light>().intensity = 1f;
                brakeLights[i].GetComponent<LensFlare>().brightness = 0.3f;
            }
        }
        else
        {
            for (int i = 0; i < brakeLights.Length; i++)
            {
                brakeLights[i].GetComponent<Light>().intensity = 0.5f;
                brakeLights[i].GetComponent<LensFlare>().brightness = 0.1f;
            }
        }

        if (gearIndex == -1)
        {
            for (int i = 0; i < reverseLights.Length; i++)
            {
                reverseLights[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < reverseLights.Length; i++)
            {
                reverseLights[i].SetActive(false);
            }
        }
    }

    private void CheckComponentsIfAvailable()
    {
        if (brakeLights == null)
            return;
        if (reverseLights == null)
            return;
    }
}
