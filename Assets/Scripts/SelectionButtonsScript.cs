using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectionButtonsScript : MonoBehaviour
{
    public GameObject[] ListOfCars;
    public Button Next;
    public Button Previous;
    string CarSelectedPath = "";
    void Start()
    {
        Previous.interactable = false;
        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
    }

    int index = 0;
    void Update()
    {
        switch (index)
        {
            case 0:
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    //do nothing
                }

                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    OnClickNext();
                }
                break;
            case 8:
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    //do nothing
                }

                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    OnClickPrevious();
                }
                break;
            default:
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    OnClickNext(); 
                }

                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    OnClickPrevious();
                }
                break;
        }
    }

    public void OnClickBackToStartMenu()
    {
        SceneManager.LoadScene((int)Instance.Levels.START_MENU);
    }

    public void OnClickFreeRoamMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.RACE_TRACKONE);
    }

    public void OnClickStraightRoadMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.STRAIGHT_ROAD);
    }

    public void OnClickHandlingEasyMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.HANDLING_EASY);
    }

    public void OnClickHandlingMediumMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.HANDLING_MEDIUM);
    }

    public void OnClickParkingTestMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.PARKING_TEST);
    }

    public void OnClickRepositioningMode()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.REPOSITIONING_TEST);
    }

    public void OnClickRace()
    {
        SaveCarSelected();

        SceneManager.LoadScene((int)Instance.Levels.RACE_TRACKTWO);
    }

    public void OnClickNext()
    {
        index++;
        ListOfCars[index - 1].SetActive(false);
        ListOfCars[index].SetActive(true);

        if (!Previous.interactable)
        {
            Previous.interactable = true;
        }

        if (index == ListOfCars.Length - 1)
        {
            Next.interactable = false;
        }
        else
        {
            Next.interactable = true;
        }
    }

    public void OnClickPrevious()
    {
        if (!Next.interactable)
        {
            Next.interactable = true;
        }

        index--;
        ListOfCars[index + 1].SetActive(false);
        ListOfCars[index].SetActive(true);

        if (index == 0)
        {
            Previous.interactable = false;
        }
        else
        {
            Previous.interactable = true;
        }
    }

    void SaveCarSelected()
    {
        switch (index)
        {
            case 0:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/BRZ/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/BRZ/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/BRZ/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/BRZ/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 1:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Polo/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Polo/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Polo/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Polo/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 2:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Focus/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Focus/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Focus/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Focus/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 3:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/RS4/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/RS4/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/RS4/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/RS4/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 4:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/S60/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/S60/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/S60/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/S60/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 5:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Vantage/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Vantage/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Vantage/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Vantage/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 6:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/M5/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/M5/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/M5/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/M5/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 7:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/F12/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/F12/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/F12/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/F12/Prefabs/PlayerRealSense";
                    }
                }
                break;
            case 8:
                if (LoadTransmissionChosenString() == "Manual")
                {
                    Powertrain.isAutomatic = false;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Aventador/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Aventador/Prefabs/PlayerRealSense";
                    }
                }
                else if (LoadTransmissionChosenString() == "Automatic")
                {
                    Powertrain.isAutomatic = true;
                    if (LoadControllerChosenString() == "Keyboard")
                    {
                        CarSelectedPath = "Cars/Aventador/Prefabs/PlayerKeyboard";
                    }
                    else if (LoadControllerChosenString() == "RealSense")
                    {
                        CarSelectedPath = "Cars/Aventador/Prefabs/PlayerRealSense";
                    }
                }
                break;
        }

        using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/ValueHolder.txt", false))
        {
            selectedPathString.WriteLine(CarSelectedPath);
        }
    }

    private string LoadTransmissionChosenString()
    {
        string output = "";

        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/OptionValueHolder.txt");
        using (fileReader)
        {
            output = fileReader.ReadLine();
            if (output != null)
            {
                fileReader.Close();
            }
        }

        return output;
    }

    private string LoadControllerChosenString()
    {
        string output = "";

        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/OptionValueHolder.txt");
        using (fileReader)
        {
            fileReader.ReadLine();
            output = fileReader.ReadLine();
            if (output != null)
            {
                fileReader.Close();
            }
        }
        return output;
    }
}
