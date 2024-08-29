using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.IO;

public class HandlingEasySceneButtonsScript : MonoBehaviour 
{
    //This happens when a player hits the ESC button
    bool isPaused = false;
    bool tutorialIsShown = false;

    public GameObject[] Tutorials;
    public GameObject onPauseWindow;
    public GameObject onPlayPanel;
    public GameObject onStopAccomplished;
    public GameObject onStopFail;
    public GameObject tutorialWindow;
    public Button nextTutorial;
    public Button previousTutorial;

    [SerializeField] GameObject cameraView;
    GameObject Player;
    BlurOptimized blur;

    void Awake()
    {
        Instance instance = new Instance();
        instance.ResetAllStaticValues();

        Player = Resources.Load(LoadPathString(), typeof(GameObject)) as GameObject;
        Player = Instantiate(Player);
        Player.GetComponent<CarCheckpoints>().enabled = false;

        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
    }

    void Start()
    {
        if (LoadPathString().EndsWith("PlayerRealSense"))
            cameraView.SetActive(true);
        else
            cameraView.SetActive(false);
        blur = GetComponent<BlurOptimized>();
        blur.enabled = false;
        previousTutorial.interactable = false;
        tutorialIsShown = true;
        isPaused = true;
    }

    void Update()
    {
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
                onStopAccomplished.SetActive(false);
                onStopFail.SetActive(false);
                tutorialWindow.SetActive(true);
                GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
            }
            else
            {
                Time.timeScale = 0;
                onPlayPanel.SetActive(false);
                onPauseWindow.SetActive(true);
                onStopFail.SetActive(false);
                onStopAccomplished.SetActive(false);
                GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
            }
        }
        else if (!isPaused)
        {
            if (Player.GetComponent<Rigidbody>().velocity.magnitude * 3.6f >= 70f)
                Player.GetComponent<CarController>().m_Rev = 0f;
            else
                Player.GetComponent<CarController>().m_Rev = 0.65f;

            if (tutorialIsShown)
            {
                //do nothing
            }
            else
            {
                Instance instance = new Instance();
                if (ConesCollision.warningCounter > 3)
                {
                    Time.timeScale = 0.5f;
                    onPlayPanel.SetActive(false);
                    onPauseWindow.SetActive(false);
                    onStopAccomplished.SetActive(false);
                    onStopFail.SetActive(true);
                    instance.FindPlayerAndDisable();
                    GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
                }
                else if (StopCarCheckpoints.isAccomplished)
                {
                    Time.timeScale = 0.5f;
                    onPlayPanel.SetActive(false);
                    onPauseWindow.SetActive(false);
                    onStopAccomplished.SetActive(true);
                    onStopFail.SetActive(false);
                    ConesCollision.warningCounter = 0;
                    instance.FindPlayerAndDisable();
                    GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
                }
                else
                {
                    Time.timeScale = 1;
                    onPauseWindow.SetActive(false);
                    onPlayPanel.SetActive(true);
                    onStopFail.SetActive(false);
                    onStopAccomplished.SetActive(false);
                    if (Instance.isAudioOn)
                        GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
                    else
                        GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
                }
            }
        }

    }

   public void OnClickBackToGarage()
    {
        isPaused = false;
        SceneManager.LoadScene((int)Instance.Levels.SELECTION_MENU);
    }

    public void OnClickRestart()
    {
        isPaused = false;
        SceneManager.LoadScene((int)Instance.Levels.HANDLING_EASY);
    }

    public void OnClickQuit()
    {
        isPaused = false;
        SceneManager.LoadScene((int)Instance.Levels.START_MENU);
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
        onStopAccomplished.SetActive(false);
        onStopFail.SetActive(false);
        tutorialWindow.SetActive(false);
        if (Instance.isAudioOn)
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = false;
        else
            GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>().mute = true;
    }
}
