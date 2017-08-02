using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public GameObject attachedHazard;
    public bool blockActive;




	// Use this for initialization
	void Start () 
    {
        blockActive = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
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
