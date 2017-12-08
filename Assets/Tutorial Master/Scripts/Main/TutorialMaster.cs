using UnityEngine;

namespace HardCodeLab
{
    namespace TutorialMaster
    {
        public class data
        {
            public static bool saveProgress = true;
            public static bool enableLogs = false;

            public static bool track_TutorialsCompleted = true;
            public static bool track_TutorialsInProgress = true;
            public static bool track_lastFrameId = true;

            public static int tutorialsCompleted = 0;
            public static int tutorialsInProgress = 0;

            //Deletes all Tutorial-Master related PlayerPrefs and resets variables
            public static void Reset()
            {
                tutorialsCompleted = 0;
                tutorialsInProgress = 0;

                for (int i = 0; i < tutorial.tmObject.myTutorials.Count; i++)
                {
                    tutorial.tmObject.myTutorials[i].isComplete = false;
                    tutorial.tmObject.myTutorials[i].inProgress = false;
                    tutorial.tmObject.myTutorials[i].lastFrameId = 0;

                    PlayerPrefs.DeleteKey("tutorial_isComplete_" + i);
                    PlayerPrefs.DeleteKey("tutorial_inProgress_" + i);
                    PlayerPrefs.DeleteKey("tutorial_lastFrame_" + i);
                }

                AssignData();
                UpdatePlayerPrefs();

                if (Application.isPlaying)
                {
                }
            }

            //Updates or creates playerprefs from existing variables
            public static void UpdatePlayerPrefs()
            {
                if (saveProgress)
                {
                    for (int i = 0; i < tutorial.tmObject.myTutorials.Count; i++)
                    {
                        if (track_TutorialsCompleted)
                        {
                            //1 ==> 'true' | 0 ==> 'false'
                            PlayerPrefs.SetInt("tutorial_isComplete_" + i, (tutorial.tmObject.myTutorials[i].isComplete ? 1 : 0));
                        }

                        if (track_TutorialsInProgress)
                        {
                            //1 ==> 'true' | 0 ==> 'false'
                            PlayerPrefs.SetInt("tutorial_inProgress_" + i, (tutorial.tmObject.myTutorials[i].inProgress ? 1 : 0));
                        }

                        if (track_lastFrameId)
                        {
                            PlayerPrefs.SetInt("tutorial_lastFrame_" + i, tutorial.tmObject.myTutorials[i].lastFrameId);
                        }
                    }

                    if (track_TutorialsCompleted)
                    {
                        tutorialsCompleted = 0;
                        for (int i = 0; i < tutorial.tmObject.myTutorials.Count; i++)
                        {
                            if (tutorial.tmObject.myTutorials[i].isComplete)
                            {
                                tutorialsCompleted += 1;
                            }
                        }
                        PlayerPrefs.SetInt("data_tutorialsCompleted", tutorialsCompleted);
                    }

                    if (track_TutorialsInProgress)
                    {
                        tutorialsInProgress = 0;
                        for (int i = 0; i < tutorial.tmObject.myTutorials.Count; i++)
                        {
                            if (tutorial.tmObject.myTutorials[i].inProgress)
                            {
                                tutorialsInProgress += 1;
                            }
                        }

                        PlayerPrefs.SetInt("data_tutorialsInProgress", tutorialsInProgress);
                    }
                }
            }

            //Assigns PlayerPrefs data to existing variables
            public static void AssignData()
            {
                if (tutorial.tmObject.myTutorials.Count > 0)
                {
                    for (int i = 0; i < tutorial.tmObject.myTutorials.Count; i++)
                    {
                        if (track_TutorialsCompleted)
                        {
                            tutorial.tmObject.myTutorials[i].isComplete = (PlayerPrefs.GetInt("tutorial_isComplete_" + i) == 1);
                        }

                        if (track_TutorialsInProgress)
                        {
                            tutorial.tmObject.myTutorials[i].inProgress = (PlayerPrefs.GetInt("tutorial_inProgress_" + i) == 1);
                        }

                        if (track_lastFrameId)
                        {
                            tutorial.tmObject.myTutorials[i].lastFrameId = PlayerPrefs.GetInt("tutorial_lastFrame_" + i);
                        }
                    }

                    tutorialsCompleted = PlayerPrefs.GetInt("data_tutorialsCompleted");
                    tutorialsInProgress = PlayerPrefs.GetInt("data_tutorialsInProgress");
                }
            }
        }

        public class tutorial
        {
            public static TutorialMasterScript tmObject = GameObject.FindObjectOfType<TutorialMasterScript>();
            public static TutorialMasterEventsSystem tmEvents = GameObject.FindObjectOfType<TutorialMasterEventsSystem>();

            public static bool isPlaying = false;

            public static int id = 0;

            //tutorial progress from 0 (incomplete) to 1(complete);
            public static float progress = 0;

            public static void Init()
            {
                tmObject = GameObject.FindObjectOfType<TutorialMasterScript>();
                tmEvents = GameObject.FindObjectOfType<TutorialMasterEventsSystem>();
            }

            //Starts a tutorial of a specified ID
            public static void Start(int tutorial_id, bool continueFromLastFrame = false)
            {
                if (tmObject.isValid && tmObject.frame_data != null && tmObject.tutorial_data != null)
                {
                    if (!isPlaying)
                    {
                        if (tmObject.myTutorials.Count > tutorial_id)
                        {
                            if (tmObject.myTutorials[tutorial_id].isReady)
                            {
                                isPlaying = true;

                                tmObject.skipEffects = false;

                                id = tutorial_id;
                                if (data.saveProgress && continueFromLastFrame) frame.id = tmObject.myTutorials[tutorial_id].lastFrameId; else frame.id = 0;

                                tmObject.UpdateData();
                                tmObject.UpdateTutorialProgress();

                                tmObject.ConfigureItemsOrder();
                                tmObject.ConfigureBackground();
                                tmObject.ConfigureTarget();
                                tmObject.ConfigureIcon();
                                tmObject.ConfigureText();
                                tmObject.OutlineTarget();
                                tmObject.PointArrowAtTarget();
                                tmObject.ConfigureAudio();

                                tmObject.TMDebugLog("Starting the tutorial of ID " + tutorial_id);

                                if (data.saveProgress)
                                {
                                    tmObject.myTutorials[tutorial_id].isComplete = false;
                                    tmObject.myTutorials[tutorial_id].inProgress = true;
                                    tmObject.myTutorials[tutorial_id].lastFrameId = 0;

                                    data.UpdatePlayerPrefs();
                                }

                                tutorial.tmObject.ConfigureTimer();

                                if (tmEvents != null)
                                {
                                    tmEvents.OnTutorialStart.Invoke();
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Tutorial Master: It seems that something is missing for the tutorial of ID " + tutorial_id);
                            }
                        }
                        else
                        {
                            tmObject.TMDebugLog("Tutorial of ID " + tutorial_id + " does not exist");
                        }
                    }
                    else
                    {
                        tmObject.TMDebugLog("Tutorial has already started");
                    }
                }
                else
                {
                    tmObject.TMDebugLog("UI Canvas is not attached in the 'Tutorial Master Script' component. Attach it and try again");
                }
            }

            //Stops the tutorial
            public static void Stop()
            {
                if (isPlaying)
                {
                    isPlaying = false;
                    tmObject.RemoveOutlines();
                    tmObject.ResetItemsOrder();
                    tmObject.RemoveTMListener();
                    tmObject.RemovePointingArrow();

                    tmObject.ConfigureItemsOrder();
                    tmObject.ConfigureBackground();
                    tmObject.ConfigureTarget();
                    tmObject.ConfigureIcon();
                    tmObject.ConfigureText();
                    tmObject.OutlineTarget();
                    tmObject.PointArrowAtTarget();

                    tmObject.ConfigureAudio();

                    if (data.saveProgress)
                    {
                        tmObject.myTutorials[id].isComplete = true;
                        tmObject.myTutorials[id].inProgress = false;
                        tmObject.myTutorials[id].lastFrameId = frame.id;

                        data.UpdatePlayerPrefs();
                    }

                    tmObject.StopAllCoroutines();

                    frame.id = 0;
                    id = 0;

                    tmObject.UpdateData();

                    progress = 0;

                    tmObject.skipEffects = false;

                    if (tmEvents != null)
                    {
                        tmEvents.OnTutorialEnd.Invoke();
                    }

                    tmObject.TMDebugLog("Tutorial has been stopped");
                }
                else
                {
                    tmObject.TMDebugLog("Ignoring the command because no tutorial is playing");
                }
            }

            //Skip all effects such as fade-in and fly-in
            public static void SkipAppearanceEffects()
            {
                tmObject.skipEffects = true;
            }
        }

        public class frame
        {
            public static int id = 0;

            //Plays the next frame
            public static void Next()
            {
                if (tutorial.isPlaying)
                {
                    if (tutorial.tmObject.tutorial_data.myFrames.Count != (frame.id + 1))
                    {
                        tutorial.tmObject.skipEffects = false;

                        tutorial.tmObject.RemoveOutlines();
                        tutorial.tmObject.ResetItemsOrder();
                        tutorial.tmObject.RemoveTMListener();
                        tutorial.tmObject.RemovePointingArrow();
                        frame.id += 1;
                        tutorial.tmObject.UpdateData();
                        tutorial.tmObject.UpdateTutorialProgress();

                        tutorial.tmObject.ConfigureItemsOrder();
                        tutorial.tmObject.ConfigureBackground();
                        tutorial.tmObject.ConfigureTarget();
                        tutorial.tmObject.ConfigureIcon();
                        tutorial.tmObject.ConfigureText();
                        tutorial.tmObject.OutlineTarget();
                        tutorial.tmObject.PointArrowAtTarget();
                        tutorial.tmObject.ConfigureAudio();

                        tutorial.tmObject.skipEffects = false;

                        if (data.saveProgress)
                        {
                            tutorial.tmObject.myTutorials[tutorial.id].lastFrameId = id;

                            data.UpdatePlayerPrefs();
                        }

                        tutorial.tmObject.ConfigureTimer();

                        if (tutorial.tmEvents != null)
                        {
                            tutorial.tmEvents.OnFrameEnter.Invoke();
                        }
                    }
                    else
                    {
                        tutorial.tmObject.TMDebugLog("Reached the last frame. Stopping tutorial...");

                        if (data.saveProgress)
                        {
                            //int tempVal = tutorialId;
                            tutorial.Stop();
                            tutorial.tmObject.myTutorials[tutorial.id].lastFrameId = 0;
                            data.UpdatePlayerPrefs();
                        }
                        else
                        {
                            tutorial.Stop();
                        }
                    }
                }
                else
                {
                    tutorial.tmObject.TMDebugLog("Tutorial must be started in order to go to the next frame. Fix by calling 'tutorial.Start(tutorial_id)'");
                }
            }

            //Goes to a specific frame
            public static void Goto(int targetFrameId)
            {
                if (tutorial.isPlaying)
                {
                    tutorial.tmObject.skipEffects = false;

                    tutorial.tmObject.RemoveOutlines();
                    tutorial.tmObject.ResetItemsOrder();
                    tutorial.tmObject.RemoveTMListener();
                    tutorial.tmObject.RemovePointingArrow();
                    id = targetFrameId;
                    tutorial.tmObject.UpdateData();
                    tutorial.tmObject.UpdateTutorialProgress();

                    tutorial.tmObject.ConfigureBackground();
                    tutorial.tmObject.ConfigureTarget();
                    tutorial.tmObject.ConfigureIcon();
                    tutorial.tmObject.ConfigureText();
                    tutorial.tmObject.OutlineTarget();
                    tutorial.tmObject.PointArrowAtTarget();
                    tutorial.tmObject.ConfigureAudio();

                    tutorial.tmObject.TMDebugLog("Frame of ID " + targetFrameId + " is now being played");

                    if (data.saveProgress)
                    {
                        tutorial.tmObject.myTutorials[tutorial.id].lastFrameId = id;

                        data.UpdatePlayerPrefs();
                    }

                    tutorial.tmObject.ConfigureTimer();

                    if (tutorial.tmEvents != null)
                    {
                        tutorial.tmEvents.OnFrameEnter.Invoke();
                    }
                }
                else
                {
                    tutorial.tmObject.TMDebugLog("Tutorial must be started in order to jump to a specific frame. Fix by calling 'tutorial.Start(tutorial_id)'");
                }
            }

            //Plays the previous frame
            public static void Prev()
            {
                if (tutorial.isPlaying)
                {
                    if (tutorial.tmObject.tutorial_data.myFrames.Count != (id - 1))
                    {
                        tutorial.tmObject.skipEffects = false;

                        tutorial.tmObject.RemoveOutlines();
                        tutorial.tmObject.ResetItemsOrder();
                        tutorial.tmObject.RemoveTMListener();
                        tutorial.tmObject.RemovePointingArrow();
                        id -= 1;
                        tutorial.tmObject.UpdateData();
                        tutorial.tmObject.UpdateTutorialProgress();

                        tutorial.tmObject.ConfigureItemsOrder();
                        tutorial.tmObject.ConfigureBackground();
                        tutorial.tmObject.ConfigureTarget();
                        tutorial.tmObject.ConfigureIcon();
                        tutorial.tmObject.ConfigureText();
                        tutorial.tmObject.OutlineTarget();
                        tutorial.tmObject.PointArrowAtTarget();
                        tutorial.tmObject.ConfigureAudio();

                        tutorial.tmObject.skipEffects = false;

                        if (data.saveProgress)
                        {
                            tutorial.tmObject.myTutorials[tutorial.id].lastFrameId = id;

                            data.UpdatePlayerPrefs();
                        }

                        tutorial.tmObject.ConfigureTimer();

                        if (tutorial.tmEvents != null)
                        {
                            tutorial.tmEvents.OnFrameEnter.Invoke();
                        }
                    }
                    else
                    {
                        tutorial.tmObject.TMDebugLog("Ignoring the command to prevent the 'out of range' error...");
                    }
                }
                else
                {
                    tutorial.tmObject.TMDebugLog("Tutorial must be started in order to go to the previous frame. Fix by calling 'tutorial.Start(tutorial_id)'");
                }
            }
        }
    }
}