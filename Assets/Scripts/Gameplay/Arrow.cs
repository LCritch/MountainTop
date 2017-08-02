using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public GameObject arrowShooter;
    public GameObject arrowEnd;
    public GameObject arrowPrefab;

    public Vector3 startPosition;
    public Vector3 endPosition;

    //enum to determine which way to fire the arrow
    public enum arrowDirection
    {
        Left,
        Right,
        Up, 
        Down
    }

    public arrowDirection aDirection;

	// Use this for initialization
	void Start () 
    {
        arrowShooter = gameObject;
        arrowEnd = transform.GetChild(0).gameObject;
        startPosition = arrowShooter.transform.position;
        endPosition = arrowEnd.transform.position;
        arrowPrefab = Instantiate(Resources.Load("Prefabs/Arrow")) as GameObject;
        arrowPrefab.transform.position = startPosition;
		
	}
	

	// Update is called once per frame
	void Update () 
    {
        FireArrow();
	}

    void FireArrow()
    {
        switch(aDirection)
        {
            case arrowDirection.Left:
                break;
        }
    }

}
