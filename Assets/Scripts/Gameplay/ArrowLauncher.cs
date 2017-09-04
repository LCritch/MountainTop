using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour {

    public GameObject arrowShooter;
    public GameObject arrowEnd;
    public GameObject arrowPrefab;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public float delayBetweenArrows = 0;
    
    //how long the arrow takes to reach the end destination in seconds
    public float speedOfArrow = 0;

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
        SetArrowPrefab();
        StartCoroutine(DelayArrowFire(delayBetweenArrows));
	}
	

	// Update is called once per frame
	void Update () 
    {

	}

    //Set arrow to fire and rotate the object in the appropriate direction
    void FireArrow()
    {
        switch(aDirection)
        {
            case arrowDirection.Left:
                arrowPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case arrowDirection.Right:
                arrowPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            case arrowDirection.Up:
                arrowPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case arrowDirection.Down:
                arrowPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;

        }
        StartCoroutine(FireArrowAtDirection(speedOfArrow));
        StopCoroutine("DelayArrowFire");
    }

    public IEnumerator FireArrowAtDirection(float seconds)
    {
        float time = 0;
        while(time < seconds)
        {
            arrowPrefab.transform.position = Vector3.Lerp(startPosition, endPosition, (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        arrowPrefab.transform.position = endPosition;
        Debug.Log("arrow Fired");
        
        if(arrowPrefab.transform.position == endPosition)
        {
            Debug.Log("Moving arrow back to startPos");
            StartCoroutine(DelayArrowReturn(1.0f));
        }
        
    }

    public IEnumerator DelayArrowFire(float secondsToWait)
    {
        StopCoroutine("DelayArrowReturn");
        yield return new WaitForSeconds(secondsToWait);
        Debug.Log("wait over, fire arrow");
        FireArrow();
    }

    public IEnumerator DelayArrowReturn(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        arrowPrefab.transform.position = startPosition;
        StartCoroutine(DelayArrowFire(delayBetweenArrows));
        StopCoroutine("FireArrowAtDirection");
    }

    //Instantiate arrow prefab and set its startLoc and parent info
    void SetArrowPrefab()
    {
        arrowPrefab = Instantiate(Resources.Load("Prefabs/Arrow")) as GameObject;
        arrowPrefab.transform.position = startPosition;
        arrowPrefab.transform.parent = gameObject.transform;
        arrowPrefab.GetComponent<Obstacle>().parent = gameObject;
    }

}
