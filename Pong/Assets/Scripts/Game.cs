using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;
    [SerializeField] private Ball ball;
    [SerializeField] private UIScore leftScoreLabel;
    [SerializeField] private UIScore rightScoreLabel;
    [SerializeField] private WinnerMessage winnerMessage;
    [SerializeField] private Text restartMessage;
    
    private PaddleController leftPaddleController;
    private PaddleController rightPaddleController;
    private Score score;
    private GameStateMachine stateMachine;
    
    [Header("Config")]
    [SerializeField] private int scoreToWin;
    [SerializeField] private int delayForRestartMessage;
    [SerializeField] private Bounds fieldBounds;
    
    private void Awake() {
        float fieldHalfHeight = fieldBounds.extents.y; 
        leftPaddle.Init(fieldHalfHeight);
        rightPaddle.Init(fieldHalfHeight);
        
        ball.Init(fieldHalfHeight, leftPaddle, rightPaddle);
        
        leftPaddleController = new PlayerPaddleController(leftPaddle);
        rightPaddleController = new AiPaddleController(rightPaddle, ball);
        
        // UI
        score = new Score();
        score.Init(ball, fieldBounds.extents.x);
        winnerMessage.Hide();
        restartMessage.gameObject.SetActive(false);
        
        // State Machine
        stateMachine = new GameStateMachine(new GameStateMachine.IState[] {
            new WaitingToStartState(ball, score, leftScoreLabel, rightScoreLabel, winnerMessage, restartMessage),
            new PlayingState(ball, score, leftScoreLabel, rightScoreLabel, leftPaddleController, rightPaddleController, this.scoreToWin),
            new GameOverState(score, winnerMessage, restartMessage, delayForRestartMessage),
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
