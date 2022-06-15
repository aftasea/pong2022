using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float maxSpeed = 10f;
    public float speed;
    public float hitAcceleration = 0.1f;
    public Bounds bounds;
    
    private float gameFieldTop;
    private float gameFieldBottom;
    private const float HalfSize = 0.15f / 2f;

    [SerializeField]
    private Vector3 direction = new Vector3(1f, 0f, 0f);
    
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    private bool canMove;
    
    public void Init(float screenHeight, Paddle leftPaddle, Paddle rightPaddle) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
        direction = direction.normalized;
        this.leftPaddle = leftPaddle;
        this.rightPaddle = rightPaddle;
        Reset();
    }

    public void Reset() {
        transform.position = Vector3.zero;
        speed = initialSpeed;
    }

    public void UpdatePosition() {
        if (!canMove)
            return;
        
        float distance = speed * Time.deltaTime;
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + (direction * distance);

        if (targetPos.y > 0f && targetPos.y + HalfSize > gameFieldTop) {
            targetPos.y = gameFieldTop - HalfSize;
            direction.y *= -1;
        }
        else if (targetPos.y < 0f && targetPos.y - HalfSize < gameFieldBottom) {
            targetPos.y = gameFieldBottom + HalfSize;
            direction.y *= -1;
        }
        
        var hit = Physics2D.Raycast(currentPos, direction, distance);
        if (hit) {
            Paddle paddle;
            if (direction.x > 0f) {
                paddle = rightPaddle;
                targetPos.x = paddle.transform.position.x - paddle.bounds.extents.x - bounds.extents.x;
            }
            else {
                paddle = this.leftPaddle;
                targetPos.x = paddle.transform.position.x + paddle.bounds.extents.x + bounds.extents.x;
            }
            this.BounceX();

            float distanceFromCenter = targetPos.y - paddle.transform.position.y;
            float ratio = distanceFromCenter / paddle.bounds.size.y;
            direction.y = ratio;
            direction = direction.normalized;
        }
        
        transform.position = targetPos;
        bounds.center = targetPos;
    }

    private void BounceX() {
        direction.x *= -1;
        speed = Mathf.Min(speed + hitAcceleration, maxSpeed);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    public void Serve() {
        StartCoroutine(ServeCo());
    }

    private IEnumerator ServeCo() {
        canMove = false;
        Reset();
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
}
