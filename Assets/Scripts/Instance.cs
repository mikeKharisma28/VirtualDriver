using UnityEngine;

public class Instance {
    public static bool isAudioOn;
    public enum Levels { SPLASH_SCREEN, START_MENU, SELECTION_MENU, STRAIGHT_ROAD, HANDLING_EASY, HANDLING_MEDIUM, PARKING_TEST, REPOSITIONING_TEST, RACE_TRACKONE, RACE_TRACKTWO };

    public void ResetAllStaticValues()
    {
        ConesCollision.warningCounter = 0;
        ParkingCheckpoints.isAccomplished = false;
        ParkingCheckpoints.currentCheckpoint = 0;
        RepositionCheckpoints.isAccomplished = false;
        RepositionCheckpoints.currentCheckpoint = 0;
        StopCarCheckpoints.isAccomplished = false;

        CarCheckpoints.currentCheckpoint = 0;
        CarCheckpoints.currentLap = 0;
        Checkpoints.isAccomplished = false;

        ConesRigidCollision.s_conesHitCounter = 0;
    }

    public void FindPlayerAndDisable()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ThrottleAction>() == null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardInput>().enabled = false;
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardInput>() == null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThrottleAction>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<BrakingAction>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SteeringAction>().enabled = false;
        }
    }

    public static string Timer()
    {
        float time = 0;
        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        return string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }

    public Instance()
    {

    }
}
