using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform playerLoc;
    public bool playerAlive;
    Vector3 originalCamPosition;
    public float timeScale;

	// Use this for initialization
	void Start () 
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerAlive = true;
        timeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void FixedUpdate()
    {
        Vector3 camPos = transform.position;
        Vector3 playerPos = playerLoc.position;
        Vector3 velocity = Vector3.zero;
        //lerp follow player, constrain movement to the up axis.
        if (playerAlive)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, (playerPos.y + 2), transform.position.z),ref velocity, Time.deltaTime * 5);
        }
    }

    public IEnumerator ZoomOnPlayer()
    {
        float seconds = 2.0f;
        float time = 0;
        originalCamPosition = transform.position;
        playerAlive = false;

        while(time<seconds)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, timeScale / 2, (time / seconds));
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 2, (time / seconds)/50);
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerLoc.position.x, playerLoc.position.y, transform.position.z), (time / seconds) / 50);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ZoomOutPlayer());
    }

    public IEnumerator ZoomOutPlayer()
    {
        timeScale = Time.timeScale;
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1.0f;
        timeScale = Time.timeScale;
        GetComponent<Camera>().orthographicSize = 5;
        transform.position = originalCamPosition;
        playerAlive = true;
    }
}
