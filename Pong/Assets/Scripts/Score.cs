public class Score : IScoreProvider
{
    private Ball ball;
    private float fieldWidthHalf;
    
    private short leftScore;
    private short rightScore;

    public short GetLeftScore() => leftScore;

    public short GetRightScore() => rightScore;

    public bool NewPointScoredInThisFrame {
        get;
        private set;
    }

    public Score(Ball ball, float fieldWidthHalf) {
        this.ball = ball;
        this.fieldWidthHalf = fieldWidthHalf;
    }

    public void Reset() {
        leftScore = 0;
        rightScore = 0;
    }
    
    public void Update() {
        NewPointScoredInThisFrame = false;
        if (ball.transform.position.x > fieldWidthHalf) {
            leftScore++;
            NewPointScoredInThisFrame = true;
        }
        else if (ball.transform.position.x < -fieldWidthHalf) {
            rightScore++;
            NewPointScoredInThisFrame = true;
        }
    }
}
