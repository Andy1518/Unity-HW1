using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPlayer : MonoBehaviour
{
    public string startScece;//此程式放在只有人物和畫布之初始化場景Init，並將此欄位設定至第一關的場景，只要一進初始化場景就會馬上轉到第一關。
    // Start is called before the first frame update
    void Start()
    {
        if (CheckInit.debugSceneName == null) {//如果目前debugSceceName是空的，
            SceneManager.LoadScene(startScece);//表示目前沒有載入任何場景，所以從初始化場景載入到第一關。
        }
        else {//debugSceneName有儲存場景名稱，表示我們是從別的場景過來初始化場景的，
            SceneManager.LoadScene(CheckInit.debugSceneName);//所以初始化後要載回到原本待在的那個場景。
            CheckInit.debugSceneName = null;//把debugSceneName清空。
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
