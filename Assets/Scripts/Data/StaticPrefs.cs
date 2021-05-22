using UnityEngine;

public static class StaticPrefs
{
    #region Server Data
    public static string ServerIP
    {
        get { return PlayerPrefs.GetString("ServerIP", "localhost"); }
        set { PlayerPrefs.SetString("ServerIP", value); }
    }
    public static int ServerPort
    {
        get { return PlayerPrefs.GetInt("ServerPort", 1337); }
        set { PlayerPrefs.SetInt("ServerPort", value); }
    }
    #endregion
    
    #region User Data
    public static string AccessToken
    {
        get { return PlayerPrefs.GetString("AccessToken", ""); }
        set { PlayerPrefs.SetString("AccessToken", value); }
    }
    public static string NickName
    {
        get { return PlayerPrefs.GetString("NickName", ""); }
        set { PlayerPrefs.SetString("NickName", value); }
    }
    public static string Email
    {
        get { return PlayerPrefs.GetString("Email", ""); }
        set { PlayerPrefs.SetString("Email", value); }
    }
    
    public static int UserID
    {
        get { return PlayerPrefs.GetInt("UserID", 0); }
        set { PlayerPrefs.SetInt("UserID", value); }
    }
    #endregion
    
    #region  Game Data
    public static float SoundVolume
    {
        get { return PlayerPrefs.GetFloat("SoundVolume", 1); }
        set { PlayerPrefs.SetFloat("SoundVolume", value); }
    }
    public static float MusicVolume
    {
        get { return PlayerPrefs.GetFloat("SoundVolume", 1); }
        set { PlayerPrefs.SetFloat("SoundVolume", value); }
    }
    #endregion
}
