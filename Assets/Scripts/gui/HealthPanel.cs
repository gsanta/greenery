using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [SerializeField] private GameObject heartPrefab;

        [SerializeField] private Transform heartContainer;

        public void SetMaxHealth(float value)
        {
            //slider.maxValue = value;
            //slider.value = value;
        }
        
        public void SetHealth(float amount)
        {
            foreach (Transform child in heartContainer)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < amount; i++)
            {
                Instantiate(heartPrefab, heartContainer);
            }
            //slider.value = value;
        }
    }
}