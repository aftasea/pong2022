using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController
{
    public float GetMovementSpeed(float maxSpeed) {
        if (Input.GetKey(KeyCode.UpArrow)) {
            return maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            return -maxSpeed * Time.deltaTime;
        }

        return 0f;
    }
}
