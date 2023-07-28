using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDeliver : MonoBehaviour
{
    public StarRating starRating; 
    private bool foodCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && foodCollected)
        {
            starRating.UpdateRating(starRating.currentStars + 1);

            Destroy(gameObject); 
        }
    }

    public void FoodCollected()
    {
        foodCollected = true;
    }
}
