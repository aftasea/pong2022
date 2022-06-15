public abstract class PaddleController {
	protected Paddle paddle;

	protected PaddleController(Paddle paddle) {
		this.paddle = paddle;
	}

	public void Reset() {
		paddle.Reset();
	}

	public void Update() {
		float speed = GetMovementSpeed();
		paddle.UpdatePosition(speed);
	}

	protected abstract float GetMovementSpeed();
}
