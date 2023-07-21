using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{

    private static SceneLoader _instance = null;
    public static SceneLoader Instance() { return _instance; }

    // Start is called before the first frame update
    public void Init()
    {
        _instance = this;

    }

    public void RegisterCallback(UnityAction<Scene, LoadSceneMode> finishLoaded)//UnityAction<Scene, LoadSceneMode>，可以直接把有Scene, LoadSceneMode這兩個參數的函式加進去。
    {
        SceneManager.sceneLoaded += finishLoaded;//把丟進finishLoaded的函式註冊進sceneLoaded，當我Load完場景以後，呼叫該函式。
    }

    public void UnRegisterCallback(UnityAction<Scene, LoadSceneMode> finishLoaded)
    {
        SceneManager.sceneLoaded -= finishLoaded;//如果有不同的場景，就需要反註冊。
    }

    public void ChangeScene(string name)
    {
        LoadingProgress.Instance().EnableProgress();
        SceneManager.LoadScene(name);//LoadScene預設模式：Single。
    }

    public IEnumerator ChangeSceneAsync(string name)
    {
        Debug.Log("Change scene async");
        LoadingProgress.Instance().EnableProgress();
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        if (ao == null)//防呆機制。
        {
            yield break; //直接跳出IEnumerator，以免找不到路徑、名字有錯，變成空的。
        }
        Debug.Log("Disable activation");
        ao.allowSceneActivation = false; //單純先把場景讀完，還不會去初始化它。
        float loadingRatio = 0.5f;
        while (true)
        {
            LoadingProgress.Instance().UpdateProgress(ao.progress * loadingRatio);
            if (ao.progress > 0.8999f)
            {
                ao.allowSceneActivation = true;
                break;
            }
            yield return 0; //每個frame進來迴圈一次。
        }
    }
}
