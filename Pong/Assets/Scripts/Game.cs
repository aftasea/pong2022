using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Game : MonoBehaviour {
    private Paddle playerPaddle;
    private Paddle aiPaddle;

    private void Awake() {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        Assert.IsTrue(paddles.Length == 2, "There are not two paddles in the scene");
        if (paddles[0].transform.position.x < 0f) {
            playerPaddle = paddles[0];
            aiPaddle = paddles[1];
        }
        else {
            playerPaddle = paddles[1];
            aiPaddle = paddles[0];
        }
        Camera cam = FindObjectOfType<Camera>();
        playerPaddle.Init(cam.orthographicSize);
    }
}
