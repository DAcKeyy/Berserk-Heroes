using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CardDisplayURLimage : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private DownloadingType РежимСкачивания;
    public void SetURL_Image(string URL)
    {
        if(string.IsNullOrEmpty(URL)) image.sprite = Sprite.Create(null,new Rect(0f, 0f, 0f, 0f),new Vector2(0.5f,0.5f));
        StartCoroutine(setImage(URL, sprite => 
        {
            image.sprite = sprite;
        }));
    }

    IEnumerator setImage(string url, Action<Sprite> response) 
    {
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
                        response(sprite);
                    }

                    if (РежимСкачивания == DownloadingType.С_Сайта_БерсеркГерои)
                    {
                        var texture = DownloadHandlerTexture.GetContent(www);
                        var rect = new Rect(11f, 135f, 261, 220);
                        var sprite = Sprite.Create(texture,rect,new Vector2(0.5f,0.5f));
                        response(sprite);
                    }
                }
            }
        }
    }

    private enum DownloadingType
    {
        БезОбразки,
        С_Сайта_БерсеркГерои
    }
}
