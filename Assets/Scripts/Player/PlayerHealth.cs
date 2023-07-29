using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth; 
    public Slider healthSlider; 

    private void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; 

        if (currentHealth <= 0)
        {
            Die(); 
        }

        UpdateHealthBar(); 
    }

    private void Die()
    {       
        gameObject.SetActive(false); //deadge
    }

    private void UpdateHealthBar() 
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }
    }
}
