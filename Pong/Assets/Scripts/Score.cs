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
        LeftScore = 0;
        RightScore = 0;
    }
    
    public void Update() {
        NewPointScoredInThisFrame = false;
        if (ball.transform.position.x > fieldWidthHalf) {
            LeftScore++;
            NewPointScoredInThisFrame = true;
        }
        else if (ball.transform.position.x < -fieldWidthHalf) {
            RightScore++;
            NewPointScoredInThisFrame = true;
        }
    }
}
