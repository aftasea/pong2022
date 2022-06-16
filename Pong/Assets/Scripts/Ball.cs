using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] public float initialSpeed = 5f;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float hitAcceleration = 0.1f;
    [SerializeField] private Vector3 direction = new Vector3(1f, 0f, 0f);
    [SerializeField] public Bounds bounds;
    
    private const float collisionCorrectionOffset = 0.01f;
    
    private float speed;
    private float gameFieldTop;
    private float gameFieldBottom;
    
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    private bool canMove;
    
    public void Init(float screenHeight, Paddle leftPaddle, Paddle rightPaddle) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
        Vector3.Normalize(direction);
        this.leftPaddle = leftPaddle;
        this.rightPaddle = rightPaddle;
        Reset();
    }

    public void Reset() {
        transform.position = Vector3.zero;
        speed = initialSpeed;
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

    public void UpdatePosition() {
        if (!canMove)
            return;
        
        float distance = speed * Time.deltaTime;
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + (direction * distance);

        CheckVerticalBordersCollision(ref targetPos);
        CheckPaddlesCollision(currentPos, ref targetPos, distance);

        transform.position = targetPos;
        bounds.center = targetPos;
    }

    private void CheckVerticalBordersCollision(ref Vector3 targetPos) {
        float ballHalfSize = this.bounds.extents.y;
        if (targetPos.y > 0f && targetPos.y + ballHalfSize > gameFieldTop) {
            targetPos.y = gameFieldTop - ballHalfSize;
            direction.y *= -1;
        }
        else if (targetPos.y < 0f && targetPos.y - ballHalfSize < gameFieldBottom) {
            targetPos.y = gameFieldBottom + ballHalfSize;
            direction.y *= -1;
        }
    }

    private void CheckPaddlesCollision(Vector3 currentPos, ref Vector3 targetPos, float distance) {
        if (Physics2D.BoxCast(currentPos, bounds.size, 0f, direction, distance)) {
            Paddle paddle = direction.x < 0f ? leftPaddle : rightPaddle;
            AdjustHorizontalPositionAfterHit(ref targetPos, paddle);
            BounceHorizontally();
            CalculateVerticalDirAfterPaddleHit(targetPos, paddle);
        }
    }

    private void AdjustHorizontalPositionAfterHit(ref Vector3 targetPos, Paddle paddle) {
        float minAllowedDistancePaddleBall = paddle.bounds.extents.x + this.bounds.extents.x + collisionCorrectionOffset;
        targetPos.x = paddle.transform.position.x + (-this.direction.x * minAllowedDistancePaddleBall);
    }

    private void BounceHorizontally() {
        direction.x *= -1;
        speed = Mathf.Min(speed + hitAcceleration, maxSpeed);
    }

    private void CalculateVerticalDirAfterPaddleHit(Vector3 targetPos, Paddle paddle) {
        float distanceFromCenter = targetPos.y - paddle.transform.position.y;
        float ratio = distanceFromCenter / paddle.bounds.size.y;
        direction.y = ratio;
        Vector3.Normalize(direction);
    }
    
    
    void OnDrawGizmos()  {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
