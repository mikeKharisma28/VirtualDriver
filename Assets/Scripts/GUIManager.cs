using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public RenderTexture minimapTexture;
    public Material minimapMaterial;
    public RawImage steeringWheel;

    private float offset;
	// Use this for initialization
	void Awake () {
        offset = 10;
	}

    void Start()
    {
        steeringWheel = GetComponent<RawImage>();
    }

	// Update is called once per frame
	void OnGUI () {
        if (Event.current.type == EventType.Repaint && !Checkpoints.isAccomplished)
            Graphics.DrawTexture(new Rect(0, 0, 256, 256), minimapTexture, minimapMaterial);
        else
        {
            //do nothing
        }
	}
}
