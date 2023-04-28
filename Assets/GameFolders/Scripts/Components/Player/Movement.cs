using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Managers;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("---- TakeOff Variables ----")]
    [SerializeField] private float takeOffYPosition;
    [SerializeField] private float degrees = 60;

    [Header("---- Speed Variables ----")] [SerializeField]
    private float forwardSpeed;
    [SerializeField]private float takeOffSpeed;
    [SerializeField] private float rotationSpeed;

    private PlaneEventData _planeEventData;

    private void Awake()
    {
        _planeEventData = Resources.Load("Plane/PlaneEventData") as PlaneEventData;
    }
    private void OnEnable()
    {
        _planeEventData.TakeOff += TakeOff;
    }
    private void OnDisable()
    {
        _planeEventData.TakeOff -= TakeOff;
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.Idle) return;
        transform.position += transform.forward * (forwardSpeed * Time.deltaTime);

        if (!GameManager.Instance.Playability()) return;
        
        if (Input.GetMouseButton(0))
        {
            float horizontalInput = UIController.Instance.GetHorizontal();
            float verticalInput = UIController.Instance.GetVertical();

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            movementDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void TakeOff()
    {
        float duration = takeOffYPosition / (( 90 / degrees) * forwardSpeed);
        transform.DOLocalRotate(Vector3.left * degrees, 0.5f).OnComplete(() =>
            transform.DOLocalRotate(Vector3.zero, 0.5f).SetDelay(duration)
                .OnComplete(() =>
                {
                    GameManager.Instance.GameState = GameState.Play;
                    forwardSpeed = takeOffSpeed;
                }));
    }
}