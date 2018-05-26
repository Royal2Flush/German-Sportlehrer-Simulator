using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour {



    float throwIntensity = 0;
    float throwTreshold = 30f;
    public GameObject ball;

	// Path ball stuf
	public GameObject PathBall;
	private float lastPathSpawn = 0.0f;

    Rigidbody ballRigid;



	// Use this for initialization
	void Start () {



		
	}
	
	// Update is called once per frame
	void Update () {



        float mousePosX = Input.mousePosition.x;
        float mousePosY = Screen.height - Input.mousePosition.y;

        float onePercentX = Screen.width / 100;
        float mousePosXInPercent = mousePosX / onePercentX;

        float onePercentY = Screen.height / 100;
        float mousePosYInPercent = mousePosY / onePercentY;

        //0 is in the middle not buttom left
        float rotateAmountX = mousePosXInPercent - 50;
        float verticalThrowAngle = mousePosYInPercent - 50;
        //Rote the Player in 50 degree

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, rotateAmountX/2, this.transform.eulerAngles.z);


        CalcThrowIntensity();

        Vector3 forceVector = transform.forward * throwIntensity;
        Quaternion rotation = Quaternion.Euler(verticalThrowAngle, 0, 0);
        forceVector = rotation * forceVector;

        if (Input.GetMouseButtonUp(0))
        {
            //throw ball
            GameObject ThrowBall = Resources.Load<GameObject>("throwBall") as GameObject;
            ThrowBall.transform.position = ball.transform.position;
            ThrowBall.transform.rotation = transform.rotation;

            GameObject newBall = Instantiate(ThrowBall);
            ballRigid = newBall.GetComponent<Rigidbody>();
            ballRigid.AddForce(forceVector,ForceMode.Impulse);

            throwIntensity = 0;
        }

		if (Time.time - lastPathSpawn > 0.1f) {

			GameObject ball = Instantiate(PathBall, transform.position, Quaternion.identity);
			ball.transform.rotation = this.gameObject.transform.rotation;
			ball.GetComponent<Rigidbody> ().AddForce (forceVector, ForceMode.Impulse);

			lastPathSpawn = Time.time;
		}

		
	}


    void CalcThrowIntensity()
    {
        if(Input.GetMouseButton(0))
        {
            throwIntensity += 0.1f;

        }

        else{
           // throwIntensity = 0;

        }

        if(throwIntensity > throwTreshold)
        {
            throwIntensity = throwTreshold;
            //spiele anspamnnsound

        }
            


    }
}
