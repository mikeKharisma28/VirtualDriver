using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.IO;

public class RaceTrackTwoSceneButtonsScript : MonoBehaviour {
    //This happens when a player hits the ESC button
    bool isPaused = false;
    bool tutorialIsShown = false;
    bool isButtonContinueClicked = false;
    static bool isStarted = false;

    public GameObject[] Tutorials;
    public GameObject onPauseWindow;
    public GameObject onPlayPanel;
    public GameObject onStopFinished;
    public GameObject tutorialWindow;
    public Text onPlayTimer;
    public Text onStopTimer;
    public Text CurrentLap;
    public Button nextTutorial;
    public Button previousTutorial;

    GameObject Player;
    BlurOptimized blur;

    // Use this for initialization
    void Awake () {
        Instance instance = new Instance();
        instance.ResetAllStaticValues();

        Player = Resources.Load(LoadPathString(), typeof(GameObject)) as GameObject;
        Player = Instantiate(Player);
        Player.transform.localPosition = new Vector3(890.5011f, 101.8027f, 857.5994f);
        Player.transform.Rotate(0f, 133.5f, 0f);

        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
    }

    void Start()
    {
        blur = GetComponent<BlurOptimized>();
        blur.enabled = false;
        previousTutorial.interactable = false;
        tutorialIsShown = true;
        isPaused = true;
        if (File.Exists(Application.dataPath + "/Resources/FastestTimeTrackTwoHolder.txt"))
        {
            //do nothing
        }
        else
        {
            using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/FastestTimeTrackTwoHolder.txt", false))
            {
                selectedPathString.WriteLine("null");
                selectedPathString.WriteLine("9999999999999999999");
            }
        }

        Player.GetComponent<CarController>().m_Rev = 1f;

        //==============================Showing the current record==========================================
        string currentRecordName = "";
        float currentRecordTime = 0.0f;
        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/FastestTimeTrackTwoHolder.txt");
        using (fileReader)
        {
            currentRecordName = fileReader.ReadLine();
            float.TryParse(fileReader.ReadLine(), out currentRecordTime);
            if (currentRecordName != null && currentRecordTime < 0)
            {
                fileReader.Close();
            }
        }

        if (currentRecordName == "null" || currentRecordTime == 9999999999999999999)
            GameObject.Find("CurrentRecord").GetComponent<Text>().text = "Best lap time = -";
        else
            GameObject.Find("CurrentRecord").GetComponent<Text>().text = "Best lap time = " + string.Format("{0:00} : {1:00} : {2:000}", Mathf.Floor(currentRecordTime / 60), Mathf.Floor(currentRecordTime) % 60, Mathf.Floor((currentRecordTime * 1000) % 1000)) + " (" + currentRecordName + ")";
        //===================================================================================================
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            switch (isPaused)
            {
                case true:
                    isPaused = false;
                    break;
                case false:
                    isPaused = true;
                    break;
            }
        }

        if (isPaused)
        {
            if (tutorialIsShown)
            {
                Time.timeScale = 0;
                blur.enabled = true;
                onPlayPanel.SetActive(false);
                onPauseWindow.SetActive(false);
                onStopFinished.SetActive(false);
                tutorialWindow.SetActive(true);
                GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
            }
            else
            {
                Time.timeScale = 0;
                onPauseWindow.SetActive(true);
                onPlayPanel.SetActive(false);
                onStopFinished.SetActive(false);
                GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
            }
        }
        else if (!isPaused)
        {
            if (tutorialIsShown)
            {
                //do nothing.
            }
            else
            {
                onPlayTimer.text = Timer();
                Instance instance = new Instance();
                if (Checkpoints.isAccomplished)
                {
                    Time.timeScale = 0.5f;
                    onPlayPanel.SetActive(false);
                    onPauseWindow.SetActive(false);
                    onStopFinished.SetActive(true);
                    instance.FindPlayerAndDisable();
                    GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
                }
                else
                {
                    Time.timeScale = 1;
                    onPauseWindow.SetActive(false);
                    onPlayPanel.SetActive(true);
                    onStopFinished.SetActive(false);
                    if (Instance.isAudioOn)
                        GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
                    else
                        GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
                }
            }

            if (CurrentLap.text == "1")
                isStarted = true;
            else if (Checkpoints.isAccomplished)
            {
                isStarted = false;
                onStopTimer.text = "Your time is " + Timer();
                StreamReader fileReaderName = new StreamReader(Application.dataPath + "/Resources/NameValueHolder.txt");
                string currentName = "";
                using (fileReaderName)
                {
                    currentName = fileReaderName.ReadLine();
                    if (currentName != null)
                    {
                        fileReaderName.Close();
                    }
                }
                if (isButtonContinueClicked)
                    GameObject.Find("YourTime").GetComponent<Text>().text = "Your time lap = " + Timer() + "(" + currentName + ")";
            }
        }
	}

    public void OnClickBackToGarage()
    {
        isPaused = false;
        isStarted = false;
        SceneManager.LoadScene((int)Instance.Levels.SELECTION_MENU);
    }

    public void OnClickRestart()
    {
        isPaused = false;
        isStarted = false;
        SceneManager.LoadScene((int)Instance.Levels.RACE_TRACKTWO);
    }

    public void OnClickContinue()
    {
        isButtonContinueClicked = true;
    }

    public void OnClickQuit()
    {
        isPaused = false;
        isStarted = false;
        SceneManager.LoadScene((int)Instance.Levels.START_MENU);
    }

    float time = 0;
    private string Timer()
    {
        if (isStarted)
        {
            time += Time.deltaTime;
            return string.Format("{0:00} : {1:00} : {2:000}", Mathf.Floor(time / 60), Mathf.Floor(time) % 60, Mathf.Floor((time * 1000) % 1000));
        }
        else
        {
            if (Checkpoints.isAccomplished)
            {
                string fastestName = "";
                float fastestTime = 0.0f;
                StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/FastestTimeTrackTwoHolder.txt");
                using (fileReader)
                {
                    fastestName = fileReader.ReadLine();
                    float.TryParse(fileReader.ReadLine(), out fastestTime);
                    if (fastestName != null && fastestTime < 0)
                    {
                        fileReader.Close();
                    }
                }

                //Compare current time lap to the fastest time lap, including player's name.
                if (time < fastestTime)
                {
                    string currentName = "";
                    StreamReader fileReaderName = new StreamReader(Application.dataPath + "/Resources/NameValueHolder.txt");
                    using (fileReaderName)
                    {
                        currentName = fileReaderName.ReadLine();
                        if (currentName != null)
                        {
                            fileReaderName.Close();
                        }
                    }

                    using (StreamWriter selectedPathString = new StreamWriter(Application.dataPath + "/Resources/FastestTimeTrackTwoHolder.txt", false))
                    {
                        selectedPathString.WriteLine(currentName);
                        selectedPathString.WriteLine(time);
                    }
                    if (isButtonContinueClicked)
                        GameObject.Find("FastestTime").GetComponent<Text>().text = "Best lap time = " + string.Format("{0:00} : {1:00} : {2:000}", Mathf.Floor(time / 60), Mathf.Floor(time) % 60, Mathf.Floor((time * 1000) % 1000)) + "(" + currentName + ")\n" + "New Record!";
                }
                else
                {
                    if (isButtonContinueClicked)
                        GameObject.Find("FastestTime").GetComponent<Text>().text = "Best lap time = " + string.Format("{0:00} : {1:00} : {2:000}", Mathf.Floor(fastestTime / 60), Mathf.Floor(fastestTime) % 60, Mathf.Floor((fastestTime * 1000) % 1000)) + "(" + fastestName + ")";
                }
                //====================================================================================================================================
            }
            return string.Format("{0:00} : {1:00} : {2:000}", Mathf.Floor(time / 60), Mathf.Floor(time) % 60, Mathf.Floor((time * 1000) % 1000));
        }
    }

    private string LoadPathString()
    {
        string output = "";

        StreamReader fileReader = new StreamReader(Application.dataPath + "/Resources/ValueHolder.txt");
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

    int index = 0;
    public void OnClickNext()
    {
        index++;
        Tutorials[index - 1].SetActive(false);
        Tutorials[index].SetActive(true);

        if (!previousTutorial.interactable)
        {
            previousTutorial.interactable = true;
        }

        if (index == Tutorials.Length - 1)
        {
            nextTutorial.interactable = false;
        }
        else
        {
            nextTutorial.interactable = true;
        }
    }

    public void OnClickPrevious()
    {
        if (!nextTutorial.interactable)
        {
            nextTutorial.interactable = true;
        }

        index--;
        Tutorials[index + 1].SetActive(false);
        Tutorials[index].SetActive(true);

        if (index == 0)
        {
            previousTutorial.interactable = false;
        }
        else
        {
            previousTutorial.interactable = true;
        }
    }

    public void OnClickCloseTutorial()
    {
        tutorialIsShown = false;
        isPaused = false;
        blur.enabled = false;
        Time.timeScale = 1;
        onPlayPanel.SetActive(true);
        onPauseWindow.SetActive(false);
        onStopFinished.SetActive(false);
        tutorialWindow.SetActive(false);
        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
    }
}
