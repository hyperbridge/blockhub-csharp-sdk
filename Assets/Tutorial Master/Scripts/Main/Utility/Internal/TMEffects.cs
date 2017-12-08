using HardCodeLab.TutorialMaster;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
    This script contains effects used such as fly in and fade in
*/

public class TMEffects
{
    public IEnumerator effect_fly_in(GameObject target, TutorialMasterScript.flyInDirection flyDirection, Vector3 targetPosition, float flySpeed, float warpValue, bool skipEffects)
    {
        int my_id = frame.id;

        //Teleport Target object to the specified location based on settings
        switch (flyDirection)
        {
            case TutorialMasterScript.flyInDirection.Top:
                target.transform.position = new Vector3(
                    targetPosition.x,
                    targetPosition.y * warpValue,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.Bottom:

                target.transform.position = new Vector3(
                    targetPosition.x,
                    targetPosition.y * -warpValue,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.BottomLeft:

                target.transform.position = new Vector3(
                    targetPosition.x * -warpValue,
                    targetPosition.y * -warpValue,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.BottomRight:

                target.transform.position = new Vector3(
                    targetPosition.x * warpValue,
                    targetPosition.y * -warpValue,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.Left:

                target.transform.position = new Vector3(
                    targetPosition.x * -warpValue,
                    targetPosition.y,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.Right:

                target.transform.position = new Vector3(
                    targetPosition.x * warpValue,
                    targetPosition.y,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.TopLeft:

                target.transform.position = new Vector3(
                    targetPosition.x * -warpValue,
                    targetPosition.y * warpValue,
                    targetPosition.z);
                break;

            case TutorialMasterScript.flyInDirection.TopRight:

                target.transform.position = new Vector3(
                    targetPosition.x * warpValue,
                    targetPosition.y * warpValue,
                    targetPosition.z);
                break;
        }

        //Initiate flying effect of the target
        while (target.transform.position != targetPosition && my_id == frame.id && !skipEffects)
        {
            target.transform.position = Vector3.LerpUnclamped(
                target.transform.position,
                targetPosition,
                flySpeed * Time.deltaTime);

            yield return true;
        }

        if (my_id != frame.id || skipEffects)
        {
            target.transform.position = targetPosition;
        }
    }

    public IEnumerator effect_fade_in(GameObject target, float fadeSpeed, bool isText, bool skipEffects)
    {
        int my_id = frame.id;
        if (!isText)
        {
            Color defaultColor = target.GetComponent<Image>().color;
            target.GetComponent<Image>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);

            while (target.GetComponent<Image>().color != defaultColor && my_id == frame.id && !skipEffects)
            {
                target.GetComponent<Image>().color = Color.LerpUnclamped(
                    target.GetComponent<Image>().color,
                    defaultColor,
                    fadeSpeed * Time.deltaTime);

                yield return true;
            }

            if (my_id != frame.id || skipEffects)
            {
                target.GetComponent<Image>().color = defaultColor;
            }
        }
        else
        {
            Color defaultColor = target.GetComponent<Text>().color;
            target.GetComponent<Text>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);

            while (target.GetComponent<Text>().color != defaultColor && my_id == frame.id && !skipEffects)
            {
                target.GetComponent<Text>().color = Color.LerpUnclamped(
                    target.GetComponent<Text>().color,
                    defaultColor,
                    fadeSpeed * Time.deltaTime);

                yield return true;
            }

            if (my_id != frame.id || skipEffects)
            {
                target.GetComponent<Text>().color = defaultColor;
            }
        }
    }

    public static IEnumerator effect_typing_in()
    {
        //start a typing effect
        yield return true;
    }
}