using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingProgress : MonoBehaviour
{
    private static LoadingProgress _instance = null;
    public static LoadingProgress Instance() { return _instance; }


    private float _progress = 0.0f;
    public TMP_Text tmp;
    public Slider slider;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void EnableProgress()
    {
        _progress = 0.0f;
        this.gameObject.SetActive(true);
        UpdateProgress(0);
    }

    public void UpdateProgress(float p)
    {
        _progress = p;
        if (_progress < 0)
        {
            _progress = 0;
        }
        else if (_progress > 1.0f)
        {
            _progress = 1.0f;
        }
        int iPercent = Mathf.FloorToInt(_progress * 100.0f);//Floor:去掉小數點的浮點數。
        tmp.text = "Loading..." + iPercent.ToString() + "%";
        slider.value= p;
    }

    public void EndProgress()
    {
        _progress = 0.0f;
        this.gameObject.SetActive(false);
    }
}
