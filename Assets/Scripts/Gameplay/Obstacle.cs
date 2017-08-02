using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		
	}
	

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //kill the player, activate the respawn event.
            Debug.Log("KillPlayer at: " + other.transform.position.ToString());
        }
    }

	// Update is called once per frame
	void Update () 
    {
		
	}
}
