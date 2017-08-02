using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public GameObject arrowShooter;
    public GameObject arrowEnd;

    public Vector3 startPosition;
    public Vector3 endPosition;

	// Use this for initialization
	void Start () 
    {
        startPosition = arrowShooter.transform.position;
        endPosition = arrowEnd.transform.position;
		
	}
	

	// Update is called once per frame
	void Update () 
    {
		
	}
}
