#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#if UNITY_5_3
using UnityEditor.SceneManagement;
#endif

using UnityEditor;
using UnityEditor.SceneManagement;

[SerializeField]
[CustomEditor(typeof(TutorialMasterScript))]
public class TutorialMasterInspector : Editor
{
	//Classes variables
	private TutorialMasterScript tm;                                                //variable which is going to be used to simplify the code

	private TutorialMasterScript.frameClass frameData;                              //variable which has access to the frame data
	private TutorialMasterScript.tutorialClass tutorialData;                        //variable which has access to the tutorial  data (frame data included)

	//Editor Variables
	private Vector3 tutorialsListVector = Vector3.zero;                             //used for scrolling of the tutorials list

	private Vector3 frameClassListVector = Vector3.zero;                            //used for scrolling of the frames list
	private Vector3 exceptionsListVector = Vector3.zero;                            //used for scrolling of the exceptions list

	private bool showBaseSettings = true;

	public Rect frames_list_drop_area;                                      //used to specify the area in the custom inspector where GameObjects could be dropped

	//Custom GUI variables
	private GUIStyle infoText = new GUIStyle();                                     //text which shows brief info on tutorials and frames (e.g ID)

	private GUIStyle boldText = new GUIStyle();                                     //text which acts like a header in the custom inspector
	private GUIStyle errorText = new GUIStyle();                                    //text that displays any related errors tutorial/frame may have (e.g a missing GameObject)
	private GUIStyle customTextField;                                               //custom style for a multiline text field

	private GUISkin tmSkin;                                                         //declares a custom Tutorial Master skin

	//GUI Resources variables are declared here

	//Sprites of a button when it's in debug state and is currently being played
	public Texture2D debug_button_normal;                                   //sprite of a button in its normal state

	public Texture2D debug_button_pressed;                                  //sprite of a button when a mouse has clicked on it
	public Texture2D debug_button_selected;                                 //sprite of a button when a it has been selected (normally same as debug_button_pressed)

	//Sprites of an ordinary button
	public Texture2D default_button_normal;                                 //sprite of a button in its normal state

	public Texture2D default_button_pressed;                                //sprite of a button when a mouse has clicked on it
	public Texture2D default_button_selected;                               //sprite of a button when a it has been selected (normally same as debug_button_pressed)

	public Texture2D icon_error;                                            //sprite of an icon error that is being displayed alongside a tutorial/frame name when there is an error

	//Run the following code when the TutorialMasterScript component is added (runs once)
	public void OnEnable()
	{
		TutorialMasterScript tm = (TutorialMasterScript)target;

		//Assigns a Tutorial Master tag to the GameObject if it hans't been assigned yet
		if (tm.tag != "Tutorial Master")
		{
			tm.GenerateTag("Tutorial Master");
			tm.tag = "Tutorial Master";
		}

		EditorGUIUtility.labelWidth = 150;                                  //configure the distance between the label and the GUI input field
	}

	[SerializeField]

	//Everytime user interacts with the custom inspector, the code below starts running
	public override void OnInspectorGUI()
	{
		tm = (TutorialMasterScript)target;                                  //assign TutorialMasterScript to tm to simplify

		//Load resources and assign skin
		if (EditorGUIUtility.isProSkin)                                     //checks whether user has a dark skin or a light skin enabled
		{
			//Loads appropriate resouces for the pro skin user
			tmSkin = (GUISkin)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/TutorialMasterSkin_Pro.guiskin", typeof(GUISkin));

			//Configure the bold text
			boldText.fontStyle = FontStyle.Bold;
			boldText.normal.textColor = Color.white;
		}
		else
		{
			tmSkin = (GUISkin)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/TutorialMasterSkin.guiskin", typeof(GUISkin));

			//Configure the bold text
			boldText.fontStyle = FontStyle.Bold;
		}

		//Configure the info text
		infoText.fontSize = 10;
		infoText.alignment = TextAnchor.MiddleRight;

		//Configure the error text
		errorText.normal.textColor = Color.red;
		errorText.wordWrap = true;

		//Load actual sprites of button when it's in debug mode of its all 3 states
		debug_button_normal = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_debug_normal.png", typeof(Texture2D));
		debug_button_pressed = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_debug_pressed.png", typeof(Texture2D));
		debug_button_selected = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_debug_pressed.png", typeof(Texture2D));

		//Load actual sprites of button of its all 3 states
		default_button_normal = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_button_normal.png", typeof(Texture2D));
		default_button_pressed = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_button_pressed.png", typeof(Texture2D));
		default_button_selected = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/tm_button_pressed.png", typeof(Texture2D));

		//Load sprite of an error icon
		icon_error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Tutorial Master/Internal Sprites/Skins/SkinItems/icon_error.png", typeof(Texture2D));

		//Finally assign the Tutorial Master skin to the Custom Inspector
		GUI.skin = tmSkin;

		//ACTUAL INSPECTOR GUI STARTS NOW ==========================================================================================================

		/*
		 * Each main Custom Inspector GUI setting has been split into separate functions
		 * This makes it easier for a developer to manage the souce code
		*/

		DrawBaseSettings();

		DrawGeneralSettings();

		EditorGUILayout.LabelField("Tutorial Settings", boldText);

		//This button would CREATE A NEW TUTORIAL
		if (GUILayout.Button(new GUIContent("Create a new Tutorial", "Creates a new Tutorial"), "button", GUILayout.Height(30)))
		{
			Undo.RecordObject(tm, "Creating a New Tutorial");                                   //Firstly record the undo history
			tm.showTutorialsList = true;                                                        //Ensure that tutorial settings would be shown
			tm.selectedTutorialIndex = tm.myTutorials.Count;                                    //Set the selected tutorial to the new one
			tm.tutorialSelected = true;                                                         //Make tutorial selected set to true
			tm.frameSelected = false;                                                           //Deselect frame to avoid errors
			tm.CreateTutorial();                                                                //Actually start creating a tutorial
			GUI.FocusControl(null);                                                             //Remove any GUI focus (e.g mouse in the input field) to avoid confusion
			tutorialsListVector.y = Mathf.Infinity;                                             //Make sure that the tutorials list is scrolled to the very bottom
			MarkEditorDirty();                                                                  //Mark editor dirty to record the newly created object
		}

		DrawTutorialsList();

		//TUTORIAL SETTINGS
		if (tm.tutorialSelected)                                                                //draw tutorial settings if the user has selected a tutorial
		{
			tutorialData = tm.myTutorials[tm.selectedTutorialIndex];

			//Tutorial name + info
			DrawTutorialBaseSettings();

			//'Add Frame' button
			EditorGUILayout.LabelField("Frame Settings", boldText);
			if (GUILayout.Button(new GUIContent("Add Frame", "Adds a new frame to a selected tutorial"), GUILayout.Height(30)))
			{
				Undo.RecordObject(tm, "Adding a New Frame");
				tm.selectedFrameIndex = tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count;
				tm.AddFrame(tm.selectedTutorialIndex);
				tm.frameSelected = true;
				GUI.FocusControl(null);
				frameClassListVector.y = Mathf.Infinity;
				MarkEditorDirty();
			}

			DrawFrameList();

			//FRAME SETTINGS
			if (tm.frameSelected)
			{
				//Shorten variable to make it easier to refer to frame data when needed
				frameData = tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex];

				//Start drawing frame settings

				DrawFrameBaseSettings();

				DrawTimerSettings();

				DrawDarkBackgroundSettings();

				DrawHighlightSettings();

				DrawTextIconSettings();

				DrawAudioSettings();

				//Check if frame isn't missing anything and is ready to be used for tutorial
				frame_checkValidity();
			}
		}

		//Check if all frames of a tutorial are valid and ready to be used
		tutorial_checkValidity();

		//Mark Scene Dirty and record undo history in case GUI has been changed
		if (GUI.changed)
		{
			MarkEditorDirty();
			Undo.RegisterCompleteObjectUndo(tm, "Tutorial Master Changes");
		}
	}

	//Marks editor dirty depending on the Unity version
	private void MarkEditorDirty()
	{
		if (!Application.isPlaying)                                                     //ensure that Unity isn't in play mode
		{
#if UNITY_5_3                                                                           //if the Unity is 5.3 or higher
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());     //run this bit of code
#elif UNITY_2017_1
			EditorSceneManager.MarkAllScenesDirty();
#endif
		}
	}

	//This is where frame's validity is being assessed as user works in the custom inspector
	public void frame_checkValidity()
	{
		//Check if some GameObjects are null
		tm.hasAudioSource = (tm.audioSource != null);
		tm.hasIconUI = (tm.iconUI != null);
		tm.hasTextUI = (tm.textUI != null);
		tm.hasDarkBackgroundUI = (tm.darkBackgroundUI != null);
		tm.hasArrowUI = (tm.arrowUI != null);

		//Check if frame is fully ready

		//icon sprite
		if (frameData.useIcon)
		{
			frameData.iconReady = (frameData.iconSprite != null);
		}
		else
		{
			frameData.iconReady = true;
		}

		//highlight target
		if (frameData.doHighlight)
		{
			frameData.targetReady = (frameData.targetObject != null);
		}
		else
		{
			frameData.targetReady = true;
		}

		//audio clip
		if (frameData.useAudio)
		{
			frameData.audioReady = (frameData.audioClip != null);
		}
		else
		{
			frameData.audioReady = true;
		}

		//Check if frame is ready
		if (frameData.targetReady && frameData.audioReady && frameData.iconReady)
		{
			frameData.isReady = true;
		}
		else
		{
			frameData.isReady = false;
		}
	}

	//This is where tutorial's validity is being assessed as user works in the custom inspector
	public void tutorial_checkValidity()
	{
		for (int a = 0; a < tm.myTutorials.Count; a++)
		{
			if (tm.myTutorials[a].myFrames.Count > 0)
			{
				int correct_count = 0;

				for (int b = 0; b < tm.myTutorials[a].myFrames.Count; b++)
				{
					if (tm.myTutorials[a].myFrames[b].isReady)
					{
						correct_count += 1;
					}
				}

				if (correct_count == tm.myTutorials[a].myFrames.Count)
				{
					tm.myTutorials[a].isReady = true;
				}
				else
				{
					tm.myTutorials[a].isReady = false;
				}
			}
			else
			{
				tm.myTutorials[a].isReady = true;
			}
		}
	}

	//INSPECTOR GUI FUNCTIONS

	public void DrawBaseSettings()
	{
		EditorGUILayout.Space();
		showBaseSettings = EditorGUILayout.Foldout(showBaseSettings, "Settings");

		if (showBaseSettings)
		{
			EditorGUI.indentLevel++;
			EditorGUIUtility.labelWidth = 160;
			tm.enable_logs = EditorGUILayout.Toggle("Enable Logs", tm.enable_logs);
			tm.save_progress = EditorGUILayout.Toggle("Track Tutorial Progress", tm.save_progress);
			EditorGUI.indentLevel--;
		}
	}

	//Draws general settings (e.g UI components to attach)
	public void DrawGeneralSettings()
	{
		EditorGUILayout.Space();                                    //leave some space

		//UI SETTINGS
		EditorGUILayout.LabelField("UI Settings", boldText);
		tm.UICanvas = (GameObject)EditorGUILayout.ObjectField(new GUIContent("UI Canvas", "A Canvas where a Tutorial is going to be played"), tm.UICanvas, typeof(GameObject), true);

		if (tm.UICanvas == null)                                    //ensure that the UI canvas isn't empty before showing settings
		{
			//Draw Help Box to help user
			EditorGUILayout.HelpBox("Attach the UI Canvas to get started", MessageType.Warning);
			tm.isValid = false;                                     //disable the validity of the Tutorial Master
			GUI.enabled = false;                                    //disable GUI below to prevent user from modifying the input below
		}
		else
		{
			tm.isValid = true;                                      //otherwise enable the validity of the Tutorial Master
		}

		//Draw UI Components settings
		tm.darkBackgroundUI = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Dark Background UI", "An image which is going to be used to cover unnecessary UI components during the tutorial. This can be left empty if you don't need to darken background"), tm.darkBackgroundUI, typeof(GameObject), true);
		tm.arrowUI = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Arrow UI", "Arrow UI which is used to point at a UI target. This can be left empty if you don't need to point an arrow at UI components"), tm.arrowUI, typeof(GameObject), true);
		tm.textUI = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Text UI", "UI component with a Text component attached to it. Will be used to display instruction text. Can be left empty if you won't need to show instructions to the player"), tm.textUI, typeof(GameObject), true);
		tm.iconUI = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Icon UI", "Image UI used to display icons during the tutorial. Can be left empty if you won't need to show images to the player"), tm.iconUI, typeof(GameObject), true);

		if (tm.UICanvas != null)                                                                                //check if UI Canvas isn't empty
		{
			if (tm.iconUI != null)                                                                              //check if ICON UI isn't null
			{
				if (tm.iconUI.transform.parent != null && tm.iconUI.transform.parent != tm.UICanvas.transform)  //check whether ICON UI has a parent apart from Canvas
				{
					tm.icon_ui_has_parent = true;                                                               //say that ICON UI has a parent
					tm.icon_ui_parent = tm.iconUI.transform.parent.gameObject;                                  //assign ICON UI's parent
				}
				else                                                                                            //otherwise...
				{
					tm.icon_ui_has_parent = false;                                                              //set it false
					tm.icon_ui_parent = null;                                                                   //ensure that ICON_UI_PARENT is empty
				}
			}

			if (tm.textUI != null)                                                                              //check if TEXT UI isn't null
			{
				if (tm.textUI.transform.parent != null && tm.textUI.transform.parent != tm.UICanvas.transform)  //check whether TEXT UI has a parent apart from Canvas
				{
					tm.text_ui_has_parent = true;                                                               //say that TEXT UI has a parent
					tm.text_ui_parent = tm.textUI.transform.parent.gameObject;                                  //assign TEXT UI's parent
				}
				else                                                                                            //otherwise...
				{
					tm.text_ui_has_parent = false;                                                              //set it false
					tm.text_ui_parent = null;                                                                   //ensure that TEXT_UI_PARENT is empty
				}
			}
		}

		GUI.enabled = true;

		//AUDIO SOURCE SETTINGS
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Audio Source", boldText);
		//Show an Audio Source field to input audio source
		tm.audioSource = (AudioSource)EditorGUILayout.ObjectField(new GUIContent("Audio Source", "This is where audio clips are going to be played during the tutorial. You can attach an existing source or generate your own. You can leave it empty if you don't want to play any additional audio clips"), tm.audioSource, typeof(AudioSource), true);

		//Check whether or not Canvas has been assigned
		if (tm.UICanvas != null)
		{
			//Then check if any of UI components hasn't been asigned yet
			if (tm.darkBackgroundUI == null || tm.textUI == null || tm.iconUI == null || tm.audioSource == null || tm.arrowUI == null)
			{
				//Show a "Generate Missing Components" button in case something is missing
				if (GUILayout.Button(new GUIContent("Generate Missing Components", "Automatically creates and assigns missing UI elements. Requires UI Canvas to be attached. Generated components can always be modified to your needs"), GUILayout.Height(23)))
				{
					//Generates missing components if the button has been pressed
					tm.GenerateMissingComponents();
				}
			}
		}

		EditorGUILayout.Space();
	}

	//This is where TUTORIALS LIST is being drawn
	public void DrawTutorialsList()
	{
		//TUTORIALS LIST--------------------------------------------------------------------------------------------------------------------------------------------------------------
		if (tm.myTutorials.Count > 0)
		{
			EditorGUILayout.BeginVertical("box", GUILayout.MinHeight(150));                                 //Enclose the whole setting into a box with a minimum height of 150 pixels
			tutorialsListVector = EditorGUILayout.BeginScrollView(tutorialsListVector);                     //Begin a scroll area where user can scroll

			//List all tutorials
			for (int i = 0; i < tm.myTutorials.Count; i++)
			{
				/*
				if (data.enableLogs)
				{
					if (tutorial.id == i && tutorial.isPlaying)
					{
						//Change button skin to debug accordingly
						if (tm.selectedTutorialIndex == i) { tmSkin.button.normal.background = debug_button_selected; } else { tmSkin.button.normal.background = debug_button_normal; }
					}
					else
					{
						//Change button skin back to normal
						if (tm.selectedTutorialIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }
					}
				}
				else
				{
					//Change button skin back to normal
					if (tm.selectedTutorialIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }
				}
				*/

				//Change button skin back to normal
				if (tm.selectedTutorialIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }

				EditorGUILayout.BeginHorizontal();  //start the horizontal area. This is where Tutorial settings such as "Delete" are being dispplayed alongside with a tutorial name

				/*
				//Check if button is debugged and the game is playing
				if (tutorial.isPlaying && Application.isPlaying)
				{
					if (data.enableLogs)
					{
						if (tutorial.id == i)       //check whether or not the tutorial is being played in the game
						{
							//Assign debug skins to a button
							tmSkin.button.normal.background = debug_button_normal;
							tmSkin.button.active.background = debug_button_pressed;
						}
						else
						{
							//Assign normal skins to a button
							tmSkin.button.normal.background = default_button_normal;
							tmSkin.button.active.background = default_button_pressed;
						}
					}
				}
				*/

				//Selects a tutorial
				if (!tm.myTutorials[i].isReady)         //check whether or not the tutorial's frames aren't missing any important components
				{
					//Draws a tutorial button with a specified name AND an error icon along with it
					if (GUILayout.Button(new GUIContent("" + tm.myTutorials[i].tutorialName, icon_error), GUILayout.Height(23)))
					{
						if (tm.selectedTutorialIndex != i)
						{
							tm.frameSelected = false;   //deselect a frame if the current tutorial is not selected
						}

						tm.selectedTutorialIndex = i;
						tm.tutorialSelected = true;
						GUI.FocusControl(null);
					}
				}
				else
				{
					//Draws a tutorial button with a specified name
					if (GUILayout.Button(tm.myTutorials[i].tutorialName, GUILayout.Height(23)))
					{
						if (tm.selectedTutorialIndex != i)
						{
							tm.frameSelected = false;
						}

						tm.selectedTutorialIndex = i;
						tm.tutorialSelected = true;
						GUI.FocusControl(null);
					}
				}

				tmSkin.button.normal.background = default_button_normal;
				tmSkin.button.active.background = default_button_pressed;

				GUI.contentColor = new Color(1, 0, 0);                                              //changes the color of the GUI content to red
				GUI.skin.button.normal.textColor = Color.white;                                     //changes the button color to white
				GUI.skin.button.active.textColor = Color.red;                                       //changes the button text color to red
																									//Deletes a tutorial associated with it
				if (GUILayout.Button(new GUIContent("X", "Deletes the Tutorial"), GUILayout.Width(23), GUILayout.Height(23)))
				{
					Undo.RecordObject(tm, "Deleting a Tutorial");

					if (tm.selectedTutorialIndex == i)                                              //check if user is trying to delete a tutorial that has been already selected
					{
						if (tm.myTutorials.Count > 1)                                               //check if selected tutorial isn't the only tutorial on the list
						{
							if (i != 0)                                                             //check if selected tutorial isn't the first on the list
							{
								tm.selectedTutorialIndex -= 1;                                      //if not, select tutorial before it
							}
							else
							{
								tm.selectedTutorialIndex = 0;                                       //if yes, select the first tutorial on the list
							}

							if (tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count > 0)        //check if the newly selected tutorial has more than one frame
							{
								tm.selectedFrameIndex = 0;                                          //if yes, select the first one
								tm.frameSelected = true;                                            //...and show frame settings
							}
							else
							{
								tm.frameSelected = false;                                           //if nhide frame settings
							}

							tm.tutorialSelected = true;                                             //show tutorial settings
						}
						else                                                                        //if selected tutorial is last on the list...
						{
							tm.tutorialSelected = false;                                            //then hide tutorial settings
							tm.frameSelected = false;                                               //...and hide frame settings
						}
					}

					tm.myTutorials[i].myFrames.Clear();                             //delete all frames from the tutorial
					tm.DeleteTutorial(i);                                           //delete the tutorial
				}

				//Resets button colors
				GUI.contentColor = Color.white;
				GUI.skin.button.normal.textColor = new Color(0, 0, 0, 1);
				GUI.skin.button.active.textColor = Color.black;
				EditorGUILayout.EndHorizontal();
			}

			//Finish the tutorials list
			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndVertical();
		}
		else
		{
			EditorGUILayout.HelpBox("Looks like no tutorial has been created. Click 'Create a new Tutorial' to get started", MessageType.Info);
		}
	}

	//This is where TUTORIAL BASE SETTINGS are being drawn (e.g input name for the tutorial)
	public void DrawTutorialBaseSettings()
	{
		EditorGUILayout.BeginVertical("box");               //enclose the tutorial settings into a box
		EditorGUILayout.LabelField("General", boldText);    //header text
		tutorialData.tutorialName = EditorGUILayout.TextField("Tutorial Name", tutorialData.tutorialName);      //input field for tutorial's name
		EditorGUIUtility.labelWidth = 150;                  //configure the distance between the label and the GUI input field

		//shows the id and the number of frames a tutorial has
		EditorGUILayout.LabelField("ID: " + tm.selectedTutorialIndex + "   Number of frames: " + tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count, infoText);

		//check if tutorial has more than 0 frames
		if (!tm.myTutorials[tm.selectedTutorialIndex].isReady && tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count > 0)
		{
			EditorGUILayout.HelpBox("This tutorial will not run because one or more components are missing in one of its frames", MessageType.Error);
		}

		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
	}

	public void DrawFrameList()
	{
		if (tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count > 0)                                //check if there are more than 0 frames in the tutorial
		{
			frames_list_drop_area = EditorGUILayout.BeginVertical("box", GUILayout.MinHeight(150));     //creates a drop area where frames could be dropped and also encloses it into a box
			frameClassListVector = EditorGUILayout.BeginScrollView(frameClassListVector);               //begins scrollview for the frames list

			//List all frames
			for (int i = 0; i < tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count; i++)
			{
				/*
				if (data.enableLogs)                                                                    //check if logging is enabled and whether the current frame is being played
				{
					if (frame.id == i && tutorial.id == tm.selectedTutorialIndex)                                            //check if selected frame is being played
					{
						//Change button skin to debug accordingly
						if (tm.selectedFrameIndex == i) { tmSkin.button.normal.background = debug_button_selected; } else { tmSkin.button.normal.background = debug_button_normal; }
					}
					else
					{
						//Change button skin back to normal
						if (tm.selectedFrameIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }
					}
				}
				else
				{
					//Change button skin back to normal
					if (tm.selectedFrameIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }
				}
				*/

				//Change button skin back to normal
				if (tm.selectedFrameIndex == i) { tmSkin.button.normal.background = default_button_selected; } else { tmSkin.button.normal.background = default_button_normal; }

				EditorGUILayout.BeginHorizontal();

				/*
				if (tutorial.isPlaying && Application.isPlaying)
				{
					if (data.enableLogs)
					{
						if (frame.id == i)
						{
							tmSkin.button.normal.background = debug_button_normal;
							tmSkin.button.active.background = debug_button_pressed;
						}
						else
						{
							tmSkin.button.normal.background = default_button_normal;
							tmSkin.button.active.background = default_button_pressed;
						}
					}
				}
				*/

				//Select a frame
				if (!tm.myTutorials[tm.selectedTutorialIndex].myFrames[i].isReady)
				{
					if (GUILayout.Button(new GUIContent("" + tm.myTutorials[tm.selectedTutorialIndex].myFrames[i].frameName, icon_error), GUILayout.Height(23)))
					{
						tm.selectedFrameIndex = i;
						tm.frameSelected = true;

						if (tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].doHighlight == true && tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].targetObject != null)
						{
							EditorGUIUtility.PingObject(tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].targetObject);
						}
						GUI.FocusControl(null);
					}
				}
				else
				{
					if (GUILayout.Button(tm.myTutorials[tm.selectedTutorialIndex].myFrames[i].frameName, GUILayout.Height(23)))
					{
						tm.selectedFrameIndex = i;
						tm.frameSelected = true;

						if (tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].doHighlight == true && tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].targetObject != null)
						{
							EditorGUIUtility.PingObject(tm.myTutorials[tm.selectedTutorialIndex].myFrames[tm.selectedFrameIndex].targetObject);
						}
						GUI.FocusControl(null);
					}
				}
				tmSkin.button.normal.background = default_button_normal;
				tmSkin.button.active.background = default_button_pressed;

				//Moves a frame down in list
				if (i == (tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count - 1))
				{
					GUI.enabled = false;
				}
				if (tm.selectedFrameIndex == i)
				{
					if (GUILayout.Button(new GUIContent("↓", "Move a frame down the list"), GUILayout.Width(23), GUILayout.Height(23)))
					{
						Undo.RecordObject(tm, "Reordering a Frame");
						tm.MoveFrameDown(i);

						if (tm.selectedFrameIndex == i)
						{
							tm.selectedFrameIndex = (i + 1);
						}
					}
					GUI.enabled = true;

					//Moves a frame up in list
					if (i == 0)
					{
						GUI.enabled = false;
					}
					if (GUILayout.Button(new GUIContent("↑", "Move a frame up the list"), GUILayout.Width(23), GUILayout.Height(23)))
					{
						GUI.enabled = false;
						Undo.RecordObject(tm, "Reordering a Frame");
						tm.MoveFrameUp(i);

						if (tm.selectedFrameIndex == i)
						{
							tm.selectedFrameIndex = (i - 1);
						}
					}
				}

				GUI.enabled = true;

				//Deletes a frame
				GUI.contentColor = new Color(1, 0, 0);
				GUI.skin.button.normal.textColor = Color.white;
				GUI.skin.button.active.textColor = Color.red;
				if (GUILayout.Button(new GUIContent("X", "Deletes the frame"), GUILayout.Width(23), GUILayout.Height(23)))
				{
					Undo.RecordObject(tm, "Deleting a Frame");

					if (tm.selectedFrameIndex == i)                                                 //check if user is trying to delete a frame that has been already selected
					{
						if (tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count > 1)            //check if selected frame isn't the only frame on the list
						{
							if (i != 0)                                                             //check if selected frame isn't the first on the list
							{
								tm.selectedFrameIndex -= 1;                                         //if not, select frame before it
							}
							else
							{
								tm.selectedFrameIndex = 0;                                          //if yes, select the first frame on the list
							}
						}
						else                                                                        //if selected frame is last on the list...
						{
							tm.frameSelected = false;                                               //hide frame settings
							tm.selectedFrameIndex = 0;                                              //reset frame index
						}
					}

					tm.RemoveFrame(tm.selectedTutorialIndex, i);                                    //delete selected frame
				}

				GUI.contentColor = Color.white;
				GUI.skin.button.normal.textColor = new Color(0, 0, 0, 1);
				GUI.skin.button.active.textColor = Color.black;
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndVertical();

			Event current_event = Event.current;

			switch (current_event.type)
			{
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if (!frames_list_drop_area.Contains(current_event.mousePosition))
						return;

					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

					if (current_event.type == EventType.DragPerform)
					{
						DragAndDrop.AcceptDrag();

						for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
						{
							if (DragAndDrop.objectReferences[i].GetType() == typeof(GameObject)) //check if it's of GameObject type
							{
								Undo.RecordObject(tm, "Adding a New Frame");

								tm.AddFrame(tm.selectedTutorialIndex);

								tutorialData.myFrames[tutorialData.myFrames.Count - 1].targetObject = (GameObject)DragAndDrop.objectReferences[i];
								tutorialData.myFrames[tutorialData.myFrames.Count - 1].frameName = "Frame of " + DragAndDrop.objectReferences[i].name;

								tutorialData.myFrames[tutorialData.myFrames.Count - 1].doHighlight = true;
								tutorialData.myFrames[tutorialData.myFrames.Count - 1].isReady = true;
								//tm.selectedFrameIndex = tm.myTutorials[tm.selectedTutorialIndex].myFrames.Count - 1;
								//tm.frameSelected = false;
								GUI.FocusControl(null);
								frameClassListVector.y = Mathf.Infinity;
								MarkEditorDirty();
							}
						}
					}
					break;
			}
		}
		else
		{
			EditorGUILayout.HelpBox("Looks like no frame has been created. Click 'Add Frame' to get started", MessageType.Info);
		}
	}

	public void DrawFrameBaseSettings()
	{
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("General Settings", boldText);
		frameData.frameName = EditorGUILayout.TextField("Frame Name", frameData.frameName);
		EditorGUILayout.LabelField("Frame ID: " + tm.selectedFrameIndex, infoText);

		if (!frameData.iconReady)
		{
			EditorGUILayout.LabelField("Icon sprite is missing", errorText);
		}

		if (!frameData.targetReady)
		{
			EditorGUILayout.LabelField("Highlight target gameobject has not been assigned", errorText);
		}

		if (!frameData.audioReady)
		{
			EditorGUILayout.LabelField("Audio clip is missing", errorText);
		}

		frameData.allowEffectSkip = EditorGUILayout.Toggle(new GUIContent("Tap to Skip Effects", "While true, player can tap or click a screen to skip any appearance effects such as fade in, fly in etc."), frameData.allowEffectSkip);

		EditorGUILayout.EndVertical();
	}

	public void DrawTimerSettings()
	{
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Timer Settings", boldText);

		frameData.useTimer = EditorGUILayout.Toggle("Enable Timer", frameData.useTimer);
		GUI.enabled = frameData.useTimer;
		EditorGUILayout.HelpBox("Number of seconds for Tutorial Master to wait until it automatically initiates tutorial.Next() command", MessageType.Info);
		EditorGUILayout.HelpBox("1 Second = 1000 Milliseconds", MessageType.None);
		frameData.timerAmount = EditorGUILayout.FloatField("Timer Amount (sec)", frameData.timerAmount);
		GUI.enabled = true;
		EditorGUILayout.EndVertical();
	}

	public void DrawDarkBackgroundSettings()
	{
		EditorGUILayout.BeginVertical("box");

		EditorGUILayout.LabelField("Dark Background Settings", boldText);

		GUI.enabled = tm.hasDarkBackgroundUI;

		frameData.useBackground = EditorGUILayout.Toggle(new GUIContent("Darken Background", "If set True, a Dark Background would appear and cover UI (apart from the Highlight Target, UI in the Exception List, Arrow UI, Icon UI and Text UI"), frameData.useBackground);

		if (frameData.useBackground)
		{
			if (frameData.useBackground)
			{
				//Dark Background
				frameData.fadeInBackground = EditorGUILayout.Toggle(new GUIContent("Fade In Background", "If checked, the Dark Background is going to smoothly fade in. Recommended to be used only once"), frameData.fadeInBackground);
				if (frameData.fadeInBackground)
				{
					EditorGUI.indentLevel++;
					frameData.background_fade_speed = EditorGUILayout.FloatField("Fade In Speed", frameData.background_fade_speed);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.Space();

				frameData.addExceptions = EditorGUILayout.Toggle("Add Exceptions", frameData.addExceptions);
				EditorGUILayout.HelpBox("UI Components inside of the Exceptions List won't be affected by the DarkBackground UI", MessageType.None);
				if (frameData.addExceptions)
				{
					DrawExceptionsList();
				}

				EditorGUILayout.Space();
			}
		}

		GUI.enabled = true;

		if (!tm.hasDarkBackgroundUI)
		{
			EditorGUILayout.HelpBox("Dark Background UI must be attached in order for this feature to work", MessageType.Warning);
			frameData.useBackground = false;
		}

		EditorGUILayout.EndVertical();
	}

	public void DrawHighlightSettings()
	{
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Highlight Settings", boldText);

		if (!frameData.targetObject && !frameData.useBackground)
		{
			EditorGUILayout.HelpBox("Nothing will be highlighted for this frame", MessageType.Info);
		}

		frameData.doHighlight = EditorGUILayout.Toggle("Highlight Target", frameData.doHighlight);

		if (frameData.doHighlight)
		{
			frameData.targetObject = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Target", "UI which is going to be highlighted in this frame.It may be a button or even the whole UI parent"), frameData.targetObject, typeof(GameObject), true);

			if (frameData.targetObject == null)
			{
				EditorGUILayout.HelpBox("Target GameObject must be attached first!", MessageType.Warning);
				GUI.enabled = false;
			}
			else
			{
				frameData.highlight_target_is_null = false;
			}

			if (frameData.doHighlight)
			{
				if (frameData.targetObject != null)
				{
					if (frameData.targetObject.GetComponent<Button>() == null)
					{
						EditorGUILayout.HelpBox("The option below has been disabled because the highlight target is not a button (or doesn't have a 'Button' component attached to it)", MessageType.Info);
						frameData.detectUIClicks = false;
						GUI.enabled = false;
					}
				}
			}
			else
			{
				frameData.detectUIClicks = false;
			}

			frameData.detectUIClicks = EditorGUILayout.Toggle(new GUIContent("Detect UI Clicks", "Once checked, if a highlight target is a UI Button, a user can press it to play the next frame"), frameData.detectUIClicks);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Highlight Effects", boldText);

			if (!frameData.useArrow && !frameData.target_outline && !frameData.useBackground)
			{
				if (frameData.targetObject != null)
				{
					EditorGUILayout.HelpBox("Your target will not be highlighted with additional effects", MessageType.Info);
				}
			}

			EditorGUIUtility.labelWidth = 20;

			if (tm.arrowUI == null)
			{
				EditorGUILayout.HelpBox("Arrow UI must be attached in order for this feature to work", MessageType.Warning);
				frameData.useArrow = false;
			}

			EditorGUILayout.BeginHorizontal();

			GUI.enabled = (tm.arrowUI != null);
			frameData.useArrow = EditorGUILayout.ToggleLeft(new GUIContent("Point Arrow", "If set true, an Arrow would be pointing at the Highlight Target"), frameData.useArrow);

			if (Screen.width < 457)
			{
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
			}

			if (Screen.width < 457)
			{
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
			}

			frameData.target_outline = EditorGUILayout.ToggleLeft(new GUIContent("Outline", "If set True, Highlight Target would have an outline around it"), frameData.target_outline);

			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUIUtility.labelWidth = 150;

			GUI.enabled = true;

			//Point Arrow
			if (frameData.useArrow)
			{
				EditorGUILayout.LabelField("Point Arrow", boldText);

				frameData.arrow_pointDirection = (TutorialMasterScript.arrow_pointing_direction)EditorGUILayout.EnumPopup(new GUIContent("Pointing Direction", "Determines from which direction Arrow is going to point from. E.g if it's set 'Top', then an Arrow is going to be above the Highlight Target"), frameData.arrow_pointDirection);

				EditorGUILayout.LabelField("Position Offset");
				EditorGUI.indentLevel++;
				frameData.arrowOffset_x = EditorGUILayout.FloatField("X", frameData.arrowOffset_x);
				frameData.arrowOffset_y = EditorGUILayout.FloatField("Y", frameData.arrowOffset_y);
				EditorGUI.indentLevel--;

				EditorGUILayout.Space();

				frameData.arrow_useFloatingEffect = EditorGUILayout.Toggle(new GUIContent("Floating Effect", "If set True, an Arrow would slowly float (Recommended)"), frameData.arrow_useFloatingEffect);

				if (frameData.arrow_useFloatingEffect)
				{
					EditorGUI.indentLevel++;
					frameData.arrowFloating_Range = EditorGUILayout.FloatField(new GUIContent("Floating Range", "Determines how big is the floating distanc. Setting a high value is not recommended"), frameData.arrowFloating_Range);
					frameData.arrowFloating_Speed = EditorGUILayout.FloatField(new GUIContent("Floating Speed", "Determines how fast do you want an arrow to float. Setting a high value is not recommended"), frameData.arrowFloating_Speed);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.Space();

				frameData.arrow_useFadeIn = EditorGUILayout.Toggle(new GUIContent("Fade In Effect", "If true, an Arrow would slowly fade in (Recommended with Floating Effect enabled)"), frameData.arrow_useFadeIn);

				if (frameData.arrow_useFadeIn)
				{
					EditorGUI.indentLevel++;
					frameData.arrow_fade_speed = EditorGUILayout.FloatField(new GUIContent("Fade In Speed", "Determines how fast to fade an Arrow in. Setting it 0 would make Arrow invisible"), frameData.arrow_fade_speed);
					EditorGUI.indentLevel--;
				}

				EditorGUILayout.Space();

				frameData.arrow_renderTopMost = EditorGUILayout.Toggle(new GUIContent("Render Top Most", "If set True, an Arrow would be Rendered above anything including Text UI and Icon UI"), frameData.arrow_renderTopMost);

				EditorGUILayout.Space();
			}

			//Outline
			if (frameData.target_outline)
			{
				EditorGUILayout.LabelField("Outline", boldText);

				frameData.outlineColor = EditorGUILayout.ColorField(new GUIContent("Outline Color", "Specifies the color of an outline"), frameData.outlineColor);
				frameData.outlineThickness = EditorGUILayout.FloatField(new GUIContent("Outline Thickness", "Specifies the thickness of an outline. Causes visual errors if the value is too high"), frameData.outlineThickness);

				EditorGUILayout.Space();
			}
		}
		else
		{
			EditorGUILayout.HelpBox("Nothing will be highlighted for this frame", MessageType.Info);
		}

		GUI.enabled = true;
		EditorGUILayout.EndVertical();
	}

	public void DrawTextIconSettings()
	{
		//Text/Icon Settings-----------------------------------------------------------------------------------------------------------------
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Text and Icon Settings", boldText);

		//Icon settings----------------------------------------------------------------------------------------------------------------------
		if (tm.iconUI == null)
		{
			EditorGUILayout.HelpBox("Icon UI must be attached in order for this feature to work", MessageType.Warning);
			frameData.useIcon = false;
			GUI.enabled = false;
		}

		frameData.useIcon = EditorGUILayout.Toggle(new GUIContent("Use Icon", "Show an icon for this frame"), frameData.useIcon);

		if (frameData.useIcon)
		{
			frameData.iconSprite = (Sprite)EditorGUILayout.ObjectField("Icon Sprite", frameData.iconSprite, typeof(Sprite), true);

			EditorGUILayout.LabelField("Appearance Effects", boldText);

			EditorGUIUtility.labelWidth = 20;

			EditorGUILayout.BeginHorizontal();
			frameData.icon_flyIn = EditorGUILayout.ToggleLeft(new GUIContent("Fly In", "If set True, Icon UI will fly into the scene"), frameData.icon_flyIn);
			frameData.icon_fadeIn = EditorGUILayout.ToggleLeft(new GUIContent("Fade In", "If set True, Icon UI will fade in"), frameData.icon_fadeIn);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUIUtility.labelWidth = 150;

			if (!frameData.icon_fadeIn && !frameData.icon_flyIn)
			{
				EditorGUILayout.HelpBox("Icon will now instantly appear as soon as frame is played at runtime because no effect has been applied to it", MessageType.Info);
			}

			if (frameData.icon_flyIn)
			{
				EditorGUILayout.LabelField("Fly In", boldText);

				frameData.icon_fly_speed = EditorGUILayout.FloatField(new GUIContent("Fly Speed", "Determines the speed of the fly in speed of the Icon UI"), frameData.icon_fly_speed);
				frameData.icon_warpValue = EditorGUILayout.FloatField(new GUIContent("Icon Warp Value", "Specifies how far away is the target going to be from the final destination. The bigger the value, the more time it takes to travel"), frameData.icon_warpValue);
				frameData.icon_flyInDirection = (TutorialMasterScript.flyInDirection)EditorGUILayout.EnumPopup(new GUIContent("Fly In Direction", "Determines the direction Icon UI is going to fly from"), frameData.icon_flyInDirection);

				EditorGUILayout.Space();
			}

			if (frameData.icon_fadeIn)
			{
				EditorGUILayout.LabelField("Fade In", boldText);

				frameData.icon_fade_speed = EditorGUILayout.FloatField(new GUIContent("Fade In Speed", "Determines the speed of the fading of an Icon UI. Setting it 0 would render Icon UI invisible"), frameData.icon_fade_speed);

				EditorGUILayout.Space();
			}
			EditorGUILayout.Space();
		}

		GUI.enabled = true;

		//Text settings----------------------------------------------------------------------------------------------------------------------
		if (tm.textUI == null)
		{
			EditorGUILayout.HelpBox("Text UI must be attached in order for this feature to work", MessageType.Warning);
			frameData.useText = false;
			GUI.enabled = false;
		}

		frameData.useText = EditorGUILayout.Toggle(new GUIContent("Use Text", "Show text for this frame"), frameData.useText);

		if (frameData.useText)
		{
			//Configure the multiline text field

			EditorGUILayout.LabelField("Description Text");
			frameData.descriptionText = EditorGUILayout.TextArea(frameData.descriptionText, GUILayout.Height(50), GUILayout.Width(200), GUILayout.ExpandHeight(true));
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Appearance Effects", boldText);

			EditorGUIUtility.labelWidth = 20;

			EditorGUILayout.BeginHorizontal();
			//frameData.text_typing = EditorGUILayout.ToggleLeft("Typing", frameData.text_typing);
			frameData.text_flyIn = EditorGUILayout.ToggleLeft(new GUIContent("Fly In", "If set True, Text UI will fly into the scene"), frameData.text_flyIn);
			/*
			if (Screen.width < 457)
			{
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
			}
			*/
			frameData.text_fadeIn = EditorGUILayout.ToggleLeft(new GUIContent("Fade In", "If set True, Text UI would fade in"), frameData.text_fadeIn);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUIUtility.labelWidth = 150;
			/*
			if (!frameData.text_typing && !frameData.text_flyIn && !frameData.text_fadeIn)
			{
				EditorGUILayout.HelpBox("Text will now instantly appear as soon as frame is played at runtime because no effect has been applied to it", MessageType.Info);
			}
			*/
			if (frameData.text_flyIn)
			{
				EditorGUILayout.LabelField("Fly In", boldText);

				frameData.text_fly_speed = EditorGUILayout.FloatField(new GUIContent("Fly Speed", "Determines the speed of the fly in speed of the Text UI"), frameData.text_fly_speed);
				frameData.text_warpValue = EditorGUILayout.FloatField(new GUIContent("Text Warp Value", "Specifies how far away is the target going to be from the final destination. The bigger the value, the more time it takes to travel"), frameData.text_warpValue);
				frameData.text_flyInDirection = (TutorialMasterScript.flyInDirection)EditorGUILayout.EnumPopup(new GUIContent("Fly In Direction", "Determines the direction Text UI is going to fly from"), frameData.text_flyInDirection);

				EditorGUILayout.Space();
			}

			if (frameData.text_fadeIn)
			{
				EditorGUILayout.LabelField("Fade In", boldText);

				frameData.text_fade_speed = EditorGUILayout.FloatField(new GUIContent("Fade In Speed", "Determines the speed of the fading of an Icon UI. Setting it 0 would render Text UI invisible"), frameData.text_fade_speed);

				EditorGUILayout.Space();
			}

			//This code will never run-----------------
			if (frameData.text_typing)
			{
				EditorGUILayout.LabelField("Typing", boldText);

				frameData.text_typing_speed = EditorGUILayout.FloatField("Typing Speed", frameData.text_typing_speed);
				frameData.text_letter_amount = EditorGUILayout.IntField("Type Letter Amount", frameData.text_letter_amount);

				EditorGUILayout.Space();
			}
			//-----------------------------------------
		}

		if (!frameData.useText && !frameData.useIcon && tm.iconUI != null && tm.textUI != null)
		{
			EditorGUILayout.HelpBox("Neither icon nor text will be displayed for this frame", MessageType.Info);
		}

		GUI.enabled = true;
		EditorGUILayout.EndVertical();
	}

	public void DrawAudioSettings()
	{
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Audio Settings", boldText);

		if (tm.audioSource == null)
		{
			EditorGUILayout.HelpBox("Audio Source must be attached in order for this feature to work", MessageType.Warning);
			frameData.useAudio = false;
			GUI.enabled = false;
		}

		if (!frameData.useAudio && tm.audioSource != null)
		{
			EditorGUILayout.HelpBox("Audio will not be played for this frame", MessageType.Info);
		}

		EditorGUIUtility.labelWidth = 0;

		frameData.useAudio = EditorGUILayout.Toggle(new GUIContent("Play Audio", "If set True, an audio clip will be played in this frame"), frameData.useAudio);

		if (frameData.useAudio)
		{
			frameData.audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", frameData.audioClip, typeof(AudioClip), false);

			frameData.moreAudioSettings = EditorGUILayout.Toggle(new GUIContent("Expose Settings", "Few additional Audio Source settings are going to be available for editing if set true"), frameData.moreAudioSettings);

			if (frameData.moreAudioSettings)
			{
				frameData.audio_clip_output = (AudioMixerGroup)EditorGUILayout.ObjectField(new GUIContent("Mixer Group Output", "Is an audio source associated with any audio mixer group?"), frameData.audio_clip_output, typeof(AudioMixerGroup), false);
				frameData.audio_enable_loop = EditorGUILayout.Toggle("Loop", frameData.audio_enable_loop);
				frameData.audio_volume = EditorGUILayout.Slider("Volume", frameData.audio_volume, 0, 1.0f);
			}
		}

		GUI.enabled = true;
		EditorGUILayout.EndVertical();
	}

	public void DrawExceptionsList()
	{
		Event evt = Event.current;

		EditorGUILayout.LabelField("Exceptions List");

		if (frameData.exceptionsList.Count == 0)
		{
			EditorGUILayout.HelpBox("Looks like the Exceptions List is empty. Drag GameObjects into the box below to add them to Exceptions List", MessageType.Warning);
		}

		Rect exceptions_list_drop_area = EditorGUILayout.BeginVertical("box", GUILayout.Height(150));
		exceptionsListVector = EditorGUILayout.BeginScrollView(exceptionsListVector);
		for (int i = 0; i < frameData.exceptionsList.Count; i++)
		{
			GUILayout.BeginHorizontal("box", GUILayout.Height(18));
			GUILayout.Label(frameData.exceptionsList[i].gameObject.name);
			if (GUILayout.Button(new GUIContent("X"), GUILayout.Width(18), GUILayout.Height(18)))
			{
				frameData.exceptionsList.RemoveAt(i);
			}
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
		GUILayout.EndVertical();

		switch (evt.type)
		{
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if (!exceptions_list_drop_area.Contains(evt.mousePosition))
					return;

				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

				if (evt.type == EventType.DragPerform)
				{
					DragAndDrop.AcceptDrag();

					for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
					{
						if (DragAndDrop.objectReferences[i].GetType() == typeof(GameObject)) //check if it's GameObject type
						{
							if (frameData.exceptionsList.Find(x => (x.gameObject == (GameObject)DragAndDrop.objectReferences[i])) == null) //avoid duplicate objects
							{
								frameData.exceptionsList.Add(new TutorialMasterScript.exceptionObject
								{
									gameObject = (GameObject)DragAndDrop.objectReferences[i],
									default_index = 0
								});

								frameData.exceptionsList[frameData.exceptionsList.Count - 1].default_index = frameData.exceptionsList[frameData.exceptionsList.Count - 1].gameObject.transform.GetSiblingIndex();

								exceptionsListVector = new Vector2(0, Mathf.Infinity);
							}
						}
					}
				}
				break;
		}
	}
}

#endif