using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public Bounds bounds;
    
    private float gameFieldTop;
    private float gameFieldBottom;
    private const float HalfSize = 0.15f / 2f;

    [SerializeField]
    private Vector3 direction = new Vector3(1f, 0f, 0f);
    
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Init(float screenHeight, Paddle leftPaddle, Paddle rightPaddle) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
        this.direction = direction.normalized;
        this.leftPaddle = leftPaddle;
        this.rightPaddle = rightPaddle;
    }

    public void Reset() {
        transform.position = Vector3.zero;
    }

    public void UpdatePosition() {
        float distance = this.speed * Time.deltaTime;
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + (this.direction * distance);

        if (targetPos.y > 0f && targetPos.y + HalfSize > this.gameFieldTop) {
            targetPos.y = this.gameFieldTop - HalfSize;
            this.direction.y *= -1;
        }
        else if (targetPos.y < 0f && targetPos.y - HalfSize < this.gameFieldBottom) {
            targetPos.y = this.gameFieldBottom + HalfSize;
            this.direction.y *= -1;
        }
        
        var hit = Physics2D.Raycast(currentPos, this.direction, distance);
        if (hit) {
            Paddle paddle;
            if (this.direction.x > 0f) {
                paddle = this.rightPaddle;
                targetPos.x = paddle.transform.position.x - paddle.bounds.extents.x - this.bounds.extents.x;
            }
            else {
                paddle = this.leftPaddle;
                targetPos.x = paddle.transform.position.x + paddle.bounds.extents.x + this.bounds.extents.x;
            }
            this.BounceX();

            float distanceFromCenter = targetPos.y - paddle.transform.position.y;
            float ratio = distanceFromCenter / paddle.bounds.size.y;
            this.direction.y = ratio;
            this.direction = this.direction.normalized;
        }
        
        transform.position = targetPos;
        this.bounds.center = targetPos;
    }

    private void BounceX() {
        this.direction.x *= -1;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(/*transform.position + */bounds.center, bounds.size);
    }
}
