using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    public float speed = 10f;

    private float gameFieldTop;
    private float gameFieldBottom;
    private float halfHeight = 0.5f;
    
    private void Start() {
    }

    public void Init(float screenHeight) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
    }

    void Update() {
        float movementSpeed = GetMovementSpeed();
        if (!Mathf.Approximately(movementSpeed, 0f)) {
            Vector3 currentPos = transform.position;
            float nextPosY = currentPos.y + movementSpeed;
            nextPosY = Mathf.Clamp(nextPosY, gameFieldBottom + halfHeight, gameFieldTop - halfHeight);
            currentPos.y = nextPosY;
            transform.position = currentPos;
        }
    }

    private float GetMovementSpeed() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            return speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
           return -speed * Time.deltaTime;
        }

        return 0f;
    }
}
