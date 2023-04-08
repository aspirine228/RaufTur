using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdWheel : MonoBehaviour
{
    public Sprite[] stars = new Sprite[3];
    public GameObject star;
    float rnd;
    float rndcounter;
    private float secondWheelTimerStart = 2;
    public GameObject wheel;
    public GameObject stopButton;
    public float counter;
    private float speed = 0;
    public static int gamesituation = 2;
    public static int starCounter = 0;
    public float Koleso;

    private float finalPosition;

    private void Start()
    {
        var rnnd = Random.Range(1, 19);
        finalPosition = 0 + 20f * rnnd - 10;
    }

    public void StartWheels()
    {
        rnd = Random.Range(4, 7);
        StartCoroutine(CountdownToStart());
    }

    public void StopWheels()
    {
        StartCoroutine(WheelStarter());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed);
        if (speed == 0 && gamesituation == 1)
        {
            Koleso = wheel.transform.rotation.eulerAngles.z;
            counter = wheel.transform.rotation.eulerAngles.z;

            if (counter >= 0 && counter < 20)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 20 && counter < 40)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 40 && counter < 60)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 60 && counter < 80)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 80 && counter < 100)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 100 && counter < 120)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 120 && counter < 140)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 140 && counter < 160)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 160 && counter < 180)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter =4;
            }
            if (counter >= 180 && counter < 200)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 200 && counter < 220)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 220 && counter < 240)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 240 && counter < 260)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 260 && counter < 280)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 280 && counter < 300)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 300 && counter < 320)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 320 && counter < 340)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 340 && counter < 360)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            star.SetActive(true);
        }
    }

    IEnumerator CountdownToStart()
    {
        while (secondWheelTimerStart > 0)
        {
            yield return new WaitForSeconds(1f);
            secondWheelTimerStart--;
        }
        if (secondWheelTimerStart == 0)
        {
            stopButton.SetActive(true);
                        speed = 6;
        }
    }
    IEnumerator WheelStarter()
    {
      
        while (rnd > 0)
        {
            yield return new WaitForSeconds(1f);
            rnd--;
        }
        if (rnd == 0)
        {
            speed = 0;
            wheel.transform.eulerAngles = new Vector3(0, 0, finalPosition);

            gamesituation = 1;
        }
    }
    IEnumerator WheelFinisher ()
    {
        rndcounter = Random.Range(0.05f, 0.23f);
        while (speed > 0.1)
        {
            yield return new WaitForSeconds(0.1f);
            speed -= rndcounter;
        }
        speed = 0;
        gamesituation = 1;
    }
}
