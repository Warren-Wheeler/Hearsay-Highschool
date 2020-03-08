using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{

    public GameObject Story;
    public GameObject[] Rules = new GameObject[4];

    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(RunPage());
    }

    public IEnumerator RunPage()
    {
        float start = Time.time;
        Story.GetComponent<CanvasGroup>().alpha = 1;

        while(true)
        {
            float percent = 2 * (Time.time - start);
            GetComponent<CanvasGroup>().alpha = percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        while(!Input.anyKeyDown) yield return null;

        start = Time.time;
        while(true)
        {
            float percent = 2 * (Time.time - start);
            Story.GetComponent<CanvasGroup>().alpha = 1-percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < 4; i++)
        {
            start = Time.time;
            while(true)
            {
                float percent = 2 * (Time.time - start);
                Rules[i].GetComponent<CanvasGroup>().alpha = percent;
                if(percent >= 1) break;
                yield return new WaitForEndOfFrame();
            }

            while(!Input.anyKeyDown) yield return null;

            start = Time.time;
            while(true)
            {
                float percent = 2 * (Time.time - start);
                Rules[i].GetComponent<CanvasGroup>().alpha = 1-percent;
                if(percent >= 1) break;
                yield return new WaitForEndOfFrame();
            }
        }
        

        start = Time.time;
        while(true)
        {
            float percent = 2 * (Time.time - start);
            GetComponent<CanvasGroup>().alpha = 1-percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }
        Rules[3].GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(false);
    }
}
