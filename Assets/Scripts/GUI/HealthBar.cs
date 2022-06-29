using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetMaxHealth(float value)
        {
            slider.maxValue = value;
            slider.value = value;
        }
        
        public void SetHealth(float value)
        {
            slider.value = value;
        }
    }
}