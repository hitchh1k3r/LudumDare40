﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{

  // Configuration:
  public float moveSpeed = 1.5f;
  public float spinSpeed = 640.0f;
  public string startingPlane = "PowerPlant";
  public FourDirectionWalk animator;

  // Cache:
  private Transform cam;

  // State:
  private NavigationPlane navPlane;
  private Quaternion targetRotation = Quaternion.identity;

  // Messages:

  void Start()
  {
    cam = Camera.main.transform;
    // cam.GetComponentInParent<CameraController>().trackingTarget = transform;
    // Interactable.SetPlayer(transform);
    navPlane = NavigationPlane.FindPlane(startingPlane);
  }

  void Update()
  {
    float speed = 0;
    if(!GameStateManager.IsMenu && GameStateManager.HasFocus)
    {
      Vector3 oldPos = transform.localPosition;
      Quaternion cameraForward = Quaternion.Euler(0, cam.eulerAngles.y, 0);
      Vector3 moveVector = cameraForward * new Vector3(Input.GetAxis("Horizontal"), 0,
            Input.GetAxis("Vertical"));
      if(moveVector.sqrMagnitude > 0.01f)
      {
        speed = moveVector.magnitude;
        if(speed > 1)
        {
          moveVector /= speed;
          speed = 1;
        }
        Vector3 newPos = transform.localPosition + Time.deltaTime * moveSpeed * moveVector;
        NavigationPlane.ValidMove(ref newPos, ref navPlane);
        transform.localPosition = newPos;
        targetRotation = Quaternion.LookRotation(moveVector);
      }
      transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation,
            spinSpeed * Time.deltaTime);
    }
    animator.speed = 10.0f * speed;
  }

  // Utilities:

  public void Teleport(string plane, Vector3 pos)
  {
    pos.y = 0;
    navPlane = NavigationPlane.FindPlane(plane);
    transform.position = pos;
  }

}
