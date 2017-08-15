using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour {

    public int remainingBlocks;

    public RectTransform topBlackBar;
    public RectTransform bottomBlackBar;

    public Text blockRemainingText;

	// Use this for initialization
	void Start () 
    {
        blockRemainingText.enabled = false;
        topBlackBar.localScale = new Vector3(Screen.width, Screen.height / 2, 0);
        bottomBlackBar.localScale = new Vector3(Screen.width, Screen.height / 2, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void startDeathScreen()
    {
        StartCoroutine("MoveBlackBars");
    }

    IEnumerator MoveBlackBars()
    {
        float seconds = 2.0f;
        float time = 0;

        while(time < seconds)
        {
            topBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0,1000,0), new Vector3(0, 200, 0), (time / seconds));
            bottomBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0, -1000, 0), new Vector3(0, -200,0), (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        blockRemainingText.text = "Blocks Remaining: " + remainingBlocks;
        blockRemainingText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine("OpenBlackBars");
    }

    IEnumerator OpenBlackBars()
    {
        float seconds = 2.0f;
        float time = 0;
        blockRemainingText.enabled = false;

        while (time < seconds)
        {
            topBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0, 200, 0), new Vector3(0, 1000, 0), (time / seconds));
            bottomBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0, -200, 0), new Vector3(0, -1000, 0), (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
