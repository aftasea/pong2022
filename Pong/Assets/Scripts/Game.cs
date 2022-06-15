using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    private PaddleController leftPaddleController;
    private PaddleController rightPaddleController;
    private Ball ball;
    private Score score;
    private UIScore leftScoreLabel;
    private UIScore rightScoreLabel;
    private WinnerMessage winnerMessage;
    [SerializeField]
    private Text restartMessage;
    
    [Header("Config")]
    [SerializeField] private Bounds fieldBounds;
    [SerializeField] private int scoreToWin;
    
    private enum State {
        WaitingToStart,
        Playing,
        GameOver,
        RetryMessage,
    };

    private State state = State.WaitingToStart;

    private void Awake() {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        Assert.IsTrue(paddles.Length == 2, "There are not two paddles in the scene");
        if (paddles[0].transform.position.x < 0f) {
            leftPaddle = paddles[0];
            rightPaddle = paddles[1];
        }
        else {
            leftPaddle = paddles[1];
            rightPaddle = paddles[0];
        }
        
        float fieldHalfHeight = fieldBounds.extents.y; 
        leftPaddle.Init(fieldHalfHeight);
        rightPaddle.Init(fieldHalfHeight);
        
        
        
        ball = FindObjectOfType<Ball>();
        ball.Init(fieldHalfHeight, leftPaddle, rightPaddle);
        
        
        leftPaddleController = new PlayerPaddleController(leftPaddle);
        rightPaddleController = new AiPaddleController(rightPaddle, ball);
        

        // UI
        score = new Score();
        score.Init(ball, fieldBounds.extents.x);
        
        UIScore[] scoreLabels = FindObjectsOfType<UIScore>();
        Assert.IsTrue(scoreLabels.Length == 2, "There are not two score labels in the scene");
        if (scoreLabels[0].transform.position.x < 0f) {
            leftScoreLabel = scoreLabels[0];
            rightScoreLabel = scoreLabels[1];
        }
        else {
            leftScoreLabel = scoreLabels[1];
            rightScoreLabel = scoreLabels[0];
        }

        winnerMessage = FindObjectOfType<WinnerMessage>(true);
        winnerMessage.Hide();
        
        restartMessage.gameObject.SetActive(false);
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                WaitForInput();
                break;
            case State.Playing:
                UpdateGameplay();
                break;
            case State.GameOver:
                winnerMessage.Show(score);
                StartCoroutine(DelayRestartMessage());
                state = State.RetryMessage;
                break;
            case State.RetryMessage:
                break;
        }
        
        
    }

    private IEnumerator DelayRestartMessage() {
        yield return new WaitForSeconds(2f);
        restartMessage.gameObject.SetActive(true);
        state = State.WaitingToStart;
    }
    

    private void WaitForInput() {
        if (Input.anyKeyDown) {
            // reset all here
            ball.Serve();
            score.Reset();
            leftScoreLabel.UpdateScore(score.LeftScore);
            rightScoreLabel.UpdateScore(score.RightScore);
            winnerMessage.Hide();
            restartMessage.gameObject.SetActive(false);
            state = State.Playing;
        }
    }

    private void UpdateGameplay() {
        leftPaddleController.Update();
        rightPaddleController.Update();
        ball.UpdatePosition();

        score.Update();
        if (score.NewPointScoredInThisFrame) {
            leftScoreLabel.UpdateScore(score.LeftScore);
            rightScoreLabel.UpdateScore(score.RightScore);

            if (score.LeftScore == scoreToWin || score.RightScore == scoreToWin)
                state = State.GameOver;
            else
                ball.Serve();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }
    }

    private void Reset() {
        leftPaddleController.Reset();
        rightPaddleController.Reset();
        ball.Reset();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(fieldBounds.center, fieldBounds.size);
    }
}
