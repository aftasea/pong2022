using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleController : PaddleController
{
    // private Paddle paddle;
    
    public PlayerPaddleController(Paddle paddle) : base(paddle) {
    }

    // public void Reset() {
    //     this.paddle.Reset();
    // }
    //
    // public void Update() {
    //     float speed = GetMovementSpeed(this.paddle.speed);
    //     this.paddle.UpdatePosition(speed);
    // }
    
    protected override float GetMovementSpeed() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            return paddle.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            return -paddle.speed * Time.deltaTime;
        }

        return 0f;
    }
}
