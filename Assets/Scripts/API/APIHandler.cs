using System.Net;
using System.IO;
using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
using Unity.RemoteConfig;

public static class APIHandler
{
    private static readonly HttpClient client = new HttpClient();

    public static string apiServer;

    public static int level;

    public static void FetchConfig()
    {
        apiServer = ConfigManager.appConfig.GetString("ApiServer");
        level = ConfigManager.appConfig.GetInt("Level");

        SaveLog("Config was set with ip " + apiServer);
    }

    public static Game GetGame()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp($"{apiServer}/api/game/1");
        request.Headers.Add("Authorization", "Bearer bpay-5JRlg9ViEJz8mKFT1YlnwSge");              // move auth key in config secret
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Game>(json);
    }

    [System.Serializable]
    public class SessionCloseObject
    {
        public int prize;
        public Digest digest;
    }

    [System.Serializable]
    public class SessionCloseObject2
    {
        public int prize;
        public string digest;
    }

    [System.Serializable]
    public class Digest
    {
        public int prize;
        public string token;
    }

    public async  static  Task<Session> StartSession(RequiredProperties properties)
    {
        try
        {
            var content = JsonUtility.ToJson(properties);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            if (!CommandLineArgsHandler.isFirstTry)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer bpay-5JRlg9ViEJz8mKFT1YlnwSge");   // move auth key in config secret
            }
           
            var response = await client.PostAsync($"{apiServer}/api/game/1/session", byteContent);

            //  response.Content

            // var awaiter = response.GetAwaiter();             used for tests

            // var result = response.GetResult();

            if (!response.IsSuccessStatusCode)
            {
                SaveLog(response.ReasonPhrase + response.StatusCode + response.Content + response.Headers);
                Application.Quit();
                return null;
            }

            var responseContent = response.Content.ReadAsStringAsync();
            return JsonUtility.FromJson<Session>(responseContent.Result);
        }
        catch (Exception ex)
        {
            SaveLog(ex);
            Application.Quit();
            return null;
        }
    }

    //async Task<string> GetResponseString(string text)             for tests
    //{
    //    var httpClient = new HttpClient();

    //    Object oj = new Object()
    //    {
    //        amount = "120",
    //        receipt = "R123456",
    //        terminal = "T654321",
    //        phone = "+37379528824",
    //        name = "Vasea Pupkin"
    //    };

    //    var content = JsonUtility.ToJson(oj);
    //    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
    //    var byteContent = new ByteArrayContent(buffer);

    //    var response = await httpClient.PostAsync("{apiServer}/api/game/1/session", byteContent);
    //    var contents = await response.Content.ReadAsStringAsync();

    //    return contents;
    //}


    public static string CloseSession(int prizeId)
    {
        var digest = new Digest
        {
            prize = prizeId,
            token = CommandLineArgsHandler.session.token
        };

        string jsonDigest = JsonUtility.ToJson(digest);

        //var encryptedData = Encryption(Encoding.UTF8.GetBytes(jsonDigest),  false);   used for encription , should be reworked

        SessionCloseObject2 oj2 = new SessionCloseObject2()
        {
            prize = prizeId,
            digest = jsonDigest   //Convert.ToBase64String(encryptedData) 
        }; 

        var content = JsonUtility.ToJson(oj2);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        try
        {
            var response = client.PutAsync($"{apiServer}/api/game/1/session/{CommandLineArgsHandler.session.token}", byteContent).Result;
            return response.ToString();
        }
        catch (Exception ex)
        {
            SaveLog(ex);
            Application.Quit();
            return ex.ToString();
        }
    }

    public async static Task<string> GetGameAssets() //to async
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp($"{apiServer}/api/game/1/assets/1659002100");
            request.Headers.Add("Authorization", "Bearer bpay-5JRlg9ViEJz8mKFT1YlnwSge");
            WebResponse response =  await request.GetResponseAsync();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            return DecodeCharacters(json);
        }
        catch (Exception ex)
        {
            SaveLog(ex);

            Application.Quit(ex.GetHashCode());
            return null;
        }  
    }

    public static string DecodeCharacters(string text)
    {
        Regex regex = new Regex(@"\\U([0-9A-F]{4})", RegexOptions.IgnoreCase);

        return regex.Replace(text, match => ((char)int.Parse(match.Groups[1].Value,
         NumberStyles.HexNumber)).ToString());
    }

    private static Game ConvertGame()
    {
        Game game = new Game();
        game = CommandLineArgsHandler.game;
        game.Id = Guid.NewGuid().ToString();
        game.stars = VideoHandlerScript.stars;
        return game;
    }

    public static void SaveGame()
    {
        Game parseGame = new Game();
        parseGame = ConvertGame();
        string json = JsonUtility.ToJson(parseGame);

        File.WriteAllText($"c:/Terminal/Logic/MarketingGame/{parseGame.Id}.json", json);
    }

    public static void SaveLog(string logText)
    {
       if(!Directory.Exists(Application.dataPath + "/logs"))
        {
           Directory.CreateDirectory(Application.dataPath + "/logs");
        }
        string fileName = Application.dataPath + "/logs/" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(fileName))
        {
            File.Create(fileName).Close();
        }
        
        TextWriter tw = new StreamWriter(fileName, true);

        tw.WriteLine(DateTime.Now.ToString() +": " + logText);

        tw.Close();
    }

    public static void SaveLog(Exception ex)
    {
        if (!Directory.Exists(Application.dataPath + "/logs"))
        {
            Directory.CreateDirectory(Application.dataPath + "/logs");
        }
        string fileName = Application.dataPath + "/logs/" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(fileName))
        {
            File.Create(fileName).Close();
        }

        TextWriter tw = new StreamWriter(fileName, true);
       
        string message = "Message: " + ex.Message + "\n" + "StackTrace: " + ex.StackTrace + "\n" + "Source: " + ex.Source + "\n"  +"Date: " + ex.Data + "\n" + "InnerException: " + ex.InnerException + "\n";

        tw.WriteLine(DateTime.Now.ToString() + ": " + message);

        tw.Close();
    }

}
