using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManger : MonoBehaviour
{
    public Canvas titleCanvas;
    public Canvas hudCanvas;
    public Canvas tutorialCanvas1;
    public Canvas tutorialCanvas2;
    public Canvas tutorialCanvas3;

    private bool atTitle = true;
    private int spaceCount = 0;
    private bool atBoat = false;
    // Start is called before the first frame update
    void Start()
    {
        titleCanvas.enabled = true;
        hudCanvas.enabled = false;
        tutorialCanvas1.enabled = false;
        tutorialCanvas2.enabled = false;
        tutorialCanvas3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atTitle && Input.GetKeyDown(KeyCode.Return))
        {
            atTitle = false;
            StartCoroutine(titleStart());
        }
        if (atBoat && Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount += 1;
            if (spaceCount > 0)
            {
                tutorialCanvas1.enabled = false;
                tutorialCanvas2.enabled = true;
            }
            if (spaceCount > 3)
            {
                tutorialCanvas2.enabled = false;
            }
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (atTitle)
                {
                    atTitle = false;
                    StartCoroutine(titleStart());
                    GameObject.FindObjectsOfType<FlyingDrummer>().ToList().ForEach(a => a.fly());
                } 
                else
                {
                    GameObject.FindObjectOfType<DrumHitter>().HitDrum();
                }
            }
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (atTitle)
            {
                atTitle = false;
                StartCoroutine(titleStart());
                GameObject.FindObjectsOfType<FlyingDrummer>().ToList().ForEach(a => a.fly());
            }
            else
            {
                GameObject.FindObjectOfType<DrumHitter>().HitDrum();
            }
        }
        */
    }

    private IEnumerator titleStart()
    {
        titleCanvas.enabled = false;
        yield return new WaitForSeconds(3f);
        hudCanvas.enabled = true;
        tutorialCanvas1.enabled = true;
        atBoat = true;
    }

    public void atEndOfGame()
    {
        tutorialCanvas3.enabled = true;
    }
}
