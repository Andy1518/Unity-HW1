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

    public void RegisterCallback(UnityAction<Scene, LoadSceneMode> finishLoaded)//UnityAction<Scene, LoadSceneMode>�A�i�H�����⦳Scene, LoadSceneMode�o��ӰѼƪ��禡�[�i�h�C
    {
        SceneManager.sceneLoaded += finishLoaded;//���ifinishLoaded���禡���U�isceneLoaded�A���Load�������H��A�I�s�Ө禡�C
    }

    public void UnRegisterCallback(UnityAction<Scene, LoadSceneMode> finishLoaded)
    {
        SceneManager.sceneLoaded -= finishLoaded;//�p�G�����P�������A�N�ݭn�ϵ��U�C
    }

    public void ChangeScene(string name)
    {
        LoadingProgress.Instance().EnableProgress();
        SceneManager.LoadScene(name);//LoadScene�w�]�Ҧ��GSingle�C
    }

    public IEnumerator ChangeSceneAsync(string name)
    {
        Debug.Log("Change scene async");
        LoadingProgress.Instance().EnableProgress();
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        if (ao == null)//���b����C
        {
            yield break; //�������XIEnumerator�A�H�K�䤣����|�B�W�r�����A�ܦ��Ū��C
        }
        Debug.Log("Disable activation");
        ao.allowSceneActivation = false; //��¥������Ū���A�٤��|�h��l�ƥ��C
        float loadingRatio = 0.5f;
        while (true)
        {
            LoadingProgress.Instance().UpdateProgress(ao.progress * loadingRatio);
            if (ao.progress > 0.8999f)
            {
                ao.allowSceneActivation = true;
                break;
            }
            yield return 0; //�C��frame�i�Ӱj��@���C
        }
    }
}
