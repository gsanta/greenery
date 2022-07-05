using UnityEngine;

namespace Items.EnterArea
{
    public class EnterAreaStore : MonoBehaviour
    {
        [SerializeField] private GameObject[] enterAreaList;

        public GameObject ChooseEnterArea()
        {
            return enterAreaList[0];
        }
    }
}