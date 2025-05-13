using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.wholeNumbers = false;
    }

    public void SetMaxHealth(float health){ //method in case we need to set max
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
    
}    
