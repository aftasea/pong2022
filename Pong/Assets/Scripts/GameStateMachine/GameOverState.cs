using UnityEngine;
using UnityEngine.UI;

public class GameOverState : GameStateMachine.IState
{
    private Score score;
    private WinnerMessage winnerMessage;
    private Text restartMessage;
    private float delayForRestartMessage;

    private float timeLeftForRestartMessage;
	
    public GameOverState(
        Score score,
        WinnerMessage winnerMessage,
        Text restartMessage,
        float delayForRestartMessage)
    {
        this.score = score;
        this.winnerMessage = winnerMessage;
        this.restartMessage = restartMessage;
        this.delayForRestartMessage = delayForRestartMessage;
    }
	
    public GameStateMachine.StateId GetId() => GameStateMachine.StateId.GameOver;

    public void OnEnter() {
        winnerMessage.Show(score);
        this.timeLeftForRestartMessage = delayForRestartMessage;
    }

    public void Execute(GameStateMachine stateMachine) {
        this.timeLeftForRestartMessage -= Time.deltaTime;
        
        if (this.timeLeftForRestartMessage < 0f) {
            restartMessage.gameObject.SetActive(true);
            stateMachine.ChangeState(GameStateMachine.StateId.WaitingToStart);
        }
    }
}
