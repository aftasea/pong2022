using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score {
    
    private Ball ball;
    private float fieldWidthHalf;

    public bool NewPointScoredInThisFrame {
        get;
        private set;
    }

    public int LeftScore {
        get;
        private set;
    }
    public int RightScore {
        get;
        private set;
    }

    public void Init(Ball ball, float fieldWidthHalf) {
        this.ball = ball;
        this.fieldWidthHalf = fieldWidthHalf;
    }

    public void Reset() {
        this.LeftScore = 0;
        this.RightScore = 0;
    }
    
    public void Update() {
        this.NewPointScoredInThisFrame = false;
        if (this.ball.transform.position.x > fieldWidthHalf) {
            this.LeftScore++;
            this.NewPointScoredInThisFrame = true;
        }
        else if (this.ball.transform.position.x < -fieldWidthHalf) {
            this.RightScore++;
            this.NewPointScoredInThisFrame = true;
        }
    }
}
