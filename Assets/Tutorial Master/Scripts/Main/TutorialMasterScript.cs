using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

#if UNITY_EDITOR

using UnityEditor;

#endif

using System.Collections;
using System.Collections.Generic;
using HardCodeLab.TutorialMaster;
using System.Linq;

[System.Serializable]
[AddComponentMenu("Tutorial Master/ Tutorial Master Module")]
public class TutorialMasterScript : MonoBehaviour
{
    //Editor variables
    public bool tutorialSelected = false;

    public bool frameSelected = false;

    public bool showTutorialsList = false;

    public int selectedTutorialIndex = 0;
    public int selectedFrameIndex = 0;

    public GameObject UICanvas;
    public GameObject darkBackgroundUI;
    public GameObject arrowUI;
    public GameObject textUI;
    public GameObject iconUI;
    public GameObject parentObject;

    public AudioSource audioSource;

    public bool icon_ui_has_parent = false;
    public bool text_ui_has_parent = false;
    public GameObject icon_ui_parent;
    public GameObject text_ui_parent;

    public bool hasDarkBackgroundUI = false;
    public bool hasArrowUI = false;
    public bool hasExceptions = false;
    public bool hasAudioSource = false;
    public bool hasTextUI = false;
    public bool hasIconUI = false;

    //Runtime
    public TMEffects tmE = new TMEffects();

    public TMArrow tmA = new TMArrow();

    public List<tutorialClass> myTutorials = new List<tutorialClass>();
    public bool skipEffects = false;
    public frameClass frame_data;
    public tutorialClass tutorial_data;

    public bool isValid = false;

    public bool enable_logs = false;
    public bool save_progress = true;

    private UnityEngine.Events.UnityAction nextTutorialAction = () => { frame.Next(); };

    public Vector3 arrow_initPos;

    private int text_parent_sibling;
    private int icon_parent_sibling;
    private int text_ui_sibling;
    private int icon_ui_sibling;
    private int dark_ui_sibling;
    private int target_ui_sibling;
    private int arrow_ui_sibling;

    //Custom classes
    [System.Serializable]
    public class tutorialClass
    {
        public string tutorialName;
        public List<frameClass> myFrames = new List<frameClass>();
        public bool isReady = false;

        public bool isComplete = false;
        public bool inProgress = false;
        public int lastFrameId = 0;
    }

    [System.Serializable]
    public class frameClass
    {
        public string frameName;

        //used to check if frame is ready for use
        public bool iconReady = false;

        public bool targetReady = false;
        public bool audioReady = false;
        public bool isReady = false;

        //timers
        public bool useTimer = false;

        public float timerAmount = 1.5f;

        //dark background
        public bool useBackground = true;

        public bool addExceptions = false;
        public List<exceptionObject> exceptionsList = new List<exceptionObject>();
        public Color blurColor = Color.white;
        public float blurSize = 2.5f;
        public bool fadeInBackground = false;
        public float background_fade_speed = 3.5f;

        //highlight target
        public bool doHighlight = true;

        public GameObject targetObject;
        public bool highlight_target_is_null = true;
        public bool detectUIClicks = true;

        //outline
        public bool target_outline = false;

        public Color outlineColor = Color.white;
        public float outlineThickness = 1.0f;
        public float colorBounceSpeed = 2.5f;

        //pointing arrow
        public bool useArrow = false;

        public float arrowOffset_x = 0;
        public float arrowOffset_y = 0;
        public bool arrow_renderTopMost = false;
        public arrow_pointing_direction arrow_pointDirection;

        //arrow floating effect
        public bool arrow_useFloatingEffect = false;

        public float arrowFloating_Range = 40;
        public float arrowFloating_Speed = 3.5f;

        //arrow fade in effect
        public bool arrow_useFadeIn = false;

        public float arrow_fade_speed = 3.5f;

        //text & icon uses
        public bool useIcon = false;

        public bool useText = true;

        //icon settings
        public Sprite iconSprite;

        public flyInDirection icon_flyInDirection;
        public float icon_fade_speed = 3.5f;
        public float icon_warpValue = 1000;
        public float icon_fly_speed = 2.5f;

        //icon effects
        public bool icon_flyIn = false;

        public bool icon_fadeIn = false;

        //text settings
        public string descriptionText = "";

        public float text_typing_speed = 2.5f;
        public int text_letter_amount = 1;
        public float text_fade_speed = 3.5f;
        public float text_warpValue = 1000;
        public float text_fly_speed = 2.5f;
        public flyInDirection text_flyInDirection;

        //text effects
        public bool text_flyIn = false;

        public bool text_fadeIn = false;
        public bool text_typing = false;

        public bool allowEffectSkip = true;

        //audio
        public bool useAudio = false;

        public AudioClip audioClip;
        public bool moreAudioSettings = false;
        public AudioMixerGroup audio_clip_output;
        public bool audio_enable_loop = false;
        public float audio_volume = 1.0f;
    }

    [System.Serializable]
    public class exceptionObject
    {
        public GameObject gameObject;
        public int default_index;
    }

    //Enumerators
    [System.Serializable]
    public enum highlight_method
    { outline, pointingArrow, bounceScale, animation, none }

    [System.Serializable]
    public enum flyInDirection
    { Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight }

    [System.Serializable]
    public enum arrow_pointing_direction
    { Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight }

    /// <summary>
    /// EDITOR FUNCTIONS - Used only inside of the editor
    ///</summary>

#if UNITY_EDITOR

    //Creates a new tutorial
    public void CreateTutorial()
    {
        myTutorials.Add(new tutorialClass
        {
            tutorialName = "Tutorial " + (myTutorials.Count + 1),
        });
    }

    //Deletes a tutorial from the list
    public void DeleteTutorial(int tutorialId)
    {
        myTutorials.RemoveAt(tutorialId);
    }

    //Adds a new frame to a specified tutorial
    public void AddFrame(int tutorialId)
    {
        if (myTutorials[tutorialId].myFrames.Count > 0)
        {
            //get the settings of the latest frame
            frameClass oldFrame = myTutorials[tutorialId].myFrames[myTutorials[tutorialId].myFrames.Count - 1];

            //Add frame
            myTutorials[tutorialId].myFrames.Add(new frameClass
            {
                frameName = "Frame " + (myTutorials[tutorialId].myFrames.Count + 1),
            });

            //get the settings of the newest frame
            frameClass newFrame = myTutorials[tutorialId].myFrames[myTutorials[tutorialId].myFrames.Count - 1];

            //background settings
            newFrame.useBackground = oldFrame.useBackground;
            if (oldFrame.fadeInBackground)
            {
                newFrame.fadeInBackground = false;
            }
            newFrame.background_fade_speed = oldFrame.background_fade_speed;

            //highlight targets
            newFrame.doHighlight = oldFrame.doHighlight;
            newFrame.detectUIClicks = oldFrame.detectUIClicks;

            //point arrow
            newFrame.useArrow = oldFrame.useArrow;
            newFrame.arrow_pointDirection = oldFrame.arrow_pointDirection;
            newFrame.arrowOffset_x = oldFrame.arrowOffset_x;
            newFrame.arrowOffset_y = oldFrame.arrowOffset_y;

            newFrame.arrow_useFloatingEffect = oldFrame.arrow_useFloatingEffect;
            newFrame.arrowFloating_Range = oldFrame.arrowFloating_Range;
            newFrame.arrowFloating_Speed = oldFrame.arrowFloating_Speed;

            newFrame.arrow_useFadeIn = oldFrame.arrow_useFadeIn;
            newFrame.arrow_fade_speed = oldFrame.arrow_fade_speed;

            //outline
            newFrame.target_outline = oldFrame.target_outline;
            newFrame.outlineColor = oldFrame.outlineColor;
            newFrame.outlineThickness = oldFrame.outlineThickness;

            //icon
            newFrame.icon_fly_speed = oldFrame.icon_fly_speed;
            newFrame.icon_fade_speed = oldFrame.icon_fade_speed;

            //text
            newFrame.text_fly_speed = oldFrame.text_fly_speed;
            newFrame.text_fade_speed = oldFrame.text_fade_speed;
            newFrame.text_typing_speed = oldFrame.text_typing_speed;
            newFrame.text_letter_amount = oldFrame.text_letter_amount;

            //audio
            newFrame.useAudio = oldFrame.useAudio;
            newFrame.audioClip = oldFrame.audioClip;
            newFrame.audio_clip_output = oldFrame.audio_clip_output;
        }
        else
        {
            myTutorials[tutorialId].myFrames.Add(new frameClass
            {
                frameName = "Frame " + (myTutorials[tutorialId].myFrames.Count + 1),
            });
        }
    }

    //Removes specific frame from a specific tutorial
    public void RemoveFrame(int tutorialId, int frameId)
    {
        myTutorials[tutorialId].myFrames.RemoveAt(frameId);
    }

    //Moves a frame up in the list
    public void MoveFrameUp(int frameId)
    {
        frameClass oldItem = myTutorials[selectedTutorialIndex].myFrames[frameId - 1];
        myTutorials[selectedTutorialIndex].myFrames[frameId - 1] = myTutorials[selectedTutorialIndex].myFrames[frameId];
        myTutorials[selectedTutorialIndex].myFrames[frameId] = oldItem;
    }

    //Moves a frame down in the list
    public void MoveFrameDown(int frameId)
    {
        frameClass oldItem = myTutorials[selectedTutorialIndex].myFrames[frameId + 1];
        myTutorials[selectedTutorialIndex].myFrames[frameId + 1] = myTutorials[selectedTutorialIndex].myFrames[frameId];
        myTutorials[selectedTutorialIndex].myFrames[frameId] = oldItem;
    }

    //Adds a new tag in the Tag Manager. Nothing happens if Tag already exists
    public void GenerateTag(string tag)
    {
        if (!Application.isPlaying)
        {
            Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
            if ((asset != null) && (asset.Length > 0))
            {
                SerializedObject serialized_object = new SerializedObject(asset[0]);
                SerializedProperty tags = serialized_object.FindProperty("tags");

                for (int i = 0; i < tags.arraySize; ++i)
                {
                    if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                    {
                        return;
                    }
                }

                tags.InsertArrayElementAtIndex(tags.arraySize);
                tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tag;
                serialized_object.ApplyModifiedProperties();
                serialized_object.Update();
            }
        }
    }

    public void GenerateMissingComponents()
    {
        GenerateTag("TM Text UI");
        GenerateTag("TM Icon UI");
        GenerateTag("TM Arrow UI");
        GenerateTag("TM Dark Background UI");
        GenerateTag("TM Audio Source");
        tag = "Tutorial Master";

        if (textUI == null)
        {
            AssignTextUI();
        }

        if (iconUI == null)
        {
            AssignIconUI();
        }

        if (audioSource == null)
        {
            AssignAudioSource();
        }

        if (arrowUI == null)
        {
            AssignArrowUI();
        }

        if (darkBackgroundUI == null)
        {
            AssignDarkBackgroundUI();
        }
    }

    public void AssignTextUI()
    {
        if (GameObject.FindWithTag("TM Text UI") == null)
        {
            GameObject temp_text_ui = new GameObject("Text UI");
            temp_text_ui.tag = "TM Text UI";

            temp_text_ui.AddComponent<CanvasRenderer>();
            temp_text_ui.AddComponent<Text>();
            temp_text_ui.GetComponent<Text>().text = "Tutorial Master Text UI";
            temp_text_ui.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            temp_text_ui.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);

            temp_text_ui.transform.SetParent(UICanvas.transform, false);
            textUI = temp_text_ui;
            textUI.transform.SetAsFirstSibling();
        }
        else
        {
            Debug.LogWarning("Found an existing Text UI. Reassigning...");
            textUI = GameObject.FindWithTag("TM Text UI");
        }
    }

    public void AssignIconUI()
    {
        if (GameObject.FindWithTag("TM Icon UI") == null)
        {
            GameObject temp_icon_ui = new GameObject("Icon UI");
            temp_icon_ui.tag = "TM Icon UI";

            temp_icon_ui.AddComponent<CanvasRenderer>();
            temp_icon_ui.AddComponent<Image>();
            temp_icon_ui.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Resources/Images/gears.png", typeof(Sprite));

            temp_icon_ui.transform.SetParent(UICanvas.transform, false);
            iconUI = temp_icon_ui;
            iconUI.transform.SetAsFirstSibling();
        }
        else
        {
            Debug.LogWarning("Found an existing Icon UI. Reassigning...");
            iconUI = GameObject.FindWithTag("TM Icon UI");
        }
    }

    public void AssignArrowUI()
    {
        if (GameObject.FindWithTag("TM Arrow UI") == null)
        {
            GameObject temp_arrow_ui = new GameObject("Arrow UI");
            temp_arrow_ui.tag = "TM Arrow UI";

            temp_arrow_ui.AddComponent<CanvasRenderer>();
            temp_arrow_ui.AddComponent<Image>();

            temp_arrow_ui.GetComponent<Image>().raycastTarget = false;
            temp_arrow_ui.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Resources/Images/arrow.png", typeof(Sprite));
            temp_arrow_ui.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 45);

            temp_arrow_ui.transform.SetParent(UICanvas.transform, false);
            temp_arrow_ui.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
            arrowUI = temp_arrow_ui;
            arrowUI.transform.SetAsFirstSibling();
            arrowUI.SetActive(false);
            arrowUI.GetComponent<RectTransform>().position = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("Found an existing Arrow UI. Reassigning...");
            arrowUI = GameObject.FindWithTag("TM Arrow UI");
        }
    }

    public void AssignDarkBackgroundUI()
    {
        if (GameObject.FindWithTag("TM Dark Background UI") == null)
        {
            GameObject temp_dark_ui = new GameObject("Dark Background UI");
            temp_dark_ui.tag = "TM Dark Background UI";

            temp_dark_ui.AddComponent<CanvasRenderer>();
            temp_dark_ui.AddComponent<RectTransform>();
            temp_dark_ui.AddComponent<Image>();
            temp_dark_ui.GetComponent<Image>().color = new Color(0, 0, 0, 0.55f);

            temp_dark_ui.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UICanvas.GetComponent<RectTransform>().sizeDelta.x);
            temp_dark_ui.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UICanvas.GetComponent<RectTransform>().sizeDelta.y);
            temp_dark_ui.GetComponent<RectTransform>().localPosition = UICanvas.GetComponent<RectTransform>().pivot;

            temp_dark_ui.transform.SetParent(UICanvas.transform, false);

            darkBackgroundUI = temp_dark_ui;
            darkBackgroundUI.transform.SetAsFirstSibling();

            darkBackgroundUI.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            darkBackgroundUI.GetComponent<RectTransform>().anchorMax = Vector2.one;

            darkBackgroundUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Found an existing Dark Background UI. Reassigning...");
            darkBackgroundUI = GameObject.FindWithTag("TM Dark Background UI");
        }
    }

    public void AssignAudioSource()
    {
        if (GameObject.FindWithTag("TM Audio Source") == null)
        {
            GameObject temp_audio_source = new GameObject("TM Audio Source");
            temp_audio_source.tag = "TM Audio Source";

            temp_audio_source.AddComponent<AudioSource>();

            audioSource = temp_audio_source.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("Found an existing Audio Source. Reassigning...");
            audioSource = GameObject.FindWithTag("TM Audio Source").GetComponent<AudioSource>();
        }
    }

#endif

    /// <summary>
    /// RUNTIME FUNCTIONS - Functions used while the game is running
    /// </summary>

    private void Awake()
    {
        tutorial.Init();

        if (data.saveProgress)
        {
            data.AssignData();
        }
    }

    private void Update()
    {
        data.enableLogs = enable_logs;
        data.saveProgress = save_progress;

        if (tutorial.isPlaying)
        {
            if (frame_data.allowEffectSkip)
            {
                if (Input.GetMouseButtonDown(0) && !skipEffects)
                {
                    TMDebugLog("Skipping all appearance effects...");
                    tutorial.SkipAppearanceEffects();
                }
            }

            if (frame_data.doHighlight)
            {
                if (frame_data.useArrow)
                {
                    if (frame_data.arrow_useFloatingEffect)
                    {
                        tmA.AnimateFloatingArrow(arrowUI, frame_data.targetObject, frame_data.arrowFloating_Range, frame_data.arrowFloating_Speed, frame_data.arrow_pointDirection);
                    }
                }
            }
        }
    }

    //Updates the tutorial and frame data variables
    public void UpdateData()
    {
        frame_data = myTutorials[tutorial.id].myFrames[frame.id];
        tutorial_data = myTutorials[tutorial.id];
    }

    //Darkens the background behind the sprite/UI
    public void ConfigureBackground()
    {
        if (tutorial.isPlaying)
        {
            if (hasDarkBackgroundUI)
            {
                if (frame_data.useBackground)
                {
                    darkBackgroundUI.SetActive(true);

                    if (frame_data.fadeInBackground)
                    {
                        StartCoroutine(tmE.effect_fade_in(darkBackgroundUI, frame_data.background_fade_speed, false, skipEffects));
                    }
                }
                else
                {
                    if (darkBackgroundUI != null)
                    {
                        darkBackgroundUI.SetActive(false);
                    }
                }
            }
            else
            {
                TMDebugLog("DarkBackgroundUI is null. Skipping...");
            }
        }
        else
        {
            if (hasDarkBackgroundUI && darkBackgroundUI != null)
            {
                darkBackgroundUI.SetActive(false);
            }
        }
    }

    //Puts dark background UI, target UI, icon and text UIs in specified order
    public void ConfigureItemsOrder()
    {
        if (tutorial.isPlaying)
        {
            if (hasDarkBackgroundUI)
            {
                if (frame_data.useBackground)
                {
                    //Put DARK BACKGROUND above anything else
                    dark_ui_sibling = darkBackgroundUI.transform.GetSiblingIndex();
                    darkBackgroundUI.transform.SetAsLastSibling();
                }

                if (frame_data.useBackground)
                {
                    if (frame_data.doHighlight)
                    {
                        //Put HIGHLIGHT TARGET above anything else
                        target_ui_sibling = frame_data.targetObject.transform.GetSiblingIndex();
                        frame_data.targetObject.transform.SetAsLastSibling();
                    }

                    //Put items from EXCEPTIONS list above anything else
                    if (frame_data.addExceptions)
                    {
                        //order all exception list items into accending order based on their sibling index
                        frame_data.exceptionsList = frame_data.exceptionsList.OrderBy(x => x.default_index).ToList();

                        for (int i = 0; i < frame_data.exceptionsList.Count; i++)
                        {
                            frame_data.exceptionsList[i].gameObject.transform.SetAsLastSibling();
                        }
                    }

                    //Put ICON, TEXT and ARROW ui in order accordingly
                    if (frame_data.arrow_renderTopMost) //check whether user wants an arrow to be rendered on top of everything else
                    {
                        if (frame_data.useIcon)
                        {
                            if (!icon_ui_has_parent)
                            {
                                icon_ui_sibling = iconUI.transform.GetSiblingIndex();
                                iconUI.transform.SetAsLastSibling();
                            }
                            else
                            {
                                icon_parent_sibling = icon_ui_parent.transform.GetSiblingIndex();
                                icon_ui_parent.transform.SetAsLastSibling();
                            }
                        }
                        else
                        {
                            if (iconUI != null)
                            {
                                iconUI.transform.SetSiblingIndex(icon_ui_sibling);
                            }
                        }

                        if (frame_data.useText)
                        {
                            if (!text_ui_has_parent)
                            {
                                text_ui_sibling = textUI.transform.GetSiblingIndex();
                                textUI.transform.SetAsLastSibling();
                            }
                            else
                            {
                                text_parent_sibling = text_ui_parent.transform.GetSiblingIndex();
                                text_ui_parent.transform.SetAsLastSibling();
                            }
                        }
                        else
                        {
                            textUI.transform.SetSiblingIndex(text_ui_sibling);
                        }

                        if (frame_data.useArrow)
                        {
                            arrow_ui_sibling = arrowUI.transform.GetSiblingIndex();
                            arrowUI.transform.SetAsLastSibling();
                        }
                        else
                        {
                            arrowUI.transform.SetSiblingIndex(arrow_ui_sibling);
                        }
                    }
                    else
                    {
                        if (frame_data.useArrow)
                        {
                            arrow_ui_sibling = arrowUI.transform.GetSiblingIndex();
                            arrowUI.transform.SetAsLastSibling();
                        }
                        else
                        {
                            arrowUI.transform.SetSiblingIndex(arrow_ui_sibling);
                        }

                        if (frame_data.useIcon)
                        {
                            if (!icon_ui_has_parent)
                            {
                                if (iconUI != null)
                                {
                                    icon_ui_sibling = iconUI.transform.GetSiblingIndex();
                                    iconUI.transform.SetAsLastSibling();
                                }
                            }
                            else
                            {
                                icon_parent_sibling = icon_ui_parent.transform.GetSiblingIndex();
                                icon_ui_parent.transform.SetAsLastSibling();
                            }
                        }
                        else
                        {
                            if (iconUI != null)
                            {
                                iconUI.transform.SetSiblingIndex(icon_ui_sibling);
                            }
                        }

                        if (frame_data.useText)
                        {
                            if (!text_ui_has_parent)
                            {
                                if (textUI != null)
                                {
                                    text_ui_sibling = textUI.transform.GetSiblingIndex();
                                    textUI.transform.SetAsLastSibling();
                                }
                            }
                            else
                            {
                                if (text_ui_parent != null)
                                {
                                    text_parent_sibling = text_ui_parent.transform.GetSiblingIndex();
                                    text_ui_parent.transform.SetAsLastSibling();
                                }
                            }
                        }
                        else
                        {
                            if (textUI != null)
                            {
                                textUI.transform.SetSiblingIndex(text_ui_sibling);
                            }
                        }
                    }
                }
            }
            else
            {
                TMDebugLog("Dark Background UI is null. Unable to set its hierarchy order. Skipping...");
            }
        }
    }

    //Configures a highlight target
    public void ConfigureTarget()
    {
        if (tutorial.isPlaying)
        {
            if (frame_data.doHighlight)
            {
                if (frame_data.detectUIClicks)
                {
                    frame_data.targetObject.GetComponent<Button>().onClick.AddListener(nextTutorialAction);
                }
            }
        }
    }

    //Removes onClick Listener from the button
    public void RemoveTMListener()
    {
        if (frame_data.doHighlight)
        {
            if (frame_data.detectUIClicks)
            {
                frame_data.targetObject.GetComponent<Button>().onClick.RemoveListener(nextTutorialAction);
            }
        }
    }

    //Puts items into their original order
    public void ResetItemsOrder()
    {
        if (frame_data.doHighlight)
        {
            frame_data.targetObject.transform.SetSiblingIndex(target_ui_sibling);
        }

        if (frame_data.useArrow)
        {
            arrowUI.transform.SetSiblingIndex(arrow_ui_sibling);
        }

        if (!icon_ui_has_parent)
        {
            if (hasIconUI)
            {
                iconUI.transform.SetSiblingIndex(icon_ui_sibling);
            }
        }
        else
        {
            icon_ui_parent.transform.SetSiblingIndex(icon_parent_sibling);
        }

        if (!text_ui_has_parent)
        {
            if (hasTextUI && textUI != null)
            {
                textUI.transform.SetSiblingIndex(text_ui_sibling);
            }
        }
        else
        {
            text_ui_parent.transform.SetSiblingIndex(text_parent_sibling);
        }

        if (hasDarkBackgroundUI && darkBackgroundUI != null)
        {
            darkBackgroundUI.transform.SetSiblingIndex(dark_ui_sibling);
        }

        if (hasExceptions)
        {
            for (int i = 0; i < frame_data.exceptionsList.Count; i++)
            {
                frame_data.exceptionsList[i].gameObject.transform.SetSiblingIndex(frame_data.exceptionsList[i].default_index);
            }
        }
    }

    //Plays audio
    public void ConfigureAudio()
    {
        if (tutorial.isPlaying)
        {
            if (hasAudioSource && audioSource != null)
            {
                if (frame_data.useAudio)
                {
                    audioSource.clip = frame_data.audioClip;

                    if (frame_data.moreAudioSettings)
                    {
                        audioSource.outputAudioMixerGroup = frame_data.audio_clip_output;
                        audioSource.loop = frame_data.audio_enable_loop;
                        audioSource.volume = frame_data.audio_volume;
                    }

                    audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                }
            }
            else
            {
                TMDebugLog("AudioSource is null. Skipping...");
            }
        }
        else
        {
            if (hasAudioSource && audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }

    //Changes icon and sets an appearance effect
    public void ConfigureIcon()
    {
        if (tutorial.isPlaying)
        {
            if (hasIconUI && iconUI != null)
            {
                if (frame_data.useIcon)
                {
                    iconUI.SetActive(true);
                    iconUI.GetComponent<Image>().sprite = frame_data.iconSprite;

                    //if fade in has been enabled
                    if (frame_data.icon_fadeIn)
                    {
                        StartCoroutine(tmE.effect_fade_in(iconUI, frame_data.icon_fade_speed, false, skipEffects));
                    }

                    //if fly-in has been enabled
                    if (frame_data.icon_flyIn)
                    {
                        //Save description UI's initial position for future use
                        Vector3 iconUItargetPos;
                        iconUItargetPos = iconUI.transform.position;

                        //Initialize Fly-In Effect
                        StartCoroutine(tmE.effect_fly_in(iconUI, frame_data.icon_flyInDirection, iconUItargetPos, frame_data.icon_fly_speed, frame_data.icon_warpValue, skipEffects));
                    }
                }
                else
                {
                    iconUI.SetActive(false);
                }
            }
            else
            {
                TMDebugLog("IconUI is null. Skipping...");
            }
        }
        else
        {
            if (hasIconUI) { iconUI.SetActive(false); }
        }
    }

    //Changes text and sets an appearance effect
    public void ConfigureText()
    {
        if (tutorial.isPlaying)
        {
            if (hasTextUI && textUI != null)
            {
                if (frame_data.useText)
                {
                    if (textUI != null)
                    {
                        textUI.SetActive(true);

                        //if typing has been enabled
                        if (frame_data.text_typing)
                        {
                            //StartCoroutine(effect_typing_in());
                        }
                        else
                        {
                            textUI.GetComponent<Text>().text = frame_data.descriptionText;
                        }

                        //if fade in has been enabled
                        if (frame_data.text_fadeIn)
                        {
                            StartCoroutine(tmE.effect_fade_in(textUI, frame_data.text_fade_speed, true, skipEffects));
                        }

                        //if fly-in has been enabled
                        if (frame_data.text_flyIn)
                        {
                            //save description UI's initial position for future use
                            Vector3 textUITargetPos;
                            textUITargetPos = textUI.transform.position;

                            //Initiate the fly-in effect
                            StartCoroutine(tmE.effect_fly_in(textUI, frame_data.text_flyInDirection, textUITargetPos, frame_data.text_fly_speed, frame_data.icon_warpValue, skipEffects));
                        }
                    }
                }
                else
                {
                    textUI.SetActive(false);
                }
            }
            else
            {
                TMDebugLog("TextUI is null. Skipping...");
            }
        }
        else
        {
            if (hasTextUI && textUI != null)
            {
                textUI.SetActive(false);
            }
        }
    }

    //Shows an outline of a target
    public void OutlineTarget()
    {
        if (tutorial.isPlaying)
        {
            if (frame_data.doHighlight)
            {
                if (frame_data.target_outline)
                {
                    frame_data.targetObject.AddComponent<Outline>();
                    frame_data.targetObject.GetComponent<Outline>().effectColor = frame_data.outlineColor;
                    frame_data.targetObject.GetComponent<Outline>().effectDistance = new Vector2(frame_data.outlineThickness, frame_data.outlineThickness);
                }
            }
        }
        else
        {
            if (frame_data.targetObject != null)
            {
                Destroy(frame_data.targetObject.GetComponent<Outline>());
            }
        }
    }

    //Remove an out line off the target
    public void RemoveOutlines()
    {
        if (frame_data.targetObject != null)
        {
            if (frame_data.targetObject.GetComponent<Outline>() != null)
            {
                Destroy(frame_data.targetObject.GetComponent<Outline>());
            }
        }
    }

    public void PointArrowAtTarget()
    {
        if (frame_data.doHighlight)
        {
            if (frame_data.useArrow)
            {
                tmA.PointArrow(arrowUI, frame_data.targetObject, frame_data.arrow_pointDirection, frame_data.arrowOffset_x, frame_data.arrowOffset_y);

                if (frame_data.arrow_useFadeIn)
                {
                    StartCoroutine(tmE.effect_fade_in(arrowUI, frame_data.arrow_fade_speed, false, skipEffects));
                }
            }
        }
    }

    //Removes a pointing arrow from the target
    public void RemovePointingArrow()
    {
        TMArrow.DisableArrow(arrowUI);
    }

    //Updates tutorial progress variable
    public void UpdateTutorialProgress()
    {
        tutorial.progress = (float)(frame.id + 1) / tutorial_data.myFrames.Count;
    }

    //Starts a timer
    public void ConfigureTimer()
    {
        if (tutorial.isPlaying)
        {
            if (frame_data.useTimer)
            {
                Debug.Log(this);
                StartCoroutine(timer_start());
            }
        }
    }

    private IEnumerator timer_start()
    {
        int currentFrame = frame.id;

        yield return new WaitForSeconds(frame_data.timerAmount);

        if (currentFrame == frame.id)
        {
            frame.Next();
        }
    }

    public void TMDebugLog(string messageText)
    {
        if (data.enableLogs)
        {
            Debug.Log("Tutorial Master: " + messageText);
        }
    }
}