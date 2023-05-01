using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General.Data;
using UnityEngine;

public class ColliderOperations : MonoBehaviour
{
    private PlaneEventData _planeEventData;

    private void Awake()
    {
        _planeEventData = Resources.Load("Plane/PlaneEventData") as PlaneEventData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TakeOff"))
        {
            _planeEventData.TakeOff?.Invoke();
        }
    }
}