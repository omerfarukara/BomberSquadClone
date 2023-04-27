using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneModel : MonoBehaviour
{
    [SerializeField] private float maxRotatePos;
    [SerializeField] private bool isSingleEngine;
    
    [ShowIf("isSingleEngine")]
    [SerializeField] private GameObject engine;
    [ShowIf("isSingleEngine")]
    [SerializeField] private float engineRotateSpeed;
    
    void Update()
    {
        if (isSingleEngine)
        {
            engine.transform.Rotate(Vector3.forward * (engineRotateSpeed * Time.deltaTime));
        }
        
        if (!GameManager.Instance.Playability()) return;
        transform.localRotation =
            Quaternion.Euler(Vector3.back * (UIController.Instance.GetHorizontal() * maxRotatePos));

    }
}