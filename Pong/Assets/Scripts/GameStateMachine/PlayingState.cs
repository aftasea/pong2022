using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : GameStateMachine.IState
{
    private Ball ball;
    private Score score;
    private UIScore leftScoreLabel;
    private UIScore rightScoreLabel;
    private PaddleController leftPaddleController;
    private PaddleController rightPaddleController;
    private int scoreToWin;
	
    public PlayingState(
        Ball ball,
        Score score,
        UIScore leftScoreLabel,
        UIScore rightScoreLabel,
        PaddleController leftPaddleController,
        PaddleController rightPaddleController,
        int scoreToWin
        )
    {
        this.ball = ball;
        this.score = score;
        this.leftScoreLabel = leftScoreLabel;
        this.rightScoreLabel = rightScoreLabel;
        this.leftPaddleController = leftPaddleController;
        this.rightPaddleController = rightPaddleController;
        this.scoreToWin = scoreToWin;
    }
	
    public GameStateMachine.StateId GetId() => GameStateMachine.StateId.Playing;
	
    public void OnEnter() { }

    public void Execute(GameStateMachine stateMachine) {
        leftPaddleController.Update();
        rightPaddleController.Update();
        ball.UpdatePosition();
        score.Update();
        
        CheckScoreState(stateMachine);

        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }
    }

    private void CheckScoreState(GameStateMachine stateMachine) {
        if (score.NewPointScoredInThisFrame) {
            leftScoreLabel.UpdateScore(score.LeftScore);
            rightScoreLabel.UpdateScore(score.RightScore);

            if (IsGameOver)
                stateMachine.ChangeState(GameStateMachine.StateId.GameOver);
            else
                ball.Serve();
        }
    }

    private bool IsGameOver => score.LeftScore == scoreToWin || score.RightScore == scoreToWin;

    private void Reset() {
#if UNITY_EDITOR
        leftPaddleController.Reset();
        rightPaddleController.Reset();
        ball.Reset();
#endif
    }
}
