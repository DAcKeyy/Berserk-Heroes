using System;
using Berserk.Messaging.Messages;
using UnityEngine;

namespace Berserk.Networking.Managers
{
    public class NotificationManager : MonoBehaviour
    {
        private ServerManager _serverManager;

        public static NotificationManager NotificationManagerInstance;
        
        public Action<Authorization> AuthorizationServerResponse;
        public Action<Registration> RegistrationServerResponse;
        public Action<UnityConnection> UnityConnectionServerResponse;
        
        public Action<string,Authorization> ServerNonValidAuthorizationMessage;
        public Action<string,Registration> ServerNonValidRegistrationMessage;
        public Action<string,UnityConnection> ServerNonValidUnityConnectionMessage;
        void Awake()
        {
            //Сингтон
            if (NotificationManagerInstance == null) NotificationManagerInstance = this;   
            else if (NotificationManagerInstance == this) Destroy(gameObject);
            
            //Не уничтожать ServerManager ни в коем случае, инче телепатеивский Client удалится и придётся реконектиться
            DontDestroyOnLoad(this.gameObject);
            
            _serverManager = UnityEngine.Object.FindObjectOfType<ServerManager>();
            _serverManager.ServerResponseObjectEvent += (obj, type) =>
            {
                if (type == typeof(Notification)) ListenTypeofNotification(obj.ToObject<Notification>());
            };
            DontDestroyOnLoad(this.gameObject);
        }

        private void ListenTypeofNotification(Notification notificationMessage)
        {
            if (notificationMessage.isValid)
            {
                if (Type.GetType(notificationMessage.Message.Type) ==  typeof(Authorization))
                    AuthorizationServerResponse(notificationMessage.Message.Value.ToObject<Authorization>());
                if (Type.GetType(notificationMessage.Message.Type) ==  typeof(Registration))
                    RegistrationServerResponse(notificationMessage.Message.Value.ToObject<Registration>());
                if (Type.GetType(notificationMessage.Message.Type) ==  typeof(UnityConnection))
                    UnityConnectionServerResponse(notificationMessage.Message.Value.ToObject<UnityConnection>());
            }
            else
            {
                ServerNonValidAuthorizationMessage(notificationMessage.Description,notificationMessage.Message.Value.ToObject<Authorization>());
                ServerNonValidRegistrationMessage(notificationMessage.Description, notificationMessage.Message.Value.ToObject<Registration>());
                ServerNonValidUnityConnectionMessage(notificationMessage.Description,notificationMessage.Message.Value.ToObject<UnityConnection>());
            }
        }
    }
}