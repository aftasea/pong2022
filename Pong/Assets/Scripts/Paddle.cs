using UnityEngine;

public class Paddle : MonoBehaviour {
    public float maxSpeed = 10f;
    public Bounds bounds;
    
    private float gameFieldTop;
    private float gameFieldBottom;

    private Vector3 initialPosition;

    public void Init(float screenHeight) {
        gameFieldTop = screenHeight;
        gameFieldBottom = -screenHeight;
        initialPosition = transform.position;
    }

    public void Reset() {
        transform.position = initialPosition;
    }

    public void UpdatePosition(float movementSpeed) {
        if (!Mathf.Approximately(movementSpeed, 0f)) {
            Vector3 currentPos = transform.position;
            float nextPosY = currentPos.y + movementSpeed;
            float paddleHalfHeight = bounds.extents.y;
            currentPos.y = Mathf.Clamp(nextPosY, gameFieldBottom + paddleHalfHeight, gameFieldTop - paddleHalfHeight);
            transform.position = currentPos;
            bounds.center = currentPos;
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
