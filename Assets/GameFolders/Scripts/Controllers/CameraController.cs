using System;
using Cinemachine;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Data;
using GameFolders.Scripts.Managers;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera startCamera;
        [SerializeField] private CinemachineVirtualCamera gameCamera;

        private CinemachineBrain _cinemachineBrain;
        private PlaneEventData _planeEventData;
        private EventData _eventData;

        private void Awake()
        {
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            _planeEventData = Resources.Load("Plane/PlaneEventData") as PlaneEventData;
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.PlayCamera += PlayCamera;
            _planeEventData.Landing += Landing;
        }

        private void Landing()
        {
            startCamera.Priority = 2;
            gameCamera.Priority = 1;
        }

        private void OnDisable()
        {
            _eventData.PlayCamera -= PlayCamera;
            _planeEventData.Landing -= Landing;
        }


        private void PlayCamera()
        {
            startCamera.Priority = 1;
            gameCamera.Priority = 2;
        }
    }
}