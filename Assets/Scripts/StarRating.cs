using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public int maxStars = 5; 
    public int currentStars = 0; 

    public Image starFillImage; 

    public void UpdateRating(int stars)
    {
        currentStars = Mathf.Clamp(stars, 0, maxStars);
        float fillAmount = (float)currentStars / maxStars;
        starFillImage.fillAmount = fillAmount;
    }
}
