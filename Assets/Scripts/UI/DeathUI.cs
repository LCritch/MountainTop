using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour {

    public int remainingBlocks;

    public RectTransform topBlackBar;
    public RectTransform bottomBlackBar;

    public Text blockRemainingText;


    Vector3 topStart = new Vector3(0,Screen.height * 2,0);
    Vector3 bottomStart = new Vector3(0, -Screen.height * 2, 0);

	// Use this for initialization
	void Start () 
    {
        blockRemainingText.enabled = false;
        topBlackBar.localScale = new Vector3(Screen.width, Screen.height/1.75f, 0);
        bottomBlackBar.localScale = new Vector3(Screen.width, Screen.height/1.75f, 0);
        topBlackBar.anchoredPosition = topStart;
        bottomBlackBar.anchoredPosition = bottomStart;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void startDeathScreen()
    {
        StartCoroutine("MoveBlackBars");
    }

    #region Close and open black bars upon death, will remain closed and show GameOver menu if blocks/Lives are at 0
    IEnumerator MoveBlackBars()
    {
        float seconds = 2.0f;
        float time = 0;

        while(time < seconds)
        {
            topBlackBar.anchoredPosition = Vector3.Lerp(topStart, new Vector3(0, (topStart.y/8), 0), (time / seconds));
            bottomBlackBar.anchoredPosition = Vector3.Lerp(bottomStart, new Vector3(0, (bottomStart.y/8),0), (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

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
            topBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0, (topStart.y/8), 0), topStart, (time / seconds));
            bottomBlackBar.anchoredPosition = Vector3.Lerp(new Vector3(0, (bottomStart.y/8), 0), bottomStart, (time / seconds));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}
