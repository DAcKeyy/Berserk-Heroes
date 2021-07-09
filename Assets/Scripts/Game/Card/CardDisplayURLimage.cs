using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class CardDisplayURLimage : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private DownloadingType РежимСкачивания;
    public void SetURL_Image(string URL)
    {
        if (string.IsNullOrEmpty(URL))
        {
            image.sprite = Sprite.Create(null,new Rect(0f, 0f, 0f, 0f),new Vector2(0.5f,0.5f));
            return;
        }
        StartCoroutine(setImage(URL, sprite => 
        {
            image.sprite = sprite;
        }));
    }

    IEnumerator setImage(string url, Action<Sprite> response)
    {
        string imageName = url.Substring(url.LastIndexOf('/'));

        if (System.IO.File.Exists(Application.persistentDataPath + $"/Cards Saved URL Images/{imageName}"))
        {
            byte[] data = File.ReadAllBytes(Application.persistentDataPath + $"/Cards Saved URL Images/{imageName}");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            var rect = new Rect(11f, 135f, 261, 220);
            var sprite = Sprite.Create(tex,rect,new Vector2(0.5f,0.5f));
            response(sprite);
            yield break;
        }
        
        
        using(var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    if (РежимСкачивания == DownloadingType.БезОбразки)
                    {
                        var texture = DownloadHandlerTexture.GetContent(www);
                        var rect = new Rect(0f, 0f, texture.width, texture.height);
                        var sprite = Sprite.Create(texture,rect,new Vector2(0.5f,0.5f));
                        SaveCardTexture(sprite.texture, imageName);
                        response(sprite);
                    }

                    if (РежимСкачивания == DownloadingType.С_Сайта_БерсеркГерои)
                    {
                        var texture = DownloadHandlerTexture.GetContent(www);
                        var rect = new Rect(11f, 135f, 261, 220);
                        var sprite = Sprite.Create(texture,rect,new Vector2(0.5f,0.5f));
                        SaveCardTexture(sprite.texture, imageName);
                        response(sprite);
                    }
                }
            }
        }
    }

    private void SaveCardTexture(Texture2D texture, string name)
    {
        byte[] bytes = texture.EncodeToJPG();
        var dirPath = Application.persistentDataPath + "/Cards Saved URL Images";
        
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + $"/{name}", bytes);
        Debug.Log(bytes.Length / 1024 + $"Kb was saved as: {name}");
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    
    private enum DownloadingType
    {
        БезОбразки,
        С_Сайта_БерсеркГерои
    }
}
