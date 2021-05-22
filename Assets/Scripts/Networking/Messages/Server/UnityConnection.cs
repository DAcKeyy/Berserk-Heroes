namespace Berserk.Messaging.Messages
{
    public class UnityConnection
    {
        public UnityConnection(string appVer, string deviceName)
        {
            AppVersion = appVer;
            DeviceName = deviceName;
        }

        public UnityConnection()
        {
            AppVersion = "";
            DeviceName = "";
        }
        
        public string AppVersion;
        public string DeviceName;
    }
}
