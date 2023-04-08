using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.RemoteConfig;
using System.Collections;
using System.Threading.Tasks;

public class CommandLineArgsHandler : MonoBehaviour, IMarketingGameResult.IMarketingGameResult
{
    // Start is called before the first frame update
    public static string CheckId;
    private static string Sum;
    public static string Lang;
    public static string Phone;

    public GameObject Videos;
    public GameObject Loader;

    public static bool isFirstTry;

    public TextMeshProUGUI[] rules = new TextMeshProUGUI[8];
    public TextMeshProUGUI prizeTitle;
    public TextMeshProUGUI rulesTitle;
    public TextMeshProUGUI[] rules2 = new TextMeshProUGUI[13];

    public Text text;

    public static Game game = new Game();
    public static Session session;
    public static Prizes prizes;
    public static Client client;

    public struct userAttributes
    {

    }

    public struct appAttributes
    {

    }

    void Start()
    {
        if (!isFirstTry)
        {
            // Add a listener to apply settings when successfully retrieved:
            ConfigManager.FetchCompleted += ApplyRemoteSettings;

            try
            {
                ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());

            }
            catch (Exception ex)
            {
                APIHandler.SaveLog(ex);
            }
            // Fetch configuration setting from the remote service:
        }
        else
        {
            StartCoroutine(LoadAsync());
        }
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        // Conditionally update settings, depending on the response's origin:
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:

                APIHandler.FetchConfig();
                break;
            case ConfigOrigin.Cached:
                APIHandler.SaveLog("No settings loaded this session; using cached values from a previous session.");
                APIHandler.FetchConfig();
                break;
            case ConfigOrigin.Remote:
                APIHandler.SaveLog("New settings loaded this session; update values accordingly.");

                APIHandler.FetchConfig();

                break;
        }
        StartCoroutine(LoadAsync());
    }

    //private void Start()
    //{
    //    this.Load();
    //}

    IEnumerator LoadAsync()
    {
        while (ConfigManager.requestStatus != ConfigRequestStatus.Success && APIHandler.apiServer.Length == 0)
        {
             yield return null;
        }

        if(ConfigManager.requestStatus == ConfigRequestStatus.Success && APIHandler.apiServer.Length > 0)
        {
            this.Load(); 
        }

        if (ConfigManager.requestStatus == ConfigRequestStatus.Failed)
        { 
            APIHandler.SaveLog("Failed loading config");
            Application.Quit();
        }
    }

    /// <summary>
    /// Get data from parameters when you open the app with console call
    /// </summary>
    private void GetArg()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            switch(args[i])
            {
                case "-check":
                    CheckId = args[i + 1];
                    continue;
                case "-sum":
                    Sum = args[i + 1];
                    continue;
                case "-lang":
                    Lang = args[i + 1];
                    continue;
                case "-phone":
                    Phone = args[i + 1];
                    break;
            }
        }
        CheckArg();
    }

    private void CheckArg()
    {
        //CheckId = "12345123451234512345";
        //Sum = 500.ToString();
        //Phone = "079975524";
        //Lang = "Mold";

        //uncomment this for manual tests

        if (CheckId.Length != 20 || (Lang != "Rus" && Lang != "Mold") || !Phone.StartsWith("0") || Phone.Length != 9)
        {
            Application.Quit();
        }
    }

    private async void Load()
    {
        try
        {
            client = await CreateClient();
            APIHandler.SaveLog("Client Created successfully ");
        }
        catch (Exception ex)
        {
            APIHandler.SaveLog(ex);
        }

        if (!isFirstTry)
        {
            GetArg();
        }

        RequiredProperties properties = new RequiredProperties()
        {
            receipt = CheckId,
            amount = Convert.ToInt32(Sum),
            phone = Phone,
            terminal = "test terminal",
            name = "test name"
        };

        session = await APIHandler.StartSession(properties);

        if (session == null)
        {
            Application.Quit();
        }

        try
        {
            string[] result = ParseClient("Log=Session Started with params:" + '\n' + JsonUtility.ToJson(properties));
            await client.SendToServer(result);
        }
        catch (Exception ex)
        {
            APIHandler.SaveLog(ex);
        }

        string assests = await APIHandler.GetGameAssets();


        string assetsv2 = assests.Trim('\"');
        int index = assests.IndexOf("rules_line_10");
        // assests.r  Replace("\\\"", "");

        prizes = JsonUtility.FromJson<Prizes>(assetsv2);
        int i = 0;

        if (Lang == "Rus")
        {
            prizeTitle.text = prizes.texts.prizes_list_title.ru;
            prizes.prizes.ForEach(x => {
                rules.Where(y => y.name == x.code).FirstOrDefault().text = x.labels.ru; i++;
            });

            rulesTitle.text = prizes.texts.rules_title.ru;
            rules2[0].text = prizes.texts.rules_line_1.ru;
            rules2[1].text = prizes.texts.rules_line_2.ru;
            rules2[2].text = prizes.texts.rules_line_3.ru;
            rules2[3].text = prizes.texts.rules_line_4.ru;
            rules2[4].text = prizes.texts.rules_line_5.ru;
            rules2[5].text = prizes.texts.rules_line_6.ru;
            rules2[6].text = prizes.texts.rules_line_7.ru;
            rules2[7].text = prizes.texts.rules_line_8.ru;
            rules2[8].text = prizes.texts.rules_line_9.ru;
            rules2[9].text = prizes.texts.rules_line_10.ru;
            rules2[10].text = prizes.texts.rules_line_11.ru;
            rules2[11].text = prizes.texts.rules_line_12.ru;
            rules2[12].text = prizes.texts.rules_line_13.ru;

        }
        else
        {
            prizeTitle.text = prizes.texts.prizes_list_title.ro;
            prizes.prizes.ForEach(x => {
                rules.Where(y => y.name == x.code).FirstOrDefault().text = x.labels.ro; i++;
            });
            rulesTitle.text = prizes.texts.rules_title.ro;
            rules2[0].text = prizes.texts.rules_line_1.ro;
            rules2[1].text = prizes.texts.rules_line_2.ro;
            rules2[2].text = prizes.texts.rules_line_3.ro;
            rules2[3].text = prizes.texts.rules_line_4.ro;
            rules2[4].text = prizes.texts.rules_line_5.ro;
            rules2[5].text = prizes.texts.rules_line_6.ro;
            rules2[6].text = prizes.texts.rules_line_7.ro;
            rules2[7].text = prizes.texts.rules_line_8.ro;
            rules2[8].text = prizes.texts.rules_line_9.ro;
            rules2[9].text = prizes.texts.rules_line_10.ro;
            rules2[10].text = prizes.texts.rules_line_11.ro;
            rules2[11].text = prizes.texts.rules_line_12.ro;
            rules2[12].text = prizes.texts.rules_line_13.ro;
        }

        Loader.SetActive(false);
        Videos.SetActive(true);
    }


    public static string[] ParseClient(string info)
    {
        return info.Split('=');
    }

    public async Task<Client> CreateClient()
    {
      return  new Client("tcp://localhost", "marketinggameresult", 1978);
    }

    public Task Log(string text)
    {
        throw new NotImplementedException();
    }

    Task IMarketingGameResult.IMarketingGameResult.Prize(string text)
    {
        throw new NotImplementedException();
    }

    public Task SpinResult(string result)
    {
        throw new NotImplementedException();
    }

    [System.Serializable]
    public class Labels
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class Prize
    {
        public int id;
        public string code;
        public object imageUrl;
        public Labels labels;
    }

    [System.Serializable]
    public class Prizes
    {
        public Texts texts;
        public List<Prize> prizes;
    }
    [System.Serializable]
    public class PrizesListTitle
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine1
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine10
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine11
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine12
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine13
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine2
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine3
    {
        public string ru;
        public string en;
        public string ro;
    }
    [System.Serializable]
    public class RulesLine4
    {
        public string en;
        public string ro;
        public string ru;
    }
    [System.Serializable]
    public class RulesLine5
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine6
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine7
    {
        public string ro;
        public string en;
        public string ru;
    }
    [System.Serializable]
    public class RulesLine8
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesLine9
    {
        public string ru;
        public string ro;
        public string en;
    }
    [System.Serializable]
    public class RulesTitle
    {
        public string en;
        public string ru;
        public string ro;
    }
    [System.Serializable]
    public class Texts
    {
        public PrizesListTitle prizes_list_title;
        public RulesTitle rules_title;
        public RulesLine1 rules_line_1;
        public RulesLine2 rules_line_2;
        public RulesLine3 rules_line_3;
        public RulesLine8 rules_line_8;
        public RulesLine9 rules_line_9;
        public RulesLine10 rules_line_10;
        public RulesLine11 rules_line_11;
        public RulesLine12 rules_line_12;
        public RulesLine13 rules_line_13;
        public RulesLine4 rules_line_4;
        public RulesLine6 rules_line_6;
        public RulesLine5 rules_line_5;
        public RulesLine7 rules_line_7;
    }
}
