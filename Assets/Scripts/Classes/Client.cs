using UnityEngine;
using System.Collections;
using System;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading.Tasks;

public  class Client: IDisposable
    {
        private  IMarketingGameResult.IMarketingGameResult result;

        public async Task SendToServer(string[] data)
        {
            if (data[0].ToLower() == "log") await result.Log(data[1]);
            if (data[0].ToLower() == "prize") await result.Prize(data[1]);
            if (data[0].ToLower() == "spinresult") await result.SpinResult(data[1]);
        }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        APIHandler.SaveLog("Client was Disposed");
    }

    ~Client()
    {
        Dispose();
    }

    public  Client(string serverName, string channelName, int serverPort)
        {
            BinaryClientFormatterSinkProvider binaryClientFormatterSinkProvider = new BinaryClientFormatterSinkProvider();
            IClientChannelSinkProvider sinkProvider = binaryClientFormatterSinkProvider;
            IDictionary dictionary = new Hashtable();
            dictionary["port"] = serverPort;
            dictionary["name"] = channelName;
            TcpClientChannel chnl = new TcpClientChannel(dictionary, sinkProvider);
            ChannelServices.RegisterChannel(chnl, ensureSecurity: false);
            result = (IMarketingGameResult.IMarketingGameResult)Activator.GetObject(typeof(IMarketingGameResult.IMarketingGameResult), $"{serverName}:{serverPort}/{channelName}");
            //Console.WriteLine($"MarketingGame Client has been started");
        }

    }