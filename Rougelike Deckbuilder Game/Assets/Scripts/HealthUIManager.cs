using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healthText;
    public Image blockIcon;
    public TextMeshProUGUI blockText;
    public Slider poisonSlider;
    
    // Start is called before the first frame update
    public void Start()
    {
        healthText = healthBarSlider.GetComponentInChildren<TextMeshProUGUI>();
        blockText = blockIcon.GetComponentInChildren<TextMeshProUGUI>();
        blockIcon.enabled = false;
        blockText.enabled = false;
    }


    public void UpdateHealth(float value, float currentHealth, float maxHealth)
    {
        healthBarSlider.value = value;
        healthText.text = currentHealth + "/" + maxHealth;
        if (currentHealth < 0)
        {
            healthText.text = 0 + "/" + maxHealth;
        }
    }
    public void UpdatePoison(float value)
    {
        poisonSlider.value = value;
    }

    public void UpdateBlock(float value)
    {
        if (value > 0)
        {
            blockIcon.enabled = true;
            blockText.enabled = true;
            blockText.text = value.ToString();
        }
        else
        {
            blockIcon.enabled = false;
            blockText.enabled= false;
            blockText.text = "0";
        }
    }
}
