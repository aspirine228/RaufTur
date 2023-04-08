using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class QuitGame : MonoBehaviour
{
    public VideoPlayer player;

    private bool gameIsSaved = false;
    string closed;
    public GameObject prize;
    public TextMeshProUGUI prizeText;
    public TextMeshProUGUI phoneText;
    public TextMeshProUGUI time;
    public TextMeshProUGUI check;
    public Texture imageRu;
    public Texture imageRo;
    public GameObject image;

    private void Start()
    {
        time.text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        check.text = "NR. " + SplitStr(CommandLineArgsHandler.CheckId, 5);
        phoneText.text = $"{CommandLineArgsHandler.Phone}";
        prizeText.text =  CommandLineArgsHandler.Lang == "Rus" ? VideoHandlerScript.prizeText : VideoHandlerScript.prizeTextRo;
        image.GetComponent<RawImage>().texture = CommandLineArgsHandler.Lang == "Rus" ? imageRu : imageRo;
    }

    void Update()
    {
        if(gameObject.activeSelf == true && player.isPaused && !gameIsSaved)
        {
          
            closed =  APIHandler.CloseSession(VideoHandlerScript.prizeId);

            APIHandler.SaveLog("session closed successfully with response:" + closed);

            if (closed.Length > 0)
            {
               
                gameIsSaved = true;
                CommandLineArgsHandler.isFirstTry = true;
            }

            if (gameIsSaved == true && VideoHandlerScript.stars == "333")
            {
                this.Log();
                DestroyAllStaticMembers();
                SceneManager.LoadScene(0);
            }
          
            if(gameIsSaved == true && VideoHandlerScript.stars != "333") { prize.SetActive(true); }
        }
    }

    public string SplitStr(string str, int maxSymbols)
    {
        var sb = new StringBuilder();
        var counter = 0;
        foreach (var element in str)
        {
            if (counter == maxSymbols)
            {
                sb.Append("-");
                counter = 0;
            }

            sb.Append(element);
            counter++;
        }
        return sb.ToString();
    }

    void Log()
    {
        var text = CommandLineArgsHandler.ParseClient("Prize=" + VideoHandlerScript.prizeTextRo);

        var text2 = CommandLineArgsHandler.ParseClient("spinresult=" + VideoHandlerScript.stars);

        var text3 = CommandLineArgsHandler.ParseClient("Log=" + closed.Substring(0, 15));

        try
        {
             CommandLineArgsHandler.client.SendToServer(text);
             CommandLineArgsHandler.client.SendToServer(text2);
             CommandLineArgsHandler.client.SendToServer(text3);

        }
        catch (Exception ex)
        {
            APIHandler.SaveLog(ex);
        }
        CommandLineArgsHandler.client.Dispose();
    }

    public void QuitGameMehtod()
    {
        if (gameIsSaved && VideoHandlerScript.stars != "333")
        {
            this.Log();
           
        }
        Application.Quit();
    }

    private void DestroyAllStaticMembers()
    {
        CommandLineArgsHandler.session = null;
        CommandLineArgsHandler.prizes = null;
        CommandLineArgsHandler.game = null;
        VideoHandlerScript.prizeId = 0;
        VideoHandlerScript.stars = null;
        VideoHandlerScript.prizeText = null;
        ThirdWheel.gamesituation = 0;
        ThirdWheel.starCounter = 0;
        SecondWheel.gamesituation = 0;
        SecondWheel.starCounter = 0;
        superwheel.starCounter = 0;
        superwheel.speed = 0;
        superwheel.gamesituation = 0;
    }
}
