public class Score : IScoreProvider
{
    private Ball ball;
    private float fieldWidthHalf;

    public bool NewPointScoredInThisFrame {
        get;
        private set;
    }

    private int leftScore;
    private int rightScore;

    public int GetLeftScore() => leftScore;

    public int GetRightScore() => rightScore;

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
