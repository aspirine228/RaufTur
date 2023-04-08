using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int countdownTime = 5;
    public Text countdownDisplay;
    public Button knopic;
    IEnumerator CountdownToStart()
    {
        while (countdownTime >= 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            countdownTime--;
            if(countdownTime == 0)
            {
                knopic.onClick.Invoke();
                knopic.gameObject.SetActive(false);
            }
        }

    }

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }
}
