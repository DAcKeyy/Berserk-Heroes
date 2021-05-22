using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading_Indicator_fillAmount : MonoBehaviour
{
    [SerializeField] private float fillValue;
    [SerializeField] bool SpinOnEnable;
    private Image LoadingImage;
    private bool spining;
    private void Awake()
    {
        if(GetComponent<Image>() != null) LoadingImage = GetComponent<Image>();
        else Debug.LogWarning("Set Image to the object!");
        //StartFillAmount(true);
    }

    private void OnEnable()
    {
        StartFillAmount(SpinOnEnable);
    }

    public void StartFillAmount(bool isSpining)
    {
        spining = isSpining;
        if(spining) StartCoroutine(Loading_Indicator_Move());
        else
        {
            SetToZero();
        }
    }

    private void ReSet()
    {
        LoadingImage.fillAmount = 1;
    }
    
    private void SetToZero()
    {
        LoadingImage.fillAmount = 0;
    }

    public IEnumerator Loading_Indicator_Move()
    {
        while (spining)
        {
            LoadingImage.fillAmount += fillValue;

            if (LoadingImage.fillAmount == 1f)
            {
                LoadingImage.fillClockwise = !LoadingImage.fillClockwise;
                fillValue *= -1;
            }

            if (LoadingImage.fillAmount == 0f)
            {
                //LoadingImage.sprite = Sprites[Random.Range(0, Sprites.Count)];
                LoadingImage.fillClockwise = !LoadingImage.fillClockwise;
                fillValue *= -1;
            }
            yield return null;
        }
    }
}
