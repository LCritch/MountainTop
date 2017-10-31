using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum PlayerState
    {
        Alive,
        Dead
    }

    public PlayerState pState;
    bool jumpDown;
    public bool grounded;
    public GameObject player;

    public GameManager gm;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        pState = PlayerState.Alive;
	}
	
    void Update()
    {
        //TODO Add an InputManager addon to Unity, set the relevant buttons and feed them into this script.
        grounded = checkGroundCollision();
        if (pState == PlayerState.Alive && gm.gState == GameState.InGame)
        {
            if (Input.GetKey(KeyCode.D))
            {
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.Translate(Vector3.right * (Time.deltaTime * 10));
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.Translate(Vector3.right * (Time.deltaTime * 10));
            }
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                StartCoroutine("Jump");
            }
        }
        else
        {
            StopCoroutine("Jump");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO Add Pause Menu with buttons for Main Menu/Resume/Options/Exit Game
            switch(gm.gState)
            {
                case GameState.InGame:
                    gm.PauseGame(); break;

                case GameState.Paused:
                    gm.ResumeGame(); break;
            }
        }
    }

	void FixedUpdate () 
    {

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
        Debug.DrawRay(transform.position, Vector3.down*0.7f, Color.red);

        //use physics raycast to see if there is anything below the player then allow the jump, limit the distance to make sure its grounded
            if (Physics2D.Raycast(boxBounds.center, Vector3.down, 0.7f, layerMask) && gm.gState == GameState.InGame)
            {
                return true;
            }
        
        //Debug.Log("Not Grounded");
        return false;
    }
}
