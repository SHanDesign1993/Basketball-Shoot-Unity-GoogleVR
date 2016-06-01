using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour {
    public GameObject lightprefab;
    public GameObject ballprefab;
    public Transform baseketpos;
    public Vector3 ballpos;
    public float TouchTime ;
    public Vector3 meshpos;
    public Vector3 throwpos;
    public float ThrowForceSpeed = 5.0f;
    public float Throwforce = 0.0f;
    public float maxThrowforce = 10.0f;
    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
    bool isDragging = false;
    //bool isJumping = false;
    public Vector2 barPos = new Vector2(0, 0);
    public Vector2 barSize;
    public Texture2D emptyBarTex;
    public Texture2D fullBarTex;
	// Use this for initialization
	void Start () {
        lightprefab.SetActive(false);
        barSize = new Vector2(Screen.width / 3, Screen.width / 10.8f);
	}

    void OnGUI()
    {
        if (FSPlayer.bStart)
        {
            //draw the background:
            GUI.BeginGroup(new Rect(barPos.x, barPos.y, barSize.x, barSize.y));
            GUI.Box(new Rect(barPos.x, barPos.y, barSize.x, barSize.y), emptyBarTex);

            //draw the filled-in part:
            GUI.BeginGroup(new Rect(barPos.x, barPos.y, barSize.x * Throwforce / 10, barSize.y));
            GUI.Box(new Rect(barPos.x, barPos.y, barSize.x, barSize.y), fullBarTex);
            GUI.EndGroup();
            GUI.EndGroup();
        }
       
    }
	
	// Update is called once per frame
	void Update () {

        if (FSPlayer.bStart)
        {
            //transform.Translate(Input.acceleration.x * 0.25f, 0, -Input.acceleration.z * 0.25f);
            //Debug.DrawRay(transform.position, new Vector3(Screen.width / 2 - transform.position.x, Screen.height / 2 - transform.position.y, 0) * 1000, Color.yellow);
            meshpos = GameObject.Find("Cardboard Reticle").transform.position;
            transform.LookAt(baseketpos.position);

            for (var i = 0; i < Input.touchCount; ++i)
            {

                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    TouchTime = Time.time;
                }

                if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
                {
                    if (Time.time - TouchTime <= 0.5)
                    {
                        //do stuff as a tap​
                    }
                    else
                    {
                        // this is a long press or drag​
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }

            if (isDragging)
            {
                //Vector3 newRotation = transform.rotation.eulerAngles;
                ballpos = Camera.main.transform.position - Camera.main.transform.up * 0.3f + Camera.main.transform.forward * 0.8f;

                ballprefab.transform.position = ballpos;
                ballprefab.GetComponent<Rigidbody>().velocity = Vector3.zero; 		   // Sets the velocity
                ballprefab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //ballprefab.transform.eulerAngles = newRotation;
                ballprefab.GetComponent<Rigidbody>().freezeRotation = true;
                if (Throwforce < maxThrowforce)
                {
                    Throwforce += Time.deltaTime * ThrowForceSpeed;
                }
                if (Throwforce >= maxThrowforce)
                {
                    Throwforce = 0.0f;
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                //Handheld.Vibrate();
                isDragging = false;

                ballprefab.GetComponent<Rigidbody>().freezeRotation = false;
                

                ballprefab.transform.position = ballpos;
                meshpos = GameObject.Find("Cardboard Reticle").transform.position;
                throwpos = (meshpos - ballpos).normalized;
                if (throwpos.z < 0) { throwpos.z = -throwpos.z; }
                throwpos.y = 0.5f - throwpos.y;
                throwpos.z = 3 + (throwpos.z * Throwforce) * 0.5f;
                //ballprefab.GetComponent<Rigidbody>().velocity = transform.TransformDirection(throwpos);
                ballprefab.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 50 * throwpos.z + Camera.main.transform.up * 250 + Camera.main.transform.up * 200 * throwpos.y);
               
                Throwforce = 0.0f;
            }


            if (Input.GetKeyDown("space") )
            {
                //Debug.Log("Jump!", this);
                //transform.GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.5f, 0f);
                //isJumping = true;
                //ballpos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                //ballprefab.transform.position = ballpos;
                //ballprefab.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 7.2f, 4.2f));

                //ballInstance.AddForce(new Vector3(0, 5, 5) * 30);
                //clone.velocity = ransform.TransformDirection(Vector3.forward * 10);
                //thrownItem.transform.rigidbody.AddeForce(transform.forward * ThrowForce, ForceMode.Impulse);
            }
        }


        
	
	}
}
