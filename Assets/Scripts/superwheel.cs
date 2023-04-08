using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class superwheel : MonoBehaviour
{
    public Sprite [] stars = new Sprite [3];
    public GameObject star;
    public int countdownTime;
    public static float speed;
    float rnd;
    float rndcounter;
    public GameObject wheel;
    public float counter;
    public Text winttext;
    public static int gamesituation = 2; public float Koleso;

    private float finalPosition;

    public static int starCounter = 0;
    public void StartWheels()
    {
        gamesituation = 1;
        speed = 4;
    }

    private void Start()
    {
        var rnnd = Random.Range(1, 25);
        finalPosition = 0 + 15f * rnnd - 7;
    }

    public void StopWheels()
    {
        rnd = Random.Range(2, 4);
        StartCoroutine(CountdownToStart());
    }

    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(0, 0, speed);
        if (speed == 0 && gamesituation==1)
        {
            Koleso = wheel.transform.rotation.eulerAngles.z ;
            counter = wheel.transform.rotation.eulerAngles.z;
           
            if (counter>=0 && counter < 15)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 15 && counter < 30)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 30 && counter < 45)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 45 && counter < 60)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 60 && counter < 75)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 75 && counter < 90)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 90 && counter < 105)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 105 && counter < 120)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 120 && counter < 135)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 135 && counter < 150)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 150 && counter < 165)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 165 && counter < 180)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 180 && counter < 195)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter =3;
            }
            if (counter >= 195 && counter < 210)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 210 && counter < 225)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 225 && counter < 240)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 240 && counter < 255)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 255 && counter < 270)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >=270 && counter < 285)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 285 && counter < 300)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 300 && counter < 315)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 315 && counter < 330)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            if (counter >= 330 && counter < 345)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 345 && counter < 360)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[2];
                starCounter = 5;
            }
            star.SetActive(true);
        }
        
    }
    IEnumerator CountdownToStart()
    {
        while (rnd > 0)
        {
            yield return new WaitForSeconds(1f);
            rnd--;
            countdownTime--;
        }
        if (rnd == 0)
        {
            speed = 0;
            wheel.transform.eulerAngles = new Vector3(0,0,finalPosition);
        }
    }

    //IEnumerator CountdownToStart()
    //{
    //    while (wheel.transform.rotation.eulerAngles.z != finalPosition)
    //    {
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    if (wheel.transform.rotation.eulerAngles.z == finalPosition)
    //    {
    //        speed = 0;
    //    }

    //}

    IEnumerator CountDowntostart()
    {
        rndcounter = Random.Range(0.05f,0.23f);
      
        while (speed > 0.1)
        {
            yield return new WaitForSeconds(0.1f);
            speed-=rndcounter;
        }
        
    }
}
