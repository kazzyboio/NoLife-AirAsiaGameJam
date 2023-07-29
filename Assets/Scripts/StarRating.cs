using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public static StarRating instance;

    public int maxStars = 5; 
    public int currentStars = 0; 

    public Image starFillImage;

    private float fillAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UpdateRating(3);
    }

    private void Update()
    {
        fillAmount = ((float)currentStars) / maxStars;
        starFillImage.fillAmount = fillAmount;
    }

    public void UpdateRating(int stars)
    {
        currentStars = Mathf.Clamp(stars, 0, maxStars);
    }
}
