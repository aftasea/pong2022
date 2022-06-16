using UnityEngine;
using UnityEngine.UI;

public class WaitingToStartState : GameStateMachine.IState {
	private Ball ball;
	private Score score;
	private UIScore leftScoreLabel;
	private UIScore rightScoreLabel;
	private WinnerMessage winnerMessage;
	private Text restartMessage;
	
	public WaitingToStartState(
		Ball ball,
		Score score,
		UIScore leftScoreLabel,
		UIScore rightScoreLabel,
		WinnerMessage winnerMessage,
		Text restartMessage)
	{
		this.ball = ball;
		this.score = score;
		this.leftScoreLabel = leftScoreLabel;
		this.rightScoreLabel = rightScoreLabel;
		this.winnerMessage = winnerMessage;
		this.restartMessage = restartMessage;
	}
	
	public GameStateMachine.StateId GetId() => GameStateMachine.StateId.WaitingToStart;
	
	public void OnEnter() { }

	public void Execute(GameStateMachine stateMachine) {
		if (Input.anyKeyDown) {
			ball.Serve();
			score.Reset();
			leftScoreLabel.UpdateScore(score.GetLeftScore());
			rightScoreLabel.UpdateScore(score.GetRightScore());
			winnerMessage.Hide();
			restartMessage.gameObject.SetActive(false);
			
			stateMachine.ChangeState(GameStateMachine.StateId.Playing);
		}
	}
}
