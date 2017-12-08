using HardCodeLab.TutorialMaster;
using UnityEngine;

public class TMArrow
{
    public static Vector3 initPos;

    public void AnimateFloatingArrow(GameObject arrowUI, GameObject target, float floatingRange, float floatingSpeed, TutorialMasterScript.arrow_pointing_direction pointDirection)
    {
        float arrow_posDifference = ((Mathf.Sin(Time.time * floatingSpeed) * floatingRange - floatingRange) / 2);

        Vector3 finalPos = initPos;

        switch (pointDirection)
        {
            case TutorialMasterScript.arrow_pointing_direction.Top:
                finalPos = new Vector3(
                initPos.x,
                initPos.y - arrow_posDifference,
                initPos.z);

                break;

            case TutorialMasterScript.arrow_pointing_direction.Bottom:
                finalPos = new Vector3(
                    initPos.x,
                    initPos.y + arrow_posDifference,
                    initPos.z);
                break;

            case TutorialMasterScript.arrow_pointing_direction.Left:
                finalPos = new Vector3(
                    initPos.x + arrow_posDifference,
                    initPos.y,
                    initPos.z);

                break;

            case TutorialMasterScript.arrow_pointing_direction.Right:
                finalPos = new Vector3(
                    initPos.x - arrow_posDifference,
                    initPos.y,
                    initPos.z);
                break;

            case TutorialMasterScript.arrow_pointing_direction.TopLeft:
                finalPos = new Vector3(
                    initPos.x + arrow_posDifference,
                    initPos.y - arrow_posDifference,
                    initPos.z);

                break;

            case TutorialMasterScript.arrow_pointing_direction.TopRight:
                finalPos = new Vector3(
                    initPos.x - arrow_posDifference,
                    initPos.y - arrow_posDifference,
                    initPos.z);

                break;

            case TutorialMasterScript.arrow_pointing_direction.BottomLeft:
                finalPos = new Vector3(
                    initPos.x + arrow_posDifference,
                    initPos.y + arrow_posDifference,
                    initPos.z);

                break;

            case TutorialMasterScript.arrow_pointing_direction.BottomRight:
                finalPos = new Vector3(
                    initPos.x - arrow_posDifference,
                    initPos.y + arrow_posDifference,
                    initPos.z);
                break;
        }

        arrowUI.GetComponent<RectTransform>().localPosition = finalPos;
    }

    public void PointArrow(GameObject arrowUI, GameObject target, TutorialMasterScript.arrow_pointing_direction pointDirection, float offsetX, float offsetY)
    {
        if (tutorial.isPlaying)
        {
            arrowUI.SetActive(true);
            arrowUI.transform.SetParent(target.transform);

            Vector3 finalPos = new Vector3();
            Vector3 finalAngle = new Vector3();

            Vector2 targetSize = target.GetComponent<RectTransform>().sizeDelta;

            switch (pointDirection)
            {
                case TutorialMasterScript.arrow_pointing_direction.Top:
                    finalAngle = new Vector3(0, 0, -90);
                    finalPos = new Vector3(offsetX,
                    (targetSize.y / 2) + offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.Bottom:
                    finalAngle = new Vector3(0, 0, 90);
                    finalPos = new Vector3(offsetX,
                    -(targetSize.y / 2) + offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.Left:
                    finalAngle = new Vector3(0, 0, 0);
                    finalPos = new Vector3((-targetSize.x / 2) + offsetX,
                    offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.Right:
                    finalAngle = new Vector3(0, 0, 180);
                    finalPos = new Vector3((targetSize.x / 2) + offsetX,
                    offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.TopLeft:
                    finalAngle = new Vector3(0, 0, -45);
                    finalPos = new Vector3(-(targetSize.x / 2) + offsetX,
                    (targetSize.y / 2) + offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.TopRight:
                    finalAngle = new Vector3(0, 0, -135);
                    finalPos = new Vector3((targetSize.x / 2) + offsetX,
                    (targetSize.y / 2) + offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.BottomLeft:
                    finalAngle = new Vector3(0, 0, 45);
                    finalPos = new Vector3(-(targetSize.x / 2) + offsetX,
                    -(targetSize.y / 2) + offsetY,
                    0);
                    break;

                case TutorialMasterScript.arrow_pointing_direction.BottomRight:
                    finalAngle = new Vector3(0, 0, 135);
                    finalPos = new Vector3((targetSize.x / 2) + offsetX,
                    -(targetSize.y / 2) + offsetY,
                    0);
                    break;
            }

            arrowUI.GetComponent<RectTransform>().localEulerAngles = finalAngle;
            arrowUI.GetComponent<RectTransform>().localPosition = finalPos;

            initPos = finalPos;
        }
    }

    public static void DisableArrow(GameObject arrowUI)
    {
        if (arrowUI != null)
        {
            arrowUI.SetActive(false);
        }
    }
}