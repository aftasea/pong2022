using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    public float speed = 10f;
    public Bounds bounds;

    private const float HalfHeight = 0.5f;
    
    private float gameFieldTop;
    private float gameFieldBottom;

    private Vector3 initialPosition;

    // private PlayerPaddleController controller;

    private void Start() {
    }

    public void Init(float screenHeight/*, PlayerPaddleController playerPaddleController*/) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
        // controller = playerPaddleController;
        this.initialPosition = transform.position;
    }

    public void Reset() {
        transform.position = this.initialPosition;
    }

    public void UpdatePosition(float movementSpeed) {
        // float movementSpeed = controller.GetMovementSpeed(speed);
        if (!Mathf.Approximately(movementSpeed, 0f)) {
            Vector3 currentPos = transform.position;
            float nextPosY = currentPos.y + movementSpeed;
            currentPos.y = Mathf.Clamp(nextPosY, gameFieldBottom + HalfHeight, gameFieldTop - HalfHeight);
            transform.position = currentPos;
            this.bounds.center = currentPos;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
    }
}
