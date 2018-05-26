using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonState { idle, moving, sleeping, hit }

public abstract class Person : MonoBehaviour {

    protected PersonState state;
    protected float timerToStateChange;
	protected Animator animator;
    public Vector3 currentGoal;
    private float currentMovementSpeed;

    

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

	public abstract void OnHit(int damage);
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
        SetMovementGoal(GetRandomVectorInMovementZone(), speed);
    }

    protected static Vector3 GetRandomVectorInMovementZone()
    {
        const float X_MIN = -6;
        const float X_MAX = 6;
        const float Z_MIN = 0;
        const float Z_MAX = 10;
        const float Y_POS = 1.4f;
        Vector3 r = new Vector3(Random.Range(X_MIN, X_MAX), Y_POS, Random.Range(Z_MIN, Z_MAX));
        Debug.Log(r);
        return r;
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

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ball") {
			int damage = col.gameObject.GetComponent<BallBehaviour> ().getDamage ();
			OnHit (damage);
		}
	}

    public void Spawn(Vector3 position)
    {
        gameObject.transform.position = position;
        state = PersonState.idle;
        currentGoal = gameObject.transform.position;
        currentMovementSpeed = 0f;
        animator = GetComponent<Animator>();
    }

    public abstract void Spawn();
}
