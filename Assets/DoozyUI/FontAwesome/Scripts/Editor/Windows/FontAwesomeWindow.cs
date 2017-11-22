// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using QuickEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI.FontAwesome
{
    public class FontAwesomeWindow : QWindow
    {
        public static FontAwesomeWindow Instance;

        public static bool Selected = false;
        private static bool needsRefresh = false;
        public static void NeedsRefresh(bool value = true) { needsRefresh = value; }

        private const float VERTICAL_SPACE_DIVIDER = 20f;
        private const float SIDE_BAR_WIDTH = 300f;
        private const float SIDE_BAR_SHADOW_WIDTH = 16f;
        private const float PAGE_WIDTH = SIDE_BAR_WIDTH * 2 - SIDE_BAR_SHADOW_WIDTH;
        private const float SIDE_BAR_LOGO_HEIGHT = 70f;
        private const float SIDE_BAR_HEADER_HEIGHT = 20f;
        private const float SIDE_BAR_MINI_BUTTON_HEIGHT = 24f;
        private const float SIDE_BAR_BUTTON_HEIGHT = 32f;

        public enum Section
        {
            None,
            AllIcons,
            WebApplicationIcons,
            AccesibilityIcons,
            HandIcons,
            TransportationIcons,
            GenderIcons,
            FileTypeIcons,
            SpinnerIcons,
            FormControlIcons,
            PaymentIcons,
            ChartIcons,
            CurrencyIcons,
            TextEditorIcons,
            DirectionalIcons,
            VideoPlayerIcons,
            BrandIcons,
            MedicalIcons
        }

        public static Section CurrentSection = Section.AllIcons;
        public static Section PreviousSection = Section.None;
        public static bool refreshData = true;

        private static Vector2 SectionScrollPosition = Vector2.zero;

        private static bool _utility = true;
        private static string _title = "DoozyUI - Font Awesome";
        private static bool _focus = true;

        private static float _minWidth = 900;
        private static float _minHeight = 600;

        private string SearchPattern = string.Empty;

        private static int charactersPerLine = 6;
        private static int buttonSpacing = 8;
        private static float zoom = 1f;
        private static float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                Instance.InitStyles();
            }
        }
        private static int buttonSize = 32;
        private static int fontSize = 20;
        private static int ZoomedButtonSize { get { return (int)(buttonSize * zoom); } }
        private static int ZoomedFontSize { get { return (int)(fontSize * zoom); } }
        private static GUIStyle characterButtonStyle;

        [MenuItem("Tools/DoozyUI/Font Awesome", false, 20)]
        static void Init()
        {
            Instance = GetWindow<FontAwesomeWindow>(_utility, _title, _focus);
            Instance.SetupWindow();
        }

        public static void Open(Section section)
        {
            Init();
            CurrentSection = section;
            refreshData = true;
        }

        private void OnEnable()
        {
            autoRepaintOnSceneChange = true;
            requiresContantRepaint = true;
        }

        private void SetupWindow()
        {
            titleContent = new GUIContent(_title);
            minSize = new Vector2(_minWidth, _minHeight);
            maxSize = minSize;
            CenterWindow();
        }

        private void InitStyles()
        {
            if (characterButtonStyle == null)
            {
                characterButtonStyle = new GUIStyle(FAStyles.GetStyle(FAStyles.ButtonStyle.Character))
                {
                    normal = { textColor = ColorExtensions.ColorFrom256(44, 23, 35) },
                    onNormal = { textColor = ColorExtensions.ColorFrom256(44, 23, 35) },
                    hover = { textColor = ColorExtensions.ColorFrom256(11, 42, 56) },
                    onHover = { textColor = ColorExtensions.ColorFrom256(11, 42, 56) },
                    active = { textColor = ColorExtensions.ColorFrom256(31, 51, 17) },
                    onActive = { textColor = ColorExtensions.ColorFrom256(31, 51, 17) },
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
                    padding = new RectOffset(2, 2, 1, 2),
                    border = new RectOffset(4, 4, 4, 4),
                    margin = new RectOffset(1, 1, 1, 1)
                };
            }
            characterButtonStyle.fixedWidth = ZoomedButtonSize;
            characterButtonStyle.fixedHeight = ZoomedButtonSize;
            characterButtonStyle.fontSize = ZoomedFontSize;
        }

        private void OnGUI()
        {
            InitStyles();
            DrawBackground();

            QUI.BeginHorizontal(position.width);
            {
                DrawSideBar();
                QUI.Space(16);
                DrawPages();
            }
            QUI.EndHorizontal();

            Repaint();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnFocus()
        {
            Selected = true;

            if (needsRefresh)
            {
                switch (CurrentSection)
                {
                    case Section.None: break;
                    case Section.AllIcons: break;
                    case Section.WebApplicationIcons: break;
                    case Section.AccesibilityIcons: break;
                    case Section.HandIcons: break;
                    case Section.TransportationIcons: break;
                    case Section.GenderIcons: break;
                    case Section.FileTypeIcons: break;
                    case Section.SpinnerIcons: break;
                    case Section.FormControlIcons: break;
                    case Section.PaymentIcons: break;
                    case Section.ChartIcons: break;
                    case Section.CurrencyIcons: break;
                    case Section.TextEditorIcons: break;
                    case Section.DirectionalIcons: break;
                    case Section.VideoPlayerIcons: break;
                    case Section.BrandIcons: break;
                    case Section.MedicalIcons: break;
                }
                needsRefresh = false;
            }
        }

        private void OnLostFocus()
        {
            Selected = false;
        }

        void DrawBackground()
        {
            QUI.BeginHorizontal();
            {
                QUI.DrawTexture(FAResources.backgroundGrey230Purple.texture, SIDE_BAR_WIDTH, position.height);
                QUI.Space(-SIDE_BAR_WIDTH);
                QUI.DrawTexture(FAResources.backgroundGrey242ShadowLeftPurple.texture, SIDE_BAR_SHADOW_WIDTH, position.height);
                QUI.Space(-SIDE_BAR_WIDTH * 2 + SIDE_BAR_SHADOW_WIDTH);
                QUI.DrawTexture(FAResources.backgroundGrey242Purple.texture, PAGE_WIDTH, position.height);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
        }

        void DrawSideBar()
        {
            QUI.BeginVertical(SIDE_BAR_WIDTH);
            {
                DrawSideBarLogo();
                QUI.Space(SPACE_8);
                DrawOptions(SIDE_BAR_WIDTH);
                QUI.Space(SPACE_8);
                DrawSideBarButtons();
                QUI.FlexibleSpace();
                DrawVersionInfo(SIDE_BAR_WIDTH);
                QUI.FlexibleSpace();
                DrawSideBarSocial();
            }
            QUI.EndVertical();
        }

        void DrawSideBarLogo()
        {
            QUI.DrawTexture(FAResources.sideLogoFontAwesome.texture, SIDE_BAR_WIDTH, SIDE_BAR_LOGO_HEIGHT);
        }
        void DrawSideBarButtons()
        {
            //AllIcons
            if (CurrentSection == Section.AllIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonAllIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.AllIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.AllIcons;
                    ResetPageView();
                }
            }

            //WebApplicationIcons
            if (CurrentSection == Section.WebApplicationIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonWebApplicationIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.WebApplicationIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.WebApplicationIcons;
                    ResetPageView();
                }
            }

            //AccesibilityIcons
            if (CurrentSection == Section.AccesibilityIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonAccesibilityIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.AccesibilityIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.AccesibilityIcons;
                    ResetPageView();
                }
            }

            //HandIcons
            if (CurrentSection == Section.HandIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonHandIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.HandIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.HandIcons;
                    ResetPageView();
                }
            }

            //TransportationIcons
            if (CurrentSection == Section.TransportationIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonTransportationIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.TransportationIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.TransportationIcons;
                    ResetPageView();
                }
            }

            //GenderIcons
            if (CurrentSection == Section.GenderIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonGenderIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.GenderIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.GenderIcons;
                    ResetPageView();
                }
            }

            //FileTypeIcons
            if (CurrentSection == Section.FileTypeIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonFileTypeIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.FileTypeIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.FileTypeIcons;
                    ResetPageView();
                }
            }

            //SpinnerIcons
            if (CurrentSection == Section.SpinnerIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonSpinnerIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.SpinnerIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.SpinnerIcons;
                    ResetPageView();
                }
            }

            //FormControlIcons
            if (CurrentSection == Section.FormControlIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonFormControlIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.FormControlIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.FormControlIcons;
                    ResetPageView();
                }
            }

            //PaymentIcons
            if (CurrentSection == Section.PaymentIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonPaymentIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.PaymentIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.PaymentIcons;
                    ResetPageView();
                }
            }

            //ChartIcons
            if (CurrentSection == Section.ChartIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonChartIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.ChartIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.ChartIcons;
                    ResetPageView();
                }
            }

            //CurrencyIcons
            if (CurrentSection == Section.CurrencyIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonCurrencyIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.CurrencyIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.CurrencyIcons;
                    ResetPageView();
                }
            }

            //TextEditorIcons
            if (CurrentSection == Section.TextEditorIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonTextEditorIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.TextEditorIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.TextEditorIcons;
                    ResetPageView();
                }
            }

            //DirectionalIcons
            if (CurrentSection == Section.DirectionalIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonDirectionalIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.DirectionalIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.DirectionalIcons;
                    ResetPageView();
                }
            }

            //VideoPlayerIcons
            if (CurrentSection == Section.VideoPlayerIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonVideoPlayerIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.VideoPlayerIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.VideoPlayerIcons;
                    ResetPageView();
                }
            }

            //BrandIcons
            if (CurrentSection == Section.BrandIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonBrandIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.BrandIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.BrandIcons;
                    ResetPageView();
                }
            }

            //MedicalIcons
            if (CurrentSection == Section.MedicalIcons)
            {
                QUI.DrawTexture(FAResources.sideButtonMedicalIconsSelected.texture, SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT);
            }
            else
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.MedicalIcons), SIDE_BAR_WIDTH, SIDE_BAR_MINI_BUTTON_HEIGHT))
                {
                    CurrentSection = Section.MedicalIcons;
                    ResetPageView();
                }
            }

            //QUI.Space(VERTICAL_SPACE_DIVIDER);
        }
        void DrawOptions(float width)
        {
            QUI.BeginHorizontal(width);
            {
                QUI.Space(SPACE_16);
                QUI.Label("Zoom", FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelNormal), 50);
                zoom = EditorGUILayout.Slider(zoom, 1f, 8f, GUILayout.Width(width - 50 - 40));
                QUI.Space(24);
            }
            QUI.EndHorizontal();

            zoom = (float)Math.Round(zoom, 2);
            if (zoom > 1 && zoom < 1.15) { zoom = 1; }
            else if (zoom > 1.35 && zoom < 1.5) { zoom = 1.5f; }
            else if (zoom > 1.5 && zoom < 1.65) { zoom = 1.5f; }
            else if (zoom > 1.85 && zoom < 2) { zoom = 2; }
            else if (zoom > 2 && zoom < 2.15) { zoom = 2; }
            else if (zoom > 2.35 && zoom < 2.5) { zoom = 2.5f; }
            else if (zoom > 2.5 && zoom < 2.65) { zoom = 2.5f; }
            else if (zoom > 2.85 && zoom < 3) { zoom = 3; }
            else if (zoom > 3 && zoom < 3.15) { zoom = 3; }
            else if (zoom > 3.35 && zoom < 3.5) { zoom = 3.5f; }
            else if (zoom > 3.5 && zoom < 3.65) { zoom = 3.5f; }
            else if (zoom > 3.85 && zoom < 4) { zoom = 4; }

            QUI.BeginHorizontal(width);
            {
                QUI.Space(SPACE_16);
                QUI.Label("Search", FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelNormal), 50);
                SearchPattern = EditorGUILayout.TextField(SearchPattern, GUILayout.Width(width - 50 - 40));
                QUI.Space(24);
            }
            QUI.EndHorizontal();
        }

        void DrawVersionInfo(float width)
        {
            QUI.BeginHorizontal(width);
            {
                QUI.FlexibleSpace();
                QUI.Label("Asset Version: " + FA.VERSION + "  |  Font Version: " + FA.FONT_VERSION, FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelSmallItalic), 180, 16);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            //QUI.Label(FA.COPYRIGHT, FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelSmallItalic), width, 16);

        }
        void DrawSideBarSocial()
        {
            QUI.BeginHorizontal(SIDE_BAR_WIDTH);
            {
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.ButtonTwitter), 100, 20))
                {
                    Application.OpenURL("https://twitter.com/doozyplay");
                }
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.ButtonFacebook), 100, 20))
                {
                    Application.OpenURL("https://www.facebook.com/doozyentertainment");
                }
                if (QUI.Button(FAStyles.GetStyle(FAStyles.ButtonStyle.ButtonYoutube), 100, 20))
                {
                    Application.OpenURL("http://www.youtube.com/c/DoozyEntertainment");
                }
            }
            QUI.EndHorizontal();
            QUI.Space(VERTICAL_SPACE_DIVIDER / 2);
        }

        void ResetPageView()
        {
            SectionScrollPosition = Vector2.zero; //reset scroll
            SearchPattern = ""; //reset search pattern
        }
        void DrawPages()
        {
            SectionScrollPosition = QUI.BeginScrollView(SectionScrollPosition);
            {
                switch (CurrentSection)
                {
                    case Section.AllIcons: DrawIcons(FAResources.headerAllIcons.texture, FA.AllIcons); break;
                    case Section.WebApplicationIcons: DrawIcons(FAResources.headerWebApplicationIcons.texture, FA.WebApplicationIcons); break;
                    case Section.AccesibilityIcons: DrawIcons(FAResources.headerAccessibilityIcons.texture, FA.AccessibilityIcons); break;
                    case Section.HandIcons: DrawIcons(FAResources.headerHandsIcons.texture, FA.HandIcons); break;
                    case Section.TransportationIcons: DrawIcons(FAResources.headerTransportationIcons.texture, FA.TransportationIcons); break;
                    case Section.GenderIcons: DrawIcons(FAResources.headerGenderIcons.texture, FA.GenderIcons); break;
                    case Section.FileTypeIcons: DrawIcons(FAResources.headerFileTypeIcons.texture, FA.FileTypeIcons); break;
                    case Section.SpinnerIcons: DrawIcons(FAResources.headerSpinnerIcons.texture, FA.SpinnerIcons); break;
                    case Section.FormControlIcons: DrawIcons(FAResources.headerFormControlIcons.texture, FA.FormControlIcons); break;
                    case Section.PaymentIcons: DrawIcons(FAResources.headerPaymentIcons.texture, FA.PaymentIcons); break;
                    case Section.ChartIcons: DrawIcons(FAResources.headerChartIcons.texture, FA.ChartIcons); break;
                    case Section.CurrencyIcons: DrawIcons(FAResources.headerCurrencyIcons.texture, FA.CurrencyIcons); break;
                    case Section.TextEditorIcons: DrawIcons(FAResources.headerTextEditorIcons.texture, FA.TextEditorIcons); break;
                    case Section.DirectionalIcons: DrawIcons(FAResources.headerDirectionalIcons.texture, FA.DirectionalIcons); break;
                    case Section.VideoPlayerIcons: DrawIcons(FAResources.headerVideoPlayerIcons.texture, FA.VideoPlayerIcons); break;
                    case Section.BrandIcons: DrawIcons(FAResources.headerBrandIcons.texture, FA.BrandIcons); break;
                    case Section.MedicalIcons: DrawIcons(FAResources.headerMedicalIcons.texture, FA.MedicalIcons); break;
                }
                QUI.Space(16);
            }
            QUI.EndScrollView();

            if (PreviousSection != CurrentSection || refreshData)
            {
                PreviousSection = CurrentSection;
                refreshData = false;
            }
        }
        void DrawIcons(Texture header, Dictionary<string, string> icons)
        {
            QUI.DrawTexture(header, 552, 64);
            float sectionWidth = PAGE_WIDTH - SIDE_BAR_SHADOW_WIDTH * 2;
            charactersPerLine = (int)((sectionWidth) / (ZoomedButtonSize + buttonSpacing));
            int index = 0;
            QUI.BeginHorizontal(sectionWidth);
            {
                QUI.FlexibleSpace();
                foreach (string codeName in icons.Keys)
                {
                    if (!SearchPattern.IsNullOrEmpty())//a search pattern has been entered in the search box
                    {
                        try
                        {
                            if (!Regex.IsMatch(codeName, SearchPattern, RegexOptions.IgnoreCase))
                            {
                                continue; //this does not match the search pattern --> we do not show this name it
                            }
                        }
                        catch (Exception) { }
                    }
                    if (index % charactersPerLine == 0)
                    {
                        QUI.FlexibleSpace();
                        QUI.EndHorizontal();
                        QUI.Space(buttonSpacing);
                        QUI.BeginHorizontal(sectionWidth);
                        QUI.FlexibleSpace();
                    }
                    QUI.BeginVertical(ZoomedButtonSize);
                    {
                        if (QUI.Button(icons[codeName], characterButtonStyle, ZoomedButtonSize, ZoomedButtonSize))
                        {
                            EditorGUIUtility.systemCopyBuffer = icons[codeName];
                            Debug.Log("[Font Awesome] The icon '" + codeName + "' has been copied to clipboard! Paste it in any text field that has its font set to 'Font Awesome'.");
                        }
                        if (zoom >= 2)
                        {
                            QUI.Label(codeName.Replace(" Alias", ""),
                                      (zoom >= 2 && zoom < 4) ? FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelSmallItalic) : FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelNormalItalic),
                                      ZoomedButtonSize,
                                      20);
                        }
                    }
                    QUI.EndVertical();
                    QUI.Space(buttonSpacing);
                    index++;
                }
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            if (!SearchPattern.IsNullOrEmpty() && index == 0)
            {
                QUI.Space(SPACE_16);
                QUI.BeginHorizontal(sectionWidth);
                {
                    QUI.FlexibleSpace();
                    QUI.Label("No icons were found!", FAStyles.GetStyle(FAStyles.BlackTextStyle.BlackLabelNormalItalic), 122);
                    QUI.FlexibleSpace();
                }
                QUI.EndHorizontal();
            }
        }
    }
}
