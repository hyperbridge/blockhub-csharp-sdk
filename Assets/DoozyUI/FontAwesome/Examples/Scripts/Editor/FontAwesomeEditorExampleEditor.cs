using UnityEngine;
using UnityEditor;
using DoozyUI.FontAwesome;

[CustomEditor(typeof(FontAwesomeEditorExample))]
public class FontAwesomeEditorExampleEditor : Editor
{
    float maxWidth = 420f;

    GUIStyle customTextStyle; //we create a style that uses Font Awesome as the text font and skin aware text colors
    GUIStyle customTextStylePurple; //we create a style that uses Font Awesome as the text font and a purple text color 
    GUIStyle customButtonStyle; //we create a button style that uses Font Awesome

    void CreateStyles()
    {
        if (customTextStyle == null)
        {
            customTextStyle = new GUIStyle
            {
                normal =
            {
                textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black
            },
                font = FA.Font,
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };
        }

        if (customTextStylePurple == null)
        {
            customTextStylePurple = new GUIStyle
            {
                normal =
            {
                textColor = FAColors.Purple.Color
            },
                font = FA.Font,
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };
        }

        if (customButtonStyle == null)
        {
            customButtonStyle = new GUIStyle(GUI.skin.button)
            {
                font = FA.Font,
                fontSize = 18,
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };
        }
    }


    public override void OnInspectorGUI()
    {
        CreateStyles(); //we create the styles

        GUILayout.Space(16);
        EditorGUILayout.LabelField("Usage example of Font Awesome inside a custom inspector.", GUILayout.Width(maxWidth));
        EditorGUILayout.LabelField("Below are some randomly selected icons.", GUILayout.Width(maxWidth));
        GUILayout.Space(12);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(maxWidth));
        {
            EditorGUILayout.LabelField(FA.address_book, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.barcode, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.bullhorn, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.chevron_circle_down, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.dashcube, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.envelope_square, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.flag, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.glide, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.heartbeat, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.html5, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.joomla, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.line_chart, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.magic, customTextStyle, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.music, customTextStyle, GUILayout.Width(20));
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(12);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(maxWidth));
        {
            EditorGUILayout.LabelField(FA.neuter, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.outdent, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.pause, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.paw, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.paypal, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.plug, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.podcast, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.qrcode, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.reddit, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.rss, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.sellsy, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.shield, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.sign_in, customTextStylePurple, GUILayout.Width(20));
            EditorGUILayout.LabelField(FA.sign_out, customTextStylePurple, GUILayout.Width(20));
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(12);
        GUIStyle evolvingStyle = new GUIStyle(customTextStyle);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(maxWidth));
        {
            evolvingStyle.fontSize = 8;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20));
            evolvingStyle.fontSize += 6;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 2));
            evolvingStyle.fontSize += 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 4));
            evolvingStyle.fontSize += 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 4));
            evolvingStyle.fontSize += 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 6));
            evolvingStyle.fontSize += 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 8));
            evolvingStyle.fontSize += 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 8));
            evolvingStyle.fontSize -= 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 6));
            evolvingStyle.fontSize -= 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 4));
            evolvingStyle.fontSize -= 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 4));
            evolvingStyle.fontSize -= 6;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20 + 2));
            evolvingStyle.fontSize -= 3;
            EditorGUILayout.LabelField(FA.phone, evolvingStyle, GUILayout.Width(20));
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(12);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(maxWidth));
        {
            if (GUILayout.Button(FA.gift, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.gift"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.github, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.github"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.glide, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.glide"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.google, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.google"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.hacker_news, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.hacker_news"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.headphones, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.headphones"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.houzz, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.houzz"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.instagram, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.instagram"); }
            GUILayout.Space(2);
            if (GUILayout.Button(FA.facebook, customButtonStyle, GUILayout.Width(32), GUILayout.Height(32))) { Debug.Log("Button name FA.facebook"); }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(16);
        EditorGUILayout.LabelField("Look inside FontAwesomeEditorExampleEditor to see the code.", GUILayout.Width(maxWidth));
        GUILayout.Space(4);
    }
}
