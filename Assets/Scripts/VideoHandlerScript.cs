using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Linq;

public class VideoHandlerScript : MonoBehaviour
{
    public VideoClip[] Pyramids = new VideoClip[6];
    public VideoClip[] Island = new VideoClip[6];
    public VideoClip[] Kyvshini = new VideoClip[6];

    public static string stars;
    public static int prizeId;
    public static string prizeText;
    public static string prizeTextRo;
    public VideoPlayer IntroPlayer;
    public GameObject IntroImage;

    public VideoPlayer BackGroundIntroPlayer;
    public GameObject BackGroundIntroPlayerImage;

    public VideoPlayer BackGroundPlayer;
    public GameObject BackGroundImage;

    public GameObject Prize;

    public GameObject GameScene;

    public VideoPlayer ExitPlayer;
    public GameObject ExitImage;

    private int countDownTime = 40 ;

    private void Start()
    {
        if (APIHandler.level == 0)
        {
            IntroPlayer.clip = Pyramids[0];
            BackGroundIntroPlayer.clip = Pyramids[1];
            BackGroundPlayer.clip = Pyramids[2];
        }
        if (APIHandler.level == 1)
        {
            IntroPlayer.clip = Island[0];
            BackGroundIntroPlayer.clip = Island[1];
            BackGroundPlayer.clip = Island[2];
        }
        if (APIHandler.level == 2)
        {
            IntroPlayer.clip = Kyvshini[0];
            BackGroundIntroPlayer.clip = Kyvshini[1];
            BackGroundPlayer.clip = Kyvshini[2];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IntroPlayer.isPlaying)
        {
            IntroPlayer.Stop();
            IntroImage.SetActive(false);
            //  BackGroundIntroPlayer.Play();
            //  BackGroundIntroPlayerImage.SetActive(true);
            BackGroundImage.SetActive(true);
            GameScene.SetActive(true);
        }

        if (IntroPlayer.isPaused && IntroImage.activeSelf)
        {
            IntroImage.SetActive(false);
            // BackGroundIntroPlayer.Play();
            //BackGroundIntroPlayerImage.SetActive(true);
            BackGroundImage.SetActive(true);
            GameScene.SetActive(true);
        }

        if (BackGroundIntroPlayer.isPaused && BackGroundIntroPlayerImage.activeSelf)
        {
            BackGroundIntroPlayerImage.SetActive(false);
            BackGroundPlayer.Play();
            BackGroundImage.SetActive(true);
            GameScene.SetActive(true);
        }

        if(superwheel.gamesituation == 1 && SecondWheel.gamesituation == 1 && ThirdWheel.gamesituation == 1)
        {
            this.Prize.SetActive(true);
            StartCoroutine(CountdownToStart());
        }
    }

    public int ConvertStarsToPrizeId(string stars)
    {
        if (stars == "333") 
        {
            var id =  CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "3x3").Select(x => x.id).FirstOrDefault();
            prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id ==  id).Select(x => x.labels.ru).FirstOrDefault();
            prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id ==  id).Select(x => x.labels.ro).FirstOrDefault();
            return id;
        }
        if (stars == "555")
        {
            var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "3x5").Select(x => x.id).FirstOrDefault();
            prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
            prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
            return id;
        }
        if (stars == "444")
        {
            var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "3x4").Select(x => x.id).FirstOrDefault();
            prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
            prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
            return id;
        }

        if ((stars.Contains("4") && !stars.Contains('5') || stars.ToCharArray().Count(c => c == '4') == 2) && stars != "444")
        {
            int result = stars.ToCharArray().Count(c => c == '4');
            if (result == 1)
            {
                var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "1x4").Select(x => x.id).FirstOrDefault();
                prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
                prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
                return id;
            }
            else
            {
                var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "2x4").Select(x => x.id).FirstOrDefault();
                prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
                prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
                return id;
            }
        }

        if (stars.Contains("5"))
        {
            int result = stars.ToCharArray().Count(c => c == '5');
            if (result == 1)
            {
                var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "1x5").Select(x => x.id).FirstOrDefault();
                prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
                prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
                return id;
            }
            else
            {
                var id = CommandLineArgsHandler.prizes.prizes.Where(x => x.code == "2x5").Select(x => x.id).FirstOrDefault();
                prizeText = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ru).FirstOrDefault();
                prizeTextRo = CommandLineArgsHandler.prizes.prizes.Where(x => x.id == id).Select(x => x.labels.ro).FirstOrDefault();
                return id;
            }
        }

        return 0;
    }

    IEnumerator CountdownToStart()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        int loseIndex;
        if (countDownTime == 0)
        {
            stars = superwheel.starCounter.ToString() + SecondWheel.starCounter.ToString() + ThirdWheel.starCounter.ToString();
            prizeId = ConvertStarsToPrizeId(stars);
            if (stars == "333")
            {
                loseIndex = 3;
            }
            else if (stars == "444" || stars == "555")
            {
                loseIndex = 5;
            }
            else
            {
                loseIndex = 4;
            }

            if (APIHandler.level == 0)
                ExitPlayer.clip = Pyramids[loseIndex];

            if (APIHandler.level == 1)
                ExitPlayer.clip = Island[loseIndex];

            if (APIHandler.level == 2)
                ExitPlayer.clip = Kyvshini[loseIndex];

            GameScene.SetActive(false);
            BackGroundImage.SetActive(false);
            ExitImage.SetActive(true);
            ExitPlayer.Play();
        }
    }
}
