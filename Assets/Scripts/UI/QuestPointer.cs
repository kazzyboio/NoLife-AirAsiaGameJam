using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPointer : MonoBehaviour
{
    public Transform target;
    public Image image;

    [Header("Image Scaling")]
    public float minDistance = 2f;
    public float maxDistance = 5f;
    public float minScale = 0.5f;
    public float maxScale = 1f;

    private void Update()
    {
        if (target == null)
        {
            image.enabled = false;
            return;
        }

        image.enabled = true;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(target.position);

        float minX = image.GetPixelAdjustedRect().width / 4;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height / 4;
        float maxY = Screen.height - minY;

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (screenPos.x < Screen.width / 2)
            {
                screenPos.x = maxX;
            }
            else
            {
                screenPos.x = minX;
            }
        }

        screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
        screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);

        image.transform.position = screenPos;

        Vector3 directionToTarget = target.position - Camera.main.transform.position;
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        image.transform.rotation = Quaternion.Euler(0f, 0f, angleToTarget - 180f);

        float distanceToTarget = Vector3.Distance(target.position, Camera.main.transform.position);
        float normalizedDistance = Mathf.Clamp01((distanceToTarget - minDistance) / (maxDistance - minDistance));
        float scale = Mathf.Lerp(maxScale, minScale, normalizedDistance); 
        image.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
