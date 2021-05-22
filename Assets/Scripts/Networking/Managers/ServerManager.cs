using System;
using System.Text;
using System.Text.RegularExpressions;
using Telepathy;
using Berserk.Messaging;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;
using UnityEngine;
using Berserk.Messaging.Messages;
using Berserk.Networking.Messages;
using Object = UnityEngine.Object;

namespace Berserk.Networking.Managers
{
    public class ServerManager : MonoBehaviour
    {
        public static ServerManager ServerManagerInstance = null; 
        
        
        //обявение телепатиивеского клиента 
        Client client = new Client(16000);
        
        //Событие прибытия обекта с сервера (см папку Scripts.Networking.Messages)
        public Action <JToken, Type> ServerResponseObjectEvent = delegate(JToken o, Type type) {  };
        public Action FailedToConnect = delegate {  };
        public Action Connected = delegate {  };
        
        private void Awake()
        {
            //ОГО, синглтон
            if (ServerManagerInstance == null) ServerManagerInstance = this;
            else if (ServerManagerInstance == this) Destroy(gameObject);

            //Не уничтожать ServerManager ни в коем случае, инче телепатеивский Client удалится и придётся реконектиться
            DontDestroyOnLoad(this.gameObject);


            Telepathy.Log.Info = Debug.Log;
            Telepathy.Log.Warning = Debug.LogWarning;
            Telepathy.Log.Error = Debug.LogError;
            
            Log.Info = LogMessage =>
            {
                if (Regex.Match(LogMessage, "Client Recv: failed to connect to").Success)
                    
                        FailedToConnect();
                    
            };
            
            FailedToConnect += () => Debug.Log("бРАТАН, вруби сервак");
            
            client.OnConnected = ConectedToServer;
            client.OnData = ServerBinaryResponse;  
            client.OnDisconnected = DisconectedFromServer;
            
            
            ConnectToServer(StaticPrefs.ServerIP, StaticPrefs.ServerPort);
        }

        public void ConnectToServer(string IP, int Port)
        {
            client.Connect(IP,Port);
        }

        /// <summary>
        /// Метод принятия бинарника с сервера и кновертации его в json, затем в NetworkDictionary,
        /// затем в объект в зависмости от MessageType словаря NetworkDictionary
        /// </summary>
        /// <param name="message"></param>
        private void ServerBinaryResponse(ArraySegment<byte> byteArray)
        {
            Debug.Log($"ServerBinaryResponse :\n" + Encoding.UTF8.GetString(byteArray.Array));

            JsonMessage clientMessage = JsonMessage.Deserialize(Encoding.UTF8.GetString(byteArray.Array));
            //Debug.Log(Type.GetType(clientMessage.type));
            ServerResponseObjectEvent(clientMessage.Value, Type.GetType(clientMessage.Type));
        }
        
        /// <summary>
        /// Метод отправки обекта к серверу создавая структу NetworkDictionary сообщения и в дальнейшем конвертации её в JSON а затем в бинарник для отправки
        /// </summary>
        /// <param name="objMessage">Объект с пространства имён Scripts.Messaging.Messages</param>
        public void Send_Object_ToServer<Template>(Template objMessage) //=>

        {
            Debug.Log("Send_Object_ToServer\n"+JsonMessage.Serialize(
                JsonMessage.FromValue<Template>(objMessage)));
            
            client.Send(
                new ArraySegment<byte>(
                    Encoding.UTF8.GetBytes(
                        JsonMessage.Serialize(
                            JsonMessage.FromValue(objMessage)))));
        }


        /// <summary>
        /// Метод колбека Client.OnConnected телепатии
        /// </summary>
        void ConectedToServer()
        {
            Connected();
            Connected = () => Debug.Log($"Connected to Server at {StaticPrefs.ServerIP}:{StaticPrefs.ServerPort}");
            
            //Send_Object_ToServer(new UnityConnection(Application.version,SystemInfo.deviceName));
        }

        /// <summary>
        /// Метод колбека Client.OnDisconnected телепатии
        /// </summary>
        public void DisconectedFromServer()
        {
            Debug.Log("Client Disconected");
            //Action serverShutDown;
        }
        
        private void FixedUpdate()
        {
            client.Tick(100);
        }

        void OnApplicationQuit()
        {
            client.Disconnect();
        }
    }
}
