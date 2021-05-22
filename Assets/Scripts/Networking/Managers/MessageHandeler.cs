using System.Threading.Tasks;
using System;
using Berserk.Messaging.Messages;
using Berserk.Networking.Managers;
using Berserk.Networking.Messages;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MessageHandeler : MonoBehaviour
{
    public static MessageHandeler MessageHandelerInstance = null;
    
    void Awake()
    {
        if (MessageHandelerInstance == null) MessageHandelerInstance = this;   
        else if (MessageHandelerInstance == this) Destroy(gameObject);
            
        //Не уничтожать ServerManager ни в коем случае, инче телепатеивский Client удалится и придётся реконектиться
        DontDestroyOnLoad(this.gameObject);
    }

    public async Task<Message> SendCommandMessage(Message message)
    {
        TaskCompletionSource<Message> tcs = new TaskCompletionSource<Message>();
        
        if (!message.Text.Contains("Command "))
        {
            Debug.Log($"No \"Command\" in {message.Text} text message");
            tcs.SetCanceled();
            return await tcs.Task;
        }
        ServerManager.ServerManagerInstance.Send_Object_ToServer(message);

        string command = message.Text.Replace("Command ", "");
        Action<JToken, Type> SDKLNFDSHJOFSDNJLSDJLNF = delegate(JToken obj, Type type)
        {
            if (type == typeof(Message))
            {
                Message resivedMessage = obj.ToObject<Message>();
                Debug.Log(resivedMessage.Text);
                
                if (resivedMessage.Text.Contains(command))
                    tcs.SetResult(resivedMessage);
                
                
                //надо ещё сюда таймаут ебануть
                
            }
        };
        ServerManager.ServerManagerInstance.ServerResponseObjectEvent += SDKLNFDSHJOFSDNJLSDJLNF;
        try
        {
            return await tcs.Task;
        }
        finally
        {
            ServerManager.ServerManagerInstance.ServerResponseObjectEvent -= SDKLNFDSHJOFSDNJLSDJLNF;
        }
    }
}

    