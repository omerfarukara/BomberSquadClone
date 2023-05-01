using System.Collections;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.FGEnum;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Fields And Properties

        [SerializeField] private int levelCount;
        [SerializeField] private int randomLevelLowerLimit;
        [SerializeField] private int startDelay;
        
        private EventData _eventData;

        public GameState GameState { get; set; } = GameState.Idle;

        private PlaneState _planeState = PlaneState.OnRunaway;

        public PlaneState PlaneState
        {
            get => _planeState;
            set
            {
                _planeState = value;
                _eventData.ChangePlaneState?.Invoke(value);
            }
        }

        private int Level
        {
            get => PlayerPrefs.GetInt("Level") > levelCount ? Random.Range(randomLevelLowerLimit, levelCount) : PlayerPrefs.GetInt("Level",1);
            set
            {
                PlayerPrefs.SetInt("RealLevel", value);
                PlayerPrefs.SetInt("Level", value);
            } 
        }
        public int RealLevel => PlayerPrefs.GetInt("RealLevel", 1);
        public int SceneLevel => SceneManager.GetActiveScene().buildIndex;
        
        public int Money
        {
            get => PlayerPrefs.GetInt("Money",1000000);
            set
            {
                PlayerPrefs.SetInt("Money", value);
                UIController.Instance.goldText.text = value.ToString();
            } 
        }

        #endregion
   
        #region MonoBehaviour Methods

        private void Awake()
        {
            Singleton(true);
            _eventData = Resources.Load("EventData") as EventData;
        }
    
        private void OnEnable()
        {
            _eventData.OnFinishLevel += OnFinish;
            _eventData.OnLoseLevel += OnLose;
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                StartCoroutine(LoadSceneAfterDelay(startDelay));
            }
        }

        #endregion

        #region Listening Methods

        private void OnFinish()
        {
            GameState = GameState.Idle;
            Level++;
        }

        private void OnLose()
        {
            GameState = GameState.Lose;
        }

        #endregion
    
        #region Unique Methods

        public bool Playability()
        {
            return GameState == GameState.Play;
        }
    
        public void NextLevel()
        {
            SceneManager.LoadScene(Level);
        }
        
        public void TryAgain()
        {
            SceneManager.LoadScene(Level);
        }
        
        public void PlaneStateStatu(bool value)
        {
            PlaneState = value ? PlaneState.OnFly : PlaneState.OnRunaway;
        }

        private IEnumerator LoadSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(startDelay);
            SceneManager.LoadScene(Level);
        }

        #endregion
    }
}
