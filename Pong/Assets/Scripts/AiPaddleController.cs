using UnityEngine;

public class AiPaddleController : PaddleController {
    private Ball ball;
    
    public AiPaddleController(Paddle paddle, Ball ball) : base(paddle) {
        this.ball = ball;
    }
    
    protected override float GetMovementSpeed() {
        float dirY = ball.transform.position.y - paddle.transform.position.y;
        float deltaSpeed = paddle.speed * Time.deltaTime;
        return Mathf.Clamp(dirY, -deltaSpeed, deltaSpeed);
    }
}
