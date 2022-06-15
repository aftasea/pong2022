using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController {
	protected Paddle paddle;

	protected PaddleController(Paddle paddle) {
		this.paddle = paddle;
	}

	public void Reset() {
		this.paddle.Reset();
	}

	public void Update() {
		float speed = GetMovementSpeed();
		this.paddle.UpdatePosition(speed);
	}

	protected abstract float GetMovementSpeed();
}
