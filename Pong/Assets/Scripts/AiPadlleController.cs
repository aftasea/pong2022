using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPadlleController : PaddleController {
    private Ball ball;
    
    public AiPadlleController(Paddle paddle, Ball ball) : base(paddle) {
        this.ball = ball;
        // this.paddle = paddle;
    }
    
    protected override float GetMovementSpeed() {
        float dirY = this.ball.transform.position.y - this.paddle.transform.position.y;
        float deltaSpeed = paddle.speed * Time.deltaTime;
        return Mathf.Clamp(dirY, -deltaSpeed, deltaSpeed);
    }
}
