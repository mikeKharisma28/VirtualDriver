using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityStandardAssets.ImageEffects;

public class StartMenuButtonsScripts : MonoBehaviour {
    string TransmissionSelected;
    string ControllerSelected;

    [SerializeField] Dropdown transmissionDropdown;
    [SerializeField] Dropdown controllerDropdown;

    [SerializeField] GameObject windowChangePlayer;
    [SerializeField] GameObject windowMain;
    [SerializeField] InputField newPlayerNameField;
    [SerializeField] Text currentPlayerNameField;
    [SerializeField] Text playerName;

    [SerializeField] Toggle audioToggle;

    void Start()
    {
        //Check whether the file exists, if not create a new one
        if (File.Exists(Application.dataPath + "/Resources/OptionValueHolder.txt"))
        {
            //do nothing
        }
        else
        {
            UpdateValueHolder();
        }
        
        if (File.Exists(Application.dataPath + "/Resources/NameValueHolder.txt"))
        {
            //do nothing
        }
        else
        {
            windowChangePlayer.SetActive(true);
            GameObject.Find("CancelButton").GetComponent<Button>().interactable = false;
            GameObject.Find("OKButton").GetComponent<Button>().interactable = false;
            windowMain.SetActive(false);
            GetComponent<BlurOptimized>().enabled = true;
            UpdatePlayersName();
        }

        Instance.isAudioOn = LoadAudioSetting();
        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;

        TransmissionSelected = LoadTransmissionChosenString();
        ControllerSelected = LoadControllerChosenString();
        currentPlayerNameField.text = LoadPlayerNameString();
        playerName.text = "Welcome, " + currentPlayerNameField.text + "!";
        newPlayerNameField.characterLimit = 15;

        transmissionDropdown.onValueChanged.AddListener(delegate { 
            TransmissionValueChanged(transmissionDropdown); 
        });

        controllerDropdown.onValueChanged.AddListener(delegate
        {
            ControllerValueChanged(controllerDropdown);
        });

        audioToggle.onValueChanged.AddListener(delegate
        {
            AudioValueChanged(audioToggle);
        });

        newPlayerNameField.onValueChanged.AddListener(delegate
        {
            NewPlayerFieldValueChanged(newPlayerNameField);
        });

        //Updating Transmission Setting on Start
        if (TransmissionSelected == "Manual")
            transmissionDropdown.value = 0;
        else if (TransmissionSelected == "Automatic")
            transmissionDropdown.value = 1;

        //Updating Controller Setting on Start
        if (ControllerSelected == "Keyboard")
            controllerDropdown.value = 0;
        else if (ControllerSelected == "RealSense")
            controllerDropdown.value = 1;

        //Updating Audio Setting on Start
        if (Instance.isAudioOn)
            audioToggle.isOn = true;
        else
            audioToggle.isOn = false;
    }

    void Update()
    {
        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;        
    }

    public void OnClickPlayNow()
    {
        SceneManager.LoadScene((int)Instance.Levels.SELECTION_MENU);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickBackToMenu()
    {
        UpdateValueHolder();
    }

    public void OnClickChangePlayerName()
    {
        UpdatePlayersName();
        newPlayerNameField.text = "";
    }

    public void OnClickCancelPlayerName()
    {
        newPlayerNameField.text = "";
    }

    public void OnClickButtonChangeName()
    {
        if (String.IsNullOrEmpty(newPlayerNameField.text))
            GameObject.Find("OKButton").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("OKButton").GetComponent<Button>().interactable = true;

        if (currentPlayerNameField.text != "=")
            GameObject.Find("CancelButton").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("CancelButton").GetComponent<Button>().interactable = false;
    }

    private void TransmissionValueChanged(Dropdown target)
    {
        switch (target.value)
        {
            case 0:
                TransmissionSelected = "Manual";
                break;
            case 1:
                TransmissionSelected = "Automatic";
                break;
        }
    }

    public void ControllerValueChanged(Dropdown target)
    {
        switch (target.value)
        {
            case 0:
                ControllerSelected = "Keyboard";
                break;
            case 1:
                ControllerSelected = "RealSense";
                break;
        }
    }

    public void AudioValueChanged(Toggle target)
    {
        switch (target.isOn)
        {
            case true:
                Instance.isAudioOn = true;
                break;
            case false:
                Instance.isAudioOn = false;
                break;
        }
    }

    private void NewPlayerFieldValueChanged(InputField newPlayerNameField)
    {
        if (windowChangePlayer.activeInHierarchy)
        {
            if (String.IsNullOrEmpty(newPlayerNameField.text))
                GameObject.Find("OKButton").GetComponent<Button>().interactable = false;
            else
                GameObject.Find("OKButton").GetComponent<Button>().interactable = true;
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

    private bool LoadAudioSetting()
    {
        bool output;
        string tempOutput;

        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/OptionValueHolder.txt");

        using (fileReader)
        {
            fileReader.ReadLine();
            fileReader.ReadLine();
            tempOutput = fileReader.ReadLine();
            if (tempOutput != null)
            {
                fileReader.Close();
            }
        }

        if (tempOutput == "On")
            output = true;
        else
            output = false;

        return output;
    }

    private string LoadPlayerNameString()
    {
        string output = "";

        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/NameValueHolder.txt");

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

    void UpdateValueHolder()
    {
        if (File.Exists(Application.dataPath + "/Resources/OptionValueHolder.txt"))
        {
            using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/OptionValueHolder.txt", false))
            {
                selectedPathString.WriteLine(TransmissionSelected);
                selectedPathString.WriteLine(ControllerSelected);
                if (Instance.isAudioOn)
                    selectedPathString.WriteLine("On");
                else
                    selectedPathString.WriteLine("Off");
            }
        }
        else
        {
            using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/OptionValueHolder.txt", false))
            {
                selectedPathString.WriteLine("Manual");
                selectedPathString.WriteLine("Keyboard");
                selectedPathString.WriteLine("On");
            }
        }
    }

    void UpdatePlayersName()
    {
        if (File.Exists(Application.dataPath + "/Resources/NameValueHolder.txt"))
        {
            using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/NameValueHolder.txt", false))
            {
                selectedPathString.WriteLine(newPlayerNameField.text);
                currentPlayerNameField.text = newPlayerNameField.text;
                playerName.text = "Welcome, " + newPlayerNameField.text + "!";
            }
        }
        else
        {
            using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/NameValueHolder.txt", false))
            {
                selectedPathString.WriteLine("-");
                currentPlayerNameField.text = "-";
            }
        }
    }
}
