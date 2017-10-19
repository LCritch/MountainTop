using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{

    public GameObject attachedHazard;
    public SpriteRenderer blockSprRen;

    public bool blockActive;
    public bool isMoveable;
    public bool canCrumble;
    public float crumbleTime;

    public List<GameObject> blockMovementPositions;
    public List<Vector3> blockLocations;

    void Awake()
    {
        blockLocations.Add(transform.position);
        blockSprRen = GetComponent<SpriteRenderer>();
    }

	void Start () 
    {
        blockActive = true;
        AddPositionsToVectorArray();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && canCrumble)
        {
            StartCoroutine(CrumbleBlock());
        }
    }

    void AddPositionsToVectorArray()
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i) != null)
            {
                blockLocations.Add(transform.GetChild(i).position);
            }
            else
            {
                Debug.Log("Failed to add pos location for blockmovementPos: " + i);
            }
        }
    }

    //TODO Add ability for blocks to Move/Crumble over time.
    IEnumerator MoveBlockPositionForward(Vector3 currentPosition,Vector3 positionMoveTo, float secondsBetweenPositons)
    {
        yield return new WaitForSeconds(secondsBetweenPositons);
        float distanceSeconds = Vector3.Distance(transform.position, positionMoveTo);
        float time = 0;

        while(time < distanceSeconds)
        {
            transform.position = Vector3.Lerp(transform.position, positionMoveTo, (time / distanceSeconds));
            time += Time.deltaTime;
        }
        /* TODO Function needs to know what the current position is and if there is a next position in the array
         * if last position then do a loop to move position backwards same as forwards,
         * then check again and reverse to the last point
         */
    }

    IEnumerator MoveBlockPositionBackward(Vector3 currentPosition, Vector3 positionMoveTo, float secondsBetweenPositions)
    {
        yield return new WaitForSeconds(secondsBetweenPositions);

    }

    IEnumerator CrumbleBlock()
    {
        //TODO remove this crumbled block from the existing blocks array in the GameManager
        float seconds = crumbleTime;
        float time = 0;

        StartCoroutine(SetBlockInactive(crumbleTime));

        while (time < seconds)
        {
            blockSprRen.color = Color.Lerp(blockSprRen.color, Color.clear, (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator SetBlockInactive (float seconds)
    {
        yield return new WaitForSeconds(seconds/4);
        DisableBlock();
    }

    public void DisableBlock()
    {
        blockActive = false;

        if(attachedHazard != null)
        {
            gameObject.SetActive(false);
            attachedHazard.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
