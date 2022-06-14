using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private Paddle leftPaddle;
    private Paddle rightPaddle;
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
            this.leftPaddle = paddles[0];
            this.rightPaddle = paddles[1];
        }
        else {
            this.leftPaddle = paddles[1];
            this.rightPaddle = paddles[0];
        }
        
        float fieldHalfHeight = this.fieldBounds.extents.y; 
        this.leftPaddle.Init(fieldHalfHeight, new PaddleController());
        this.rightPaddle.Init(fieldHalfHeight, new PaddleController());
        ball = FindObjectOfType<Ball>();
        ball.Init(fieldHalfHeight, this.leftPaddle, this.rightPaddle);

        score = new Score();
        score.Init(this.ball, this.fieldBounds.extents.x);
        
        UIScore[] scoreLabels = FindObjectsOfType<UIScore>();
        Assert.IsTrue(scoreLabels.Length == 2, "There are not two score labels in the scene");
        if (scoreLabels[0].transform.position.x < 0f) {
            this.leftScoreLabel = scoreLabels[0];
            this.rightScoreLabel = scoreLabels[1];
        }
        else {
            this.leftScoreLabel = scoreLabels[1];
            this.rightScoreLabel = scoreLabels[0];
        }

        this.winnerMessage = FindObjectOfType<WinnerMessage>(true);
        this.winnerMessage.Hide();
        
        this.restartMessage.gameObject.SetActive(false);
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                this.WaitForInput();
                break;
            case State.Playing:
                UpdateGameplay();
                break;
            case State.GameOver:
                this.winnerMessage.Show(this.score);
                StartCoroutine(this.DelayRestartMessage());
                state = State.RetryMessage;
                break;
            case State.RetryMessage:
                break;
        }
        
        
    }

    private IEnumerator DelayRestartMessage() {
        yield return new WaitForSeconds(2f);
        this.restartMessage.gameObject.SetActive(true);
        this.state = State.WaitingToStart;
    }
    

    private void WaitForInput() {
        if (Input.anyKeyDown) {
            // reset all here
            score.Reset();
            leftScoreLabel.UpdateScore(this.score.LeftScore);
            this.rightScoreLabel.UpdateScore(this.score.RightScore);
            this.restartMessage.gameObject.SetActive(false);
            state = State.Playing;
        }
    }

    private void UpdateGameplay() {
        this.leftPaddle.UpdatePosition();
        this.rightPaddle.UpdatePosition();
        this.ball.UpdatePosition();

        this.score.Update();
        if (this.score.NewPointScoredInThisFrame) {
            leftScoreLabel.UpdateScore(this.score.LeftScore);
            this.rightScoreLabel.UpdateScore(this.score.RightScore);

            if (this.score.LeftScore == scoreToWin || this.score.RightScore == scoreToWin)
                this.state = State.GameOver;
            else
                this.Reset();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            this.Reset();
        }
    }

    private void Reset() {
        this.leftPaddle.Reset();
        this.rightPaddle.Reset();
        this.ball.Reset();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.fieldBounds.center, this.fieldBounds.size);
    }
}
