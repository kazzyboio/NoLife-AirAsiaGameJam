using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public static StarRating instance;

    public int maxStars = 5, currentStars = 5, deliverCombo = 0;
    public Image starFillImage;

    [SerializeField]
    private GameObject menu;
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
        UpdateRating(5);
    }

    private void Update()
    {
        fillAmount = ((float)currentStars) / maxStars;
        starFillImage.fillAmount = fillAmount;

        if (deliverCombo >= 5)
        {
            AudioManager.instance.Play("GainRep");

            deliverCombo = 0;
            UpdateRating(StarRating.instance.currentStars + 1);
        }
    }

    public void UpdateRating(int stars)
    {
        currentStars = Mathf.Clamp(stars, 0, maxStars);

        if (currentStars <= 0)
        {
            menu.GetComponent<MainMenu>().GameOver();
        }
    }
}
