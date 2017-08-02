using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> Blocks;
    public Vector3 startPoint;
    public GameObject Player;

    public int blockAmount;
    public int deaths;

    public DeathUI deathScreen;

	// Use this for initialization
	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        fillBlockList();
        //deathScreen.remainingBlocks = blockAmount;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void fillBlockList()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (go.GetComponent<Block>().blockActive)
            {
                Blocks.Add(go);
            }
        }
        blockAmount = Blocks.Count;
    }

    void respawnPlayer()
    {
        Player.transform.position = startPoint;
        updateBlockCount();
        Blocks.Clear();
        fillBlockList();
    }

    void deactivateRandomBlock()
    {
        int blockToDeactivate = Random.Range(0, Blocks.Count);

        Blocks[blockToDeactivate].GetComponent<Block>().DisableBlock();
    }

    void updateBlockCount()
    {
        //decrement the amount of blocks, send this value to the deathUI to show whilst respawning
        if(blockAmount != 0)
        {
            blockAmount--;
            deathScreen.remainingBlocks = blockAmount;
        }
    }
}
