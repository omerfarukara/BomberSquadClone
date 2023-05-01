using System;
using System.Linq;
using DG.Tweening;
using GameFolders.Scripts.Controllers.Player;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Data;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Controllers
{
    public class UIController : MonoSingleton<UIController>
    {
        [SerializeField] private Joystick joystick;

        [SerializeField] internal TextMeshProUGUI goldText;
        //[Header("Panels")]
        //[SerializeField] private GameObject victoryPanel;
        //[SerializeField] private GameObject losePanel;

        //[Header("Buttons")]
        //[SerializeField] private Button nextLevelButton;
        //[SerializeField] private Button tryAgainButton;
        [Header("TapToStart-Menu")] [SerializeField]
        private Button tapToStartButton;

        [SerializeField] private GameObject tapToStartPanel;


        [SerializeField] private GameObject skillButtonGameObject;
        [SerializeField] private GameObject returnToBasePanel;

        [Header("Upgrade-Menu")] [SerializeField]
        private GameObject upgradeMenu;

        [SerializeField] private Button capacityUpgrade;
        [SerializeField] private TextMeshProUGUI capacityUpgradePrice;
        [SerializeField] private Button healthUpgrade;
        [SerializeField] private TextMeshProUGUI healthUpgradePrice;
        [SerializeField] private Button damageUpgrade;
        [SerializeField] private TextMeshProUGUI damageUpgradePrice;

        private EventData _eventData;
        private PlaneEventData _planeEventData;
        private PlaneData _planeData;
        private PlaneVariables planeVariables;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;
            _planeEventData = Resources.Load("Plane/PlaneEventData") as PlaneEventData;
            _planeData = Resources.Load("PlaneData") as PlaneData;
        }

        private void OnEnable()
        {
            //nextLevelButton.onClick.AddListener(OnNextLevel);
            //tryAgainButton.onClick.AddListener(OnTryAgain);

            capacityUpgrade.onClick.AddListener(CapacityUpgrade);
            healthUpgrade.onClick.AddListener(HealthUpgrade);
            damageUpgrade.onClick.AddListener(DamageUpgrade);


            tapToStartButton.onClick.AddListener(TapToStart);

            _eventData.ChangePlaneState += UpgradeMenuStatu;
            _planeEventData.ReturnToBase += ReturnToBase;
            //_eventData.OnFinishLevel += OnFinish;
            //_eventData.OnLoseLevel += OnLose;
        }

        private void Start()
        {
            Assignments();
        }

        private void DamageUpgrade()
        {
            if (!CheckPrice(planeVariables.damageUpgradeMoney)) return;
            AnimationButton(damageUpgrade);
            MoneyDeduction(planeVariables.damageUpgradeMoney);
            PlaneController.Instance.planeVariables.Damage +=
                PlaneController.Instance.planeVariables.damageUpgradeMoney;
        }

        private void HealthUpgrade()
        {
            if (!CheckPrice(planeVariables.healthUpgradeMoney)) return;
            AnimationButton(healthUpgrade);
            MoneyDeduction(planeVariables.healthUpgradeMoney);
            PlaneController.Instance.planeVariables.Health +=
                PlaneController.Instance.planeVariables.healthUpgradeMoney;
        }

        private void CapacityUpgrade()
        {
            if (!CheckPrice(planeVariables.capacityUpgradeMoney)) return;
            AnimationButton(capacityUpgrade);
            MoneyDeduction(planeVariables.capacityUpgradeMoney);
            PlaneController.Instance.planeVariables.Capacity +=
                PlaneController.Instance.planeVariables.capacityUpgradeMoney;
        }

        private void AnimationButton(Button btn)
        {
            btn.transform.parent.localScale = Vector3.zero;
            btn.transform.parent.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        }

        private bool CheckPrice(int upgradeMoney)
        {
            return GameManager.Instance.Money >= upgradeMoney;
        }

        private void MoneyDeduction(int deductionCount)
        {
            GameManager.Instance.Money -= deductionCount;
        }

        private void OnDisable()
        {
            _eventData.ChangePlaneState -= UpgradeMenuStatu;
            _planeEventData.ReturnToBase -= ReturnToBase;
            //_eventData.OnFinishLevel -= OnFinish;
            //_eventData.OnLoseLevel -= OnLose;
        }

        private void Assignments()
        {
            goldText.text = GameManager.Instance.Money.ToString();
            planeVariables = _planeData.planeVariablesList.FirstOrDefault(e =>
                e.PlaneDifficultyLevel == PlaneController.Instance.difficultyLevel);
            capacityUpgradePrice.text = $"{planeVariables.capacityUpgradeMoney.ToString()} $";
            damageUpgradePrice.text = $"{planeVariables.damageUpgradeMoney.ToString()} $";
            healthUpgradePrice.text = $"{planeVariables.healthUpgradeMoney.ToString()} $";
        }

        private void UpgradeMenuStatu(PlaneState state)
        {
            upgradeMenu.SetActive(state == PlaneState.OnRunaway);

            if (state == PlaneState.OnRunaway)
            {
                returnToBasePanel.SetActive(false);
                skillButtonGameObject.SetActive(false);
                tapToStartPanel.SetActive(true);
            }
            else
            {
                skillButtonGameObject.SetActive(true);
            }
        }

        private void ReturnToBase()
        {
            returnToBasePanel.SetActive(true);
        }

        // private void OnFinish()
        // {
        //     victoryPanel.SetActive(true);
        // }
        //
        // private void OnLose()
        // {
        //     losePanel.SetActive(true);
        // }

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