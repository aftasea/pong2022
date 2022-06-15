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
    private GameStateMachine stateMachine;
    [SerializeField]
    private Text restartMessage;
    
    [Header("Config")]
    [SerializeField] private Bounds fieldBounds;
    [SerializeField] private int scoreToWin;
    
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
        
        
        // State Machine
        stateMachine = new GameStateMachine(new GameStateMachine.State[] {
            new WaitingToStartState(ball, score, leftScoreLabel, rightScoreLabel, winnerMessage, restartMessage),
            new PlayingState(ball, score, leftScoreLabel, rightScoreLabel, leftPaddleController, rightPaddleController, this.scoreToWin),
            new GameOverState(score, winnerMessage, restartMessage, 2f),
        });
    }

    private void Update() {
        this.stateMachine.Update();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(fieldBounds.center, fieldBounds.size);
    }
}
