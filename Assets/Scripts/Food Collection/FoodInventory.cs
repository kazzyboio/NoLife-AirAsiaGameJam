using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInventory : MonoBehaviour
{
    public static FoodInventory instance;

    [HideInInspector]
    public int foodCount = 0;
    public int maxFoodCount;

    [SerializeField]
    private Image foodFillImage;
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

    private void Update()
    {
        fillAmount = ((float)foodCount) / maxFoodCount;
        foodFillImage.fillAmount = fillAmount;
    }

    public void FoodCollected()
    {
        foodCount++;
    }

    public void FoodDelivered() 
    {
        foodCount--;
    }
}
