using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFolders.Scripts.Components.Money
{
    public class MoneyCreator : MonoBehaviour
    {
        [SerializeField] private Vector2 minXRange,maxXRange;
        [SerializeField] private Vector2 minYRange,maxYRange;
    
        [Button("MoneyCreate")]
        public void MoneyCreate()
        {
            transform.parent = null;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                float x = Random.Range(Random.Range(minXRange.x,minXRange.y), Random.Range(maxXRange.x,maxXRange.y));
                float z = Random.Range(Random.Range(minYRange.x,minYRange.y), Random.Range(maxYRange.x,maxYRange.y));

                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).transform.DOLocalJump(new Vector3(x, 0, z), 1.5f, 1, 0.6f);
            }
        }

        [Button("ResetMoneyPositions")]
        public void ResetMoneyPositions()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).transform.localPosition = Vector3.zero;
            }
        }
    }
}