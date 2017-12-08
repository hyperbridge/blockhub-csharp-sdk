using HardCodeLab.TutorialMaster;
using UnityEngine;

[AddComponentMenu("Tutorial Master/Utility Scripts/Tutorial Master Controller")]
public class TutorialMasterController : MonoBehaviour
{
    public bool tutorialIsPlaying; //       returns true if tutorial is playings
    public int currentTutorialId; //        returns the id of the currently playing tutorial. returns -1 if nothing is playing
    public int currentFrameId; //           returns the id of the currently playing frame. returns -1 if nothing is playing

    public bool StartOnPlay = false; //     if set true, tutorial of a specified id will play when the scene starts
    public int BeginningTutorialId = 0; //  if StartOnPlay is true, which tutorial should be played when scene starts?
    public bool resumeTutorial = false; //  if set true, tutorial will resume if it has been interrupted in the process
    public float tutorialStartDelay = 0; // amount of seconds to wait till starting a specified tutorial

    private void Start()
    {
        //Start specified tutorial if StartOnPlay is true
        if (StartOnPlay)
        {
            tutorial.Start(BeginningTutorialId, resumeTutorial); //     start the tutorial
        }
    }

    private void Update()
    {
        //Synchronize variables with namespace variables
        tutorialIsPlaying = tutorial.isPlaying;
        currentTutorialId = tutorial.id;
        currentFrameId = frame.id;
    }

    //Starts the tutorial of specific id. If set true, tutorial would be continued if stopped before
    public void StartTutorial(int id)
    {
        tutorial.Start(id, false);
    }

    //Stops the current tutorial
    public void StopTutorial()
    {
        tutorial.Stop();
    }

    //Toggles the tutorial of specified id. If the tutorial is played, it's stopped. Otherwise, start the tutorial of specified id.
    public void ToggleTutorial(int id)
    {
        if (tutorial.isPlaying)
        {
            tutorial.Stop();
        }
        else
        {
            tutorial.Start(id);
        }
    }

    //Go to the next frame of the tutorial (command will be ignored if tutorial is not played)
    public void NextFrame()
    {
        frame.Next();
    }

    //Go to a previous frame of the tutorial (command will be ignored if tutorial is not played)
    public void PreviousFrame()
    {
        frame.Prev();
    }

    //Go to specific part of the tutorial (command will be ignored if tutorial is not played)
    public void GotoFrame(int id)
    {
        frame.Goto(id);
    }
}