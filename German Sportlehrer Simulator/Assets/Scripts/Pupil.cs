using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupil : Person {

    public static float hitCooldownTimer = 5f;
    public float movementSpeed;
    private SpriteRenderer zzz;
    
    private const float IDLE_MIN = 0.5f;
    private const float IDLE_MAX = 2f;


    AudioSource pupilAudio;





    protected override void SpecificUpdate()
    {
        
    }

	public override void OnHit(int damage)
	{
		playPupilAudio();

		if (damage == 0)
		{
			return;
		}

        if (state == PersonState.sleeping)
        {
			animator.SetTrigger ("GotHit");
			UIManager.Instance.AddPoints (damage);
        }
        else
        {
			animator.SetTrigger ("GotHit");
			UIManager.Instance.AddPoints (-1);
        }

        state = PersonState.hit;
        timerToStateChange = hitCooldownTimer;
    }

    protected override void UpdateIdle()
    {
		Debug.Log ("Update idle");
        timerToStateChange -= Time.deltaTime;
        if (timerToStateChange < 0)
        {
			if (Random.value > 0.5f) {
				SetMovementGoalAtRandom (movementSpeed);
				state = PersonState.moving;
				animator.SetTrigger ("IsAwake");
			} else {
				SetMovementGoalAtRandom (0);
				state = PersonState.sleeping;
				animator.SetTrigger ("IsSleeping");
                zzz.gameObject.SetActive(true);
                timerToStateChange = 10f;
			}
			Debug.Log (state);
        }
    }

    protected override void UpdateMoving()
    {
        base.Move();
    }

    protected override void UpdateSleeping()
    {
        timerToStateChange -= Time.deltaTime;
        if(timerToStateChange <= 0)
        {
            zzz.gameObject.SetActive(false);
            SetMovementGoalAtRandom(movementSpeed);
            state = PersonState.moving;
            animator.SetTrigger("IsAwake");
        }
    }

    protected override void UpdateHit()
    {
        //timerToStateChange -= Time.deltaTime;
        //if (timerToStateChange < 0)
        //{
            //SetIdleTimer();
            //state = PersonState.idle;
        //}
        
    }

    protected override void SetIdleTimer()
    {
        timerToStateChange = Random.Range(IDLE_MIN, IDLE_MAX);
    }

    public override void Spawn()
    {
		int index = Random.Range (0, 2);
		Debug.Log (index);
		transform.GetChild (index).gameObject.SetActive (true);
        Spawn(GetRandomVectorInMovementZone());

        zzz = gameObject.GetComponentInChildren<SpriteRenderer>();
        zzz.gameObject.SetActive(false);
    }


    void playPupilAudio()
    {
        Debug.Log("getroffen");
        pupilAudio = this.GetComponent<AudioSource>();

        int audioindex = Random.Range(1, 6);

        AudioClip clip = Resources.Load<AudioClip>("Sounds/schueler" + audioindex) as AudioClip;
        Debug.Log("audio geladen");
        pupilAudio.clip = clip;
        pupilAudio.Play();
        Debug.Log("audio gespielt");

    }
}
