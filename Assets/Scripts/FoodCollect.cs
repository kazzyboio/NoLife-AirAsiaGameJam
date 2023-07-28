using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollect : MonoBehaviour
{
    public FinishDeliver finishDeliver; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishDeliver.FoodCollected(); 
            Destroy(gameObject); 
        }
    }
}
