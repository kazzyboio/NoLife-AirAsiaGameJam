using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPointer : MonoBehaviour
{
    public Transform target;
    public Image image;

    private float offsetAmount = Screen.height / 10;

    [Header("Image Scaling")]
    public float minDistance = 2f;
    public float maxDistance = 5f;
    public float minScale = 0.5f;
    public float maxScale = 1f;

    private Vector2 initialOffset;

    // custom constructor to set the initial offset
    public void Initialize(Vector2 offset)
    {
        initialOffset = offset;
    }

    // method to set the offset dynamically during runtime
    public void SetOffset(Vector2 newOffset)
    {
        initialOffset = newOffset;
    }

    private void Update()
    {
        if (target == null)
        {
            image.enabled = false;
            return;
        }

        image.enabled = true;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(target.position);

        //float minX = image.GetPixelAdjustedRect().width;
        //float maxX = Screen.width - minX + 100f;

        //float minY = image.GetPixelAdjustedRect().height / 3;
        //float maxY = Screen.height - minY - 300f;

        float minX = Screen.width / 4;
        float maxX = Screen.width * 3 / 4;

        float minY = Screen.height / 8;
        float maxY = Screen.height * 3 / 4;

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

        // Apply the initial offset for this specific object
        screenPos += initialOffset;
        transform.position = screenPos;

        Vector3 directionToTarget = target.position - Camera.main.transform.position;
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        image.transform.rotation = Quaternion.Euler(0f, 0f, angleToTarget);

        float distanceToTarget = Vector3.Distance(target.position, Camera.main.transform.position);

        //float normalizedDistance = Mathf.Clamp01((distanceToTarget - minDistance) / (maxDistance - minDistance));
        //float scale = Mathf.Lerp(maxScale, minScale, normalizedDistance);
        //transform.localScale = new Vector3(scale, scale, 1f);

        Vector2 newOffset = new Vector2(0f, offsetAmount); // quest pointer image offset
        SetOffset(newOffset);
    }
}
