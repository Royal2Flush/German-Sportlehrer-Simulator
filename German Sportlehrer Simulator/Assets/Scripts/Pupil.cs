using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupil : Person {

    public static float hitCooldownTimer = 5f;
    public float movementSpeed;
    
    private const float IDLE_MIN = 0.5f;
    private const float IDLE_MAX = 2f;

    protected override void SpecificUpdate()
    {
        
    }

    public override void OnHit()
    {
        if (state == PersonState.sleeping)
        {
            Debug.Log("+1 point");
        }
        else
        {
            Debug.Log("-1 point");
        }

        state = PersonState.hit;
        timerToStateChange = hitCooldownTimer;
    }

    protected override void UpdateIdle()
    {
        timerToStateChange -= Time.deltaTime;
        if (timerToStateChange < 0)
        {
            SetMovementGoalAtRandom(movementSpeed);
            state = PersonState.moving;
        }
    }

    protected override void UpdateMoving()
    {
        base.Move();
    }

    protected override void UpdateSleeping()
    {
        
    }

    protected override void UpdateHit()
    {
        timerToStateChange -= Time.deltaTime;
        if (timerToStateChange < 0)
        {
            SetIdleTimer();
            state = PersonState.idle;
        }
        
    }

    protected override void SetIdleTimer()
    {
        timerToStateChange = Random.Range(IDLE_MIN, IDLE_MAX);
    }

}
