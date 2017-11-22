// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;
using UnityEditor;
using QuickEditor;
using QuickEngine.Extensions;

namespace DoozyUI.FontAwesome
{
    public class FAResources
    {
        private static string _DOOZYUI_PATH = "";
        public static string DOOZYUI_PATH
        {
            get
            {
                if (_DOOZYUI_PATH.IsNullOrEmpty())
                {
                    _DOOZYUI_PATH = QuickEngine.IO.File.GetRelativeDirectoryPath("DoozyUI");
                }
                return _DOOZYUI_PATH;
            }
        }

        public static Texture GetTexture(string fileName)
        {
            return AssetDatabase.LoadAssetAtPath<Texture>(ImagesPath + fileName + ".png");
        }

        private static Font m_Sansation;
        public static Font Sansation { get { if (m_Sansation == null) { m_Sansation = AssetDatabase.LoadAssetAtPath<Font>(DOOZYUI_PATH + "/Fonts/" + "Sansation-Regular.ttf"); } return m_Sansation; } }

        private static string m_ImagesPath;
        public static string ImagesPath { get { if (string.IsNullOrEmpty(m_ImagesPath)) { m_ImagesPath = DOOZYUI_PATH + "/FontAwesome/Images/"; } return m_ImagesPath; } }

        public static QTexture backgroundGrey230Purple = new QTexture(ImagesPath, "backgroundGrey230Purple");
        public static QTexture backgroundGrey242Purple = new QTexture(ImagesPath, "backgroundGrey242Purple");
        public static QTexture backgroundGrey242ShadowLeftPurple = new QTexture(ImagesPath, "backgroundGrey242ShadowLeftPurple");

        public static QTexture characterButton = new QTexture(ImagesPath, "characterButton");

        public static QTexture sideLogoFontAwesome = new QTexture(ImagesPath, "sideLogoFontAwesome");

        public static QTexture sideButtonAllIcons = new QTexture(ImagesPath, "sideButtonAllIcons");
        public static QTexture sideButtonAllIconsSelected = new QTexture(ImagesPath, "sideButtonAllIconsSelected");
        public static QTexture sideButtonWebApplicationIcons = new QTexture(ImagesPath, "sideButtonWebApplicationIcons");
        public static QTexture sideButtonWebApplicationIconsSelected = new QTexture(ImagesPath, "sideButtonWebApplicationIconsSelected");
        public static QTexture sideButtonAccesibilityIcons = new QTexture(ImagesPath, "sideButtonAccesibilityIcons");
        public static QTexture sideButtonAccesibilityIconsSelected = new QTexture(ImagesPath, "sideButtonAccesibilityIconsSelected");
        public static QTexture sideButtonHandIcons = new QTexture(ImagesPath, "sideButtonHandIcons");
        public static QTexture sideButtonHandIconsSelected = new QTexture(ImagesPath, "sideButtonHandIconsSelected");
        public static QTexture sideButtonTransportationIcons = new QTexture(ImagesPath, "sideButtonTransportationIcons");
        public static QTexture sideButtonTransportationIconsSelected = new QTexture(ImagesPath, "sideButtonTransportationIconsSelected");
        public static QTexture sideButtonGenderIcons = new QTexture(ImagesPath, "sideButtonGenderIcons");
        public static QTexture sideButtonGenderIconsSelected = new QTexture(ImagesPath, "sideButtonGenderIconsSelected");
        public static QTexture sideButtonFileTypeIcons = new QTexture(ImagesPath, "sideButtonFileTypeIcons");
        public static QTexture sideButtonFileTypeIconsSelected = new QTexture(ImagesPath, "sideButtonFileTypeIconsSelected");
        public static QTexture sideButtonSpinnerIcons = new QTexture(ImagesPath, "sideButtonSpinnerIcons");
        public static QTexture sideButtonSpinnerIconsSelected = new QTexture(ImagesPath, "sideButtonSpinnerIconsSelected");
        public static QTexture sideButtonFormControlIcons = new QTexture(ImagesPath, "sideButtonFormControlIcons");
        public static QTexture sideButtonFormControlIconsSelected = new QTexture(ImagesPath, "sideButtonFormControlIconsSelected");
        public static QTexture sideButtonPaymentIcons = new QTexture(ImagesPath, "sideButtonPaymentIcons");
        public static QTexture sideButtonPaymentIconsSelected = new QTexture(ImagesPath, "sideButtonPaymentIconsSelected");
        public static QTexture sideButtonChartIcons = new QTexture(ImagesPath, "sideButtonChartIcons");
        public static QTexture sideButtonChartIconsSelected = new QTexture(ImagesPath, "sideButtonChartIconsSelected");
        public static QTexture sideButtonCurrencyIcons = new QTexture(ImagesPath, "sideButtonCurrencyIcons");
        public static QTexture sideButtonCurrencyIconsSelected = new QTexture(ImagesPath, "sideButtonCurrencyIconsSelected");
        public static QTexture sideButtonTextEditorIcons = new QTexture(ImagesPath, "sideButtonTextEditorIcons");
        public static QTexture sideButtonTextEditorIconsSelected = new QTexture(ImagesPath, "sideButtonTextEditorIconsSelected");
        public static QTexture sideButtonDirectionalIcons = new QTexture(ImagesPath, "sideButtonDirectionalIcons");
        public static QTexture sideButtonDirectionalIconsSelected = new QTexture(ImagesPath, "sideButtonDirectionalIconsSelected");
        public static QTexture sideButtonVideoPlayerIcons = new QTexture(ImagesPath, "sideButtonVideoPlayerIcons");
        public static QTexture sideButtonVideoPlayerIconsSelected = new QTexture(ImagesPath, "sideButtonVideoPlayerIconsSelected");
        public static QTexture sideButtonBrandIcons = new QTexture(ImagesPath, "sideButtonBrandIcons");
        public static QTexture sideButtonBrandIconsSelected = new QTexture(ImagesPath, "sideButtonBrandIconsSelected");
        public static QTexture sideButtonMedicalIcons = new QTexture(ImagesPath, "sideButtonMedicalIcons");
        public static QTexture sideButtonMedicalIconsSelected = new QTexture(ImagesPath, "sideButtonMedicalIconsSelected");

        public static QTexture buttonTwitter = new QTexture(ImagesPath, "buttonTwitter");
        public static QTexture buttonFacebook = new QTexture(ImagesPath, "buttonFacebook");
        public static QTexture buttonYoutube = new QTexture(ImagesPath, "buttonYoutube");

        public static QTexture headerAllIcons = new QTexture(ImagesPath, "headerAllIcons");
        public static QTexture headerWebApplicationIcons = new QTexture(ImagesPath, "headerWebApplicationIcons");
        public static QTexture headerAccessibilityIcons = new QTexture(ImagesPath, "headerAccessibilityIcons");
        public static QTexture headerHandsIcons = new QTexture(ImagesPath, "headerHandsIcons");
        public static QTexture headerTransportationIcons = new QTexture(ImagesPath, "headerTransportationIcons");
        public static QTexture headerGenderIcons = new QTexture(ImagesPath, "headerGenderIcons");
        public static QTexture headerFileTypeIcons = new QTexture(ImagesPath, "headerFileTypeIcons");
        public static QTexture headerSpinnerIcons = new QTexture(ImagesPath, "headerSpinnerIcons");
        public static QTexture headerFormControlIcons = new QTexture(ImagesPath, "headerFormControlIcons");
        public static QTexture headerPaymentIcons = new QTexture(ImagesPath, "headerPaymentIcons");
        public static QTexture headerChartIcons = new QTexture(ImagesPath, "headerChartIcons");
        public static QTexture headerCurrencyIcons = new QTexture(ImagesPath, "headerCurrencyIcons");
        public static QTexture headerTextEditorIcons = new QTexture(ImagesPath, "headerTextEditorIcons");
        public static QTexture headerDirectionalIcons = new QTexture(ImagesPath, "headerDirectionalIcons");
        public static QTexture headerVideoPlayerIcons = new QTexture(ImagesPath, "headerVideoPlayerIcons");
        public static QTexture headerBrandIcons = new QTexture(ImagesPath, "headerBrandIcons");
        public static QTexture headerMedicalIcons = new QTexture(ImagesPath, "headerMedicalIcons");
    }
}
