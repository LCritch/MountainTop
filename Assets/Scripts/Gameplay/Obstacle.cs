using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Arrow,
    Spike,
    Moving,
    Magic
}

public class Obstacle : MonoBehaviour {

    public ObstacleType oType;
    public GameObject parent;
    public Vector2 knockVector;
    public GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
    }

    //When player hits the Obstacle, what to do?
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            #region switch dependent on what Obstacle Type the object that hit the player it is
            switch (oType)
            {
                case ObstacleType.Arrow:
                    if (gameObject != null)
                    {
                        //kill the player, activate the respawn event.
                        Debug.Log("Arrow KillPlayer at: " + other.transform.position.ToString());
                        StartCoroutine(KnockBackPlayer(other.gameObject));
                    }
                    else
                    {
                        Debug.Log("failed to get gameobject attached to script");
                    }
                    break;

                case ObstacleType.Spike:
                    if(gameObject != null)
                    {
                        Debug.Log("Spike KillPlayer at: " + other.transform.position.ToString());
                        ActivatePlayerDeath();
                    }
                    else
                    {
                        Debug.Log("failed to get gameobject attached to script");
                    }
                    break;

            }
            #endregion
        }
    }


	// Update is called once per frame
	void Update () 
    {
		
	}

    void ActivatePlayerDeath()
    {
        StartCoroutine(gameManager.GetComponent<GameManager>().respawnPlayer());
    }

    //When an object hits the player, if needed then force the player backwards over a certain period of time
    public IEnumerator KnockBackPlayer(GameObject player)
    {
        float elapsedTime = 0;
        float seconds = Time.deltaTime * 16;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        SetKnockVector();

        while (elapsedTime < seconds)
        {
            float portionComplete = elapsedTime / seconds;
            Vector2 thisFrameJumpVector = Vector2.Lerp(knockVector, Vector2.zero, portionComplete);
            player.GetComponent<Rigidbody2D>().AddForce(thisFrameJumpVector);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
        ActivatePlayerDeath();

        StopCoroutine("Jump");
    }

    //dependent on rotation of arrow, set the appropriate inverse vector of arrow hit location
    public void SetKnockVector()
    {
        switch(parent.GetComponent<ArrowLauncher>().aDirection)
        {
            case(ArrowLauncher.arrowDirection.Right):
                Vector3 aRightVector = gameObject.transform.up * 160;
                knockVector = aRightVector;
                break;

            case (ArrowLauncher.arrowDirection.Left):
                Vector3 aLeftVector = gameObject.transform.up * 160;
                knockVector = aLeftVector;
                break;

            case (ArrowLauncher.arrowDirection.Up):
                Vector3 aUpVector = gameObject.transform.up * 160;
                knockVector = aUpVector;
                break;

            case (ArrowLauncher.arrowDirection.Down):
                Vector3 aDownVector = gameObject.transform.up * 160;
                knockVector = aDownVector;
                break;
        }
    }

}
