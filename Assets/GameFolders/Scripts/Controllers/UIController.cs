using System;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Controllers
{
    public class UIController : MonoSingleton<UIController>
    {
        private EventData _eventData;
        [SerializeField] private Joystick joystick;

        //[Header("Panels")]
        //[SerializeField] private GameObject victoryPanel;
        //[SerializeField] private GameObject losePanel;

        //[Header("Buttons")]
        //[SerializeField] private Button nextLevelButton;
        //[SerializeField] private Button tryAgainButton;
        [SerializeField] private Button tapToStartButton;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            //nextLevelButton.onClick.AddListener(OnNextLevel);
            //tryAgainButton.onClick.AddListener(OnTryAgain);
            tapToStartButton.onClick.AddListener(TapToStart);
            //_eventData.OnFinishLevel += OnFinish;
            //_eventData.OnLoseLevel += OnLose;
        }

/*
        private void OnDisable()
        {
            _eventData.OnFinishLevel -= OnFinish;
            _eventData.OnLoseLevel -= OnLose;
        }

        private void OnFinish()
        {
            victoryPanel.SetActive(true);
        }

        private void OnLose()
        {
            losePanel.SetActive(true);
        }
*/
        private void TapToStart()
        {
            _eventData.PlayCamera?.Invoke();
            GameManager.Instance.GameState = GameState.TakeOff;
        }

        private void OnNextLevel()
        {
            GameManager.Instance.NextLevel();
        }

        private void OnTryAgain()
        {
            GameManager.Instance.TryAgain();
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }
        
        public float GetVertical()
        {
            return joystick.Vertical;
        }
    }
}