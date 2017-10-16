using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform playerLoc;


	// Use this for initialization
	void Start () 
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void LateUpdate()
    {
        Vector3 camPos = transform.position;
        Vector3 playerPos = playerLoc.position;
        Vector3 velocity = Vector3.zero;
        //lerp follow player, constrain movement to the up axis.
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x,(playerPos.y+2),transform.position.z), ref velocity, Time.fixedDeltaTime*5);
    }
}
