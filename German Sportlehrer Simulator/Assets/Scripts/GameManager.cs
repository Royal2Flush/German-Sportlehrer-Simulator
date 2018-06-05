using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int numOfPupilsAtStart;
    public Pupil originalPupil;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;

    private float timeSinceLastSpawn;
    private float timeToNewSpawn;

	// Use this for initialization
	void Start () {

        for(int i=0; i<numOfPupilsAtStart-1; i++)
        {
            SpawnNewPupil();
        }
        originalPupil.Spawn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;
		if(timeSinceLastSpawn >= timeToNewSpawn)
        {
			Debug.Log ("Spawn");
            SpawnNewPupil();
        }
	}

    private void SpawnNewPupil()
    {
        Pupil newPupil = Instantiate(originalPupil.gameObject).GetComponent<Pupil>();
        newPupil.Spawn();
        timeSinceLastSpawn = 0;
        timeToNewSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }
}
