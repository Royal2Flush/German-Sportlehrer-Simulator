using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonState { idle, moving, sleeping, hit }

public abstract class Person : MonoBehaviour {

    protected PersonState state;
    private Vector3 currentGoal;
    private float currentMovementSpeed;

	// Use this for initialization
	void Start ()
    {
        state = PersonState.idle;
        currentGoal = gameObject.transform.position;
        currentMovementSpeed = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case PersonState.idle:
                UpdateIdle();
                break;
            case PersonState.moving:
                UpdateMoving();
                break;
            case PersonState.sleeping:
                UpdateSleeping();
                break;
            case PersonState.hit:
                UpdateHit();
                break;
        }
    }

    public abstract void OnHit();
    protected abstract void SpecificUpdate();
    protected abstract void UpdateIdle();
    protected abstract void UpdateMoving();
    protected abstract void UpdateSleeping();
    protected abstract void UpdateHit();
    protected abstract void SetIdleTimer();

    public void MoveInDirectionOnce(Vector3 direction, float speed)
    {
        gameObject.transform.Translate(Vector3.Normalize(direction) * speed * Time.deltaTime);
    }

    public void SetMovementGoal(Vector3 goal, float speed)
    {
        state = PersonState.moving;
        currentGoal = goal;
        currentMovementSpeed = speed;
    }

    public void SetMovementGoalAtRandom(float speed)
    {
        SetMovementGoal(new Vector3(Random.Range(-5f, 5f), 0, Random.Range(0f, 5f)), speed);
    }

    protected void Move()
    {
        if(state == PersonState.moving)
        {
            MoveInDirectionOnce(currentGoal - gameObject.transform.position, currentMovementSpeed);
            if (Vector3.Distance(currentGoal, gameObject.transform.position) < currentMovementSpeed * Time.deltaTime)
            {
                gameObject.transform.position = currentGoal;
                state = PersonState.idle;
                SetIdleTimer();
            }
        }
    }
}
