// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEngine;

namespace DoozyUI.FontAwesome
{
    public class FAStyles
    {
        public enum TextStyle
        {
            LabelSmall,
            LabelSmallItalic,
            LabelNormal,
            LabelNormalItalic,
            LabelLarge
        }

        public enum BlackTextStyle
        {
            BlackLabelSmall,
            BlackLabelSmallItalic,
            BlackLabelNormal,
            BlackLabelNormalItalic,
            BlackLabelLarge
        }

        public enum ButtonStyle
        {
            Character,

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
            MedicalIcons,

            ButtonTwitter,
            ButtonFacebook,
            ButtonYoutube
        }

        private static GUISkin skin;
        public static GUISkin Skin { get { if (skin == null) { skin = GetSkin(); } return skin; } }

        public static GUIStyle GetStyle(TextStyle styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(BlackTextStyle styleName) { return Skin.GetStyle(styleName.ToString()); }
        public static GUIStyle GetStyle(ButtonStyle styleName) { return Skin.GetStyle(styleName.ToString()); }

        private static GUISkin GetSkin()
        {
            GUISkin skin = ScriptableObject.CreateInstance<GUISkin>();
            List<GUIStyle> styles = new List<GUIStyle>();
            styles.AddRange(TextStyles());
            styles.AddRange(BlackTextStyles());
            styles.AddRange(ButtonStyles());
            skin.customStyles = styles.ToArray();
            return skin;
        }

        private static void UpdateSkin()
        {
            skin = null;
            skin = GetSkin();
        }

        public static void AddStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (customStyles.Contains(style)) { return; }
            customStyles.Add(style);
            Skin.customStyles = customStyles.ToArray();
        }

        public static void RemoveStyle(GUIStyle style)
        {
            if (style == null) { return; }
            List<GUIStyle> customStyles = new List<GUIStyle>();
            customStyles.AddRange(Skin.customStyles);
            if (!customStyles.Contains(style)) { return; }
            customStyles.Remove(style);
            Skin.customStyles = customStyles.ToArray();
        }

        private static List<GUIStyle> TextStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetTextStyle(TextStyle.LabelSmall, TextAnchor.MiddleLeft, FontStyle.Normal, 10, FAResources.Sansation),
                GetTextStyle(TextStyle.LabelSmallItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 10, FAResources.Sansation),
                GetTextStyle(TextStyle.LabelNormal, TextAnchor.MiddleLeft, FontStyle.Normal, 12, FAResources.Sansation),
                GetTextStyle(TextStyle.LabelNormalItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 12, FAResources.Sansation),
                GetTextStyle(TextStyle.LabelLarge, TextAnchor.MiddleLeft, FontStyle.Normal, 14, FAResources.Sansation)
            };
            return styles;
        }
        private static GUIStyle GetTextStyle(TextStyle styleName, TextAnchor alignment, FontStyle fontStyle, int fontSize, Font font = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                name = styleName.ToString(),
                alignment = alignment,
                fontStyle = fontStyle,
                fontSize = fontSize,
                font = font
            };
            return style;
        }

        private static List<GUIStyle> BlackTextStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetBlackTextStyle(BlackTextStyle.BlackLabelSmall, TextAnchor.MiddleLeft, FontStyle.Normal, 10, FAResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelSmallItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 10, FAResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelNormal, TextAnchor.MiddleLeft, FontStyle.Normal, 12, FAResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelNormalItalic, TextAnchor.MiddleLeft, FontStyle.Italic, 12, FAResources.Sansation),
                GetBlackTextStyle(BlackTextStyle.BlackLabelLarge, TextAnchor.MiddleLeft, FontStyle.Normal, 14, FAResources.Sansation)
            };
            return styles;
        }
        private static GUIStyle GetBlackTextStyle(BlackTextStyle styleName, TextAnchor alignment, FontStyle fontStyle, int fontSize, Font font = null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                name = styleName.ToString(),
                normal = { textColor = Color.black },
                alignment = alignment,
                fontStyle = fontStyle,
                fontSize = fontSize,
                font = font,
            };
            return style;
        }

        private static List<GUIStyle> ButtonStyles()
        {
            List<GUIStyle> styles = new List<GUIStyle>
            {
                GetButtonStyle(ButtonStyle.Character, FAResources.characterButton, FA.Font),

                GetButtonStyle(ButtonStyle.AllIcons, FAResources.sideButtonAllIcons),
                GetButtonStyle(ButtonStyle.WebApplicationIcons, FAResources.sideButtonWebApplicationIcons),
                GetButtonStyle(ButtonStyle.AccesibilityIcons, FAResources.sideButtonAccesibilityIcons),
                GetButtonStyle(ButtonStyle.HandIcons, FAResources.sideButtonHandIcons),
                GetButtonStyle(ButtonStyle.TransportationIcons, FAResources.sideButtonTransportationIcons),
                GetButtonStyle(ButtonStyle.GenderIcons, FAResources.sideButtonGenderIcons),
                GetButtonStyle(ButtonStyle.FileTypeIcons, FAResources.sideButtonFileTypeIcons),
                GetButtonStyle(ButtonStyle.SpinnerIcons, FAResources.sideButtonSpinnerIcons),
                GetButtonStyle(ButtonStyle.FormControlIcons, FAResources.sideButtonFormControlIcons),
                GetButtonStyle(ButtonStyle.PaymentIcons, FAResources.sideButtonPaymentIcons),
                GetButtonStyle(ButtonStyle.ChartIcons, FAResources.sideButtonChartIcons),
                GetButtonStyle(ButtonStyle.CurrencyIcons, FAResources.sideButtonCurrencyIcons),
                GetButtonStyle(ButtonStyle.TextEditorIcons, FAResources.sideButtonTextEditorIcons),
                GetButtonStyle(ButtonStyle.DirectionalIcons, FAResources.sideButtonDirectionalIcons),
                GetButtonStyle(ButtonStyle.VideoPlayerIcons, FAResources.sideButtonVideoPlayerIcons),
                GetButtonStyle(ButtonStyle.BrandIcons, FAResources.sideButtonBrandIcons),
                GetButtonStyle(ButtonStyle.MedicalIcons, FAResources.sideButtonMedicalIcons),

                GetButtonStyle(ButtonStyle.ButtonTwitter,   FAResources.buttonTwitter),
                GetButtonStyle(ButtonStyle.ButtonFacebook, FAResources.buttonFacebook),
                GetButtonStyle(ButtonStyle.ButtonYoutube, FAResources.buttonYoutube)
            };
            return styles;
        }
        private static GUIStyle GetButtonStyle(ButtonStyle styleName, QTexture qTexture, Font font = null)
        {
            GUIStyle style = new GUIStyle
            {
                name = styleName.ToString(),
                normal = { background = qTexture.normal2D, textColor = Color.black },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D }
            };
            if (font != null) { style.font = font; }
            return style;
        }
    }
}
