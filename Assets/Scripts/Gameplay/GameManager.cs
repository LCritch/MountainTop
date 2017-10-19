using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> Blocks;
    public GameObject startPoint;
    public GameObject Player;
    public Camera mainCamera;

    public int blockAmount;
    public int deaths;

    public DeathUI deathScreen;
    public PlayerController pController;

	// Use this for initialization
	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pController = Player.GetComponent<PlayerController>();
        fillBlockList();
        blockAmount = Blocks.Count;
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(respawnPlayer());
        }
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
    }


    public IEnumerator respawnPlayer()
    {
        //Start death events
        StartCoroutine(mainCamera.GetComponent<FollowCamera>().ZoomOnPlayer());
        deathScreen.startDeathScreen();
        Player.GetComponent<Rigidbody2D>().Sleep();
        yield return new WaitForSeconds(1.5f);
        DeactivateBlockEvent();
        Player.GetComponent<SpriteRenderer>().enabled = false;
        SetPlayerState(false);

        yield return new WaitForSeconds(2.0f);

        //Respawn player and allow to move again
        SetPlayerState(true);
        Player.transform.position = startPoint.transform.position;
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Player.GetComponent<Rigidbody2D>().WakeUp();

    }

    void DeactivateBlockEvent()
    {
        deactivateRandomBlock();
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
        if(blockAmount > 1)
        {
            blockAmount--;
            deathScreen.remainingBlocks = blockAmount;
            deathScreen.blockRemainingText.text = "Blocks Remaining: " + deathScreen.remainingBlocks;
        }
        else
        {
            deathScreen.blockRemainingText.color = Color.red;
            deathScreen.blockRemainingText.fontSize = 100;
            deathScreen.blockRemainingText.text = "GAME OVER";
        }
    }

    void SetPlayerState(bool alive)
    {
        if(alive && blockAmount >= 1)
        {
           pController.pState = PlayerController.PlayerState.Alive;
        }
        else 
        {
           pController.pState = PlayerController.PlayerState.Dead;
        }
    }
}
