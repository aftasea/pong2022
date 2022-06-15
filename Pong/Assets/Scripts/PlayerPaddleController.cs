using UnityEngine;

public class PlayerPaddleController : PaddleController
{
    public PlayerPaddleController(Paddle paddle) : base(paddle) {
    }

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
