using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{

    public GameObject attachedHazard;
    public bool blockActive;
    public bool isMoveable;
    public bool canCrumble;

    public List<GameObject> blockMovementPositions;
    public List<Vector3> blockLocations;

    void Awake()
    {
        blockLocations.Add(transform.position);
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
    IEnumerator MoveBlockPositionForward(float secondsBetweenPositons)
    {
        yield return new WaitForSeconds(secondsBetweenPositons);

    }

    IEnumerator MoveBlockPositionBackward(float secondsBetweenPositions)
    {
        yield return new WaitForSeconds(secondsBetweenPositions);

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
