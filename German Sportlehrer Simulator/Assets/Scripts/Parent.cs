using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Person
{
    public float movementSpeed;

	public override void OnHit(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetIdleTimer()
    {
        timerToStateChange = 0;
    }

    protected override void SpecificUpdate()
    {
        
    }

    protected override void UpdateHit()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateIdle()
    {
        GameObject player = GetComponentInParent<Teacher>().gameObject;
        SetMovementGoal(player.transform.position, movementSpeed);
        state = PersonState.moving;
    }

    protected override void UpdateMoving()
    {
        base.Move();
    }

    protected override void UpdateSleeping()
    {
        throw new System.NotImplementedException();
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }
}
