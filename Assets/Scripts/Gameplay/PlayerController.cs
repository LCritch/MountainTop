using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool grounded;
    public GameObject player;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
        grounded = checkGroundCollision();
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
		if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 10));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (Time.deltaTime * 10));
        }
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //do a transform on an enumerator (look at NGV2 dash for how to)
            //transform.Translate(Vector3.up * (Time.deltaTime * 50));
            //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50);
            StartCoroutine("Jump");
        }
	}

    public IEnumerator Jump()
    {
        float elapsedTime = 0;
        float seconds = Time.deltaTime * 8;
        rb.velocity = Vector2.zero;
        Vector2 jumpVector = Vector2.up * 160;

        while(elapsedTime < seconds)
        {
            float portionComplete = elapsedTime/seconds;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, portionComplete);
            GetComponent<Rigidbody2D>().AddForce(thisFrameJumpVector);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StopCoroutine("Jump");
    }

    private bool checkGroundCollision()
    {
        //using layermask, tell the raycast which layers the check is trying to hit
        int layerMask = 1 << LayerMask.NameToLayer("Collision");

        //get bounds of the capsule collider so we can get the coords of the bottom
        Bounds boxBounds = GetComponent<BoxCollider2D>().bounds;
        Debug.DrawRay(transform.position, Vector3.down*1.05f, Color.red);

        //use physics raycast to see if there is anything below the player then allow the jump, limit the distance to make sure its grounded
            if (Physics2D.Raycast(boxBounds.center, Vector3.down, 1.05f, layerMask))
            {
                return true;
            }
        
        //Debug.Log("Not Grounded");
        return false;
    }
}
