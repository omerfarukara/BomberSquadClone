using GameFolders.Scripts.General;
using TMPro;
using UnityEngine;

namespace GameFolders.Scripts.Components.Money
{
    public class Money : MonoBehaviour
    {
        private PlaneController _plane;
        private EventData _eventData;

        private float distance;
        
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void Start()
        {
            _plane = PlaneController.Instance;
        }

        private void Update()
        {
            if (transform.childCount == 0) return;

            Vector3 currentTransform = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 planeTransform = new Vector3(_plane.transform.position.x, 0, _plane.transform.position.z);
            
            distance = Mathf.Abs(Vector3.Distance(currentTransform, planeTransform));

            if (distance < 20 && transform.GetChild(0).gameObject.activeInHierarchy)
            {
                for (int i = 0; i <transform.childCount; i++)
                {
                    _eventData.CollectMoney?.Invoke(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
