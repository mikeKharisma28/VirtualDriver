using UnityEngine;
using UnityEngine.UI;

public class CarProperties : MonoBehaviour 
{
    public string CarName = "";
    public string CarDriveType = "";
    public float CarPower = 0.0f;
    public float CarHandling = 0.0f;
    public float CarBraking = 0.0f;
    public float CarWeight = 0.0f;

    public Text NameLabel;
    public Text DriveLabel;
    public RawImage PowerBar;
    public RawImage HandlingBar;
    public RawImage BrakingBar;
    public RawImage WeightBar;

	// Use this for initialization
	void Start () 
    {
        NameLabel = GameObject.Find("CarName").GetComponent<Text>();
        DriveLabel = GameObject.Find("CarDriveType").GetComponent<Text>();
        PowerBar = GameObject.Find("PowerBarFilled").GetComponent<RawImage>();
        HandlingBar = GameObject.Find("HandlingBarFilled").GetComponent<RawImage>();
        BrakingBar = GameObject.Find("BrakingBarFilled").GetComponent<RawImage>();
        WeightBar = GameObject.Find("WeightBarFilled").GetComponent<RawImage>();
        NameLabel.text = CarName;
        DriveLabel.text = CarDriveType;

        PowerBar.rectTransform.sizeDelta = new Vector2(0.35f * CarPower, PowerBar.rectTransform.sizeDelta.y);
        HandlingBar.rectTransform.sizeDelta = new Vector2(0.35f * CarHandling, HandlingBar.rectTransform.sizeDelta.y);
        BrakingBar.rectTransform.sizeDelta = new Vector2(350 - (CarBraking * 5.5f), BrakingBar.rectTransform.sizeDelta.y);
        WeightBar.rectTransform.sizeDelta = new Vector2((1f / 6f) * CarWeight, WeightBar.rectTransform.sizeDelta.y);
	}

    void Update ()
    {
        NameLabel.text = CarName;
        DriveLabel.text = CarDriveType;
        PowerBar.rectTransform.sizeDelta = new Vector2(0.35f * CarPower, PowerBar.rectTransform.sizeDelta.y);
        HandlingBar.rectTransform.sizeDelta = new Vector2(0.35f * CarHandling, HandlingBar.rectTransform.sizeDelta.y);
        BrakingBar.rectTransform.sizeDelta = new Vector2(350 - (CarBraking * 5.5f), BrakingBar.rectTransform.sizeDelta.y);
        WeightBar.rectTransform.sizeDelta = new Vector2((1f / 6f) * CarWeight, WeightBar.rectTransform.sizeDelta.y);
    }
}
