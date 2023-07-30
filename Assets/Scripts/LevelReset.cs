using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReset : MonoBehaviour
{
    private void Start()
    {
        StarRating.instance.currentStars = StarRating.instance.maxStars;
        StarRating.instance.deliverCombo = 0;

        FoodInventory.instance.foodCount = FoodInventory.instance.maxFoodCount;

        AudioManager.instance.Stop("BGM");
        AudioManager.instance.Stop("Ambience");
        AudioManager.instance.Stop("Moving");
        AudioManager.instance.Stop("MainMenu");

        AudioManager.instance.Play("BGM");
        AudioManager.instance.Play("Ambience");
    }
}
