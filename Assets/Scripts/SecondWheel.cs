using System.Collections;
using UnityEngine;

public class SecondWheel : MonoBehaviour
{
    public Sprite[] stars = new Sprite[3];
    public GameObject star;
    float rnd;
    float rndcounter;
    private float secondWheelTimerStart = 1;
    public GameObject wheel;
    public float counter;
    private float speed = 0; public static int gamesituation = 2; public float Koleso;

    private float finalPosition;

    public static int starCounter = 0;
    // Start is called before the first frame update

    private void Start()
    {
        var rnnd = Random.Range(1, 10);
        finalPosition = 0 + 40f * rnnd - 20;
    }

    public void StartWheels()
    {
        rnd = Random.Range(4, 6);
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

            if (counter >= 0 && counter < 40)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 40 && counter < 80)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 80 && counter < 120)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 120 && counter < 160)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 160 && counter < 200)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 200 && counter < 240)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[1];
                starCounter = 4;
            }
            if (counter >= 240 && counter < 280)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter = 3;
            }
            if (counter >= 280 && counter < 320)
            {
                star.GetComponent<SpriteRenderer>().sprite = stars[0];
                starCounter =3;
            }
            if (counter >= 320 && counter < 360)
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
            speed = -6;
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

   // old implementation

    IEnumerator WheelFinisher()
    {
        rndcounter = Random.Range(0.05f, 0.23f);
        while (speed < -0.1)
        {
            yield return new WaitForSeconds(0.1f);
            speed += rndcounter;
        }
        speed = 0;
        gamesituation = 1;
    }
}
