using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ChangeCameraView : MonoBehaviour {
    public Camera MainCamera;
    public Camera RightBottomCamera;
    public Camera LeftBottomCamera;
    public Camera InteriorCamera;
    public Camera HoodCamera;
    public Camera BackCamera;

    Camera[] ThreeMainCamera = new Camera[3];
    BlurOptimized scriptBlur;

    int index = 0;

    bool isMainCam = true;
    public bool isChangeable = true;
	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        ThreeMainCamera[0] = MainCamera;
        ThreeMainCamera[1] = HoodCamera;
        ThreeMainCamera[2] = InteriorCamera;
        scriptBlur = ThreeMainCamera[index].GetComponent<BlurOptimized>();
        scriptBlur.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            scriptBlur = ThreeMainCamera[index].GetComponent<BlurOptimized>();
            if (!isChangeable)
            {
                isChangeable = true;
                scriptBlur.enabled = false;
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                ChangeMainCamera();
                isMainCam = true;
                isChangeable = false;
                scriptBlur.enabled = true;
            }
        }
        if (isChangeable)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                index++;
                if (index == 3)
                {
                    index = 0;
                    ThreeMainCamera[index].enabled = true;
                    ThreeMainCamera[2].enabled = false;
                }
                else
                {
                    ThreeMainCamera[index].enabled = true;
                    ThreeMainCamera[index - 1].enabled = false;
                }
            }

            if (Input.GetKey(KeyCode.S))
                ChangeBackCamera();
            else if (Input.GetKeyUp(KeyCode.S))
                ChangeMainCamera();

            if (Input.GetKey(KeyCode.D))
                ChangeRightBottomCamera();
            else if (Input.GetKeyUp(KeyCode.D))
                ChangeMainCamera();

            if (Input.GetKey(KeyCode.A))
                ChangeLeftBottomCamera();
            else if (Input.GetKeyUp(KeyCode.A))
                ChangeMainCamera();
        }


	}

    void ChangeBackCamera()
    {
        BackCamera.enabled = true;
        RightBottomCamera.enabled = false;
        LeftBottomCamera.enabled = false;
        ThreeMainCamera[index].enabled = false;
    }

    void ChangeRightBottomCamera()
    {
        RightBottomCamera.enabled = true;
        BackCamera.enabled = false;
        LeftBottomCamera.enabled = false;
        ThreeMainCamera[index].enabled = false;
    }

    void ChangeLeftBottomCamera()
    {
        LeftBottomCamera.enabled = true;
        BackCamera.enabled = false;
        RightBottomCamera.enabled = false;
        ThreeMainCamera[index].enabled = false;
    }

    void ChangeMainCamera()
    {
        ThreeMainCamera[index].enabled = true;
        BackCamera.enabled = false;
        RightBottomCamera.enabled = false;
        LeftBottomCamera.enabled = false;
    }
}
