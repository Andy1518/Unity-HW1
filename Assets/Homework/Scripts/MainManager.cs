using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance = null;
    public static MainManager Instance() { return _instance; }


    public GameObject loadingObject;
    //public Button start;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameObject go = GameObject.Instantiate(loadingObject);
            go.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    IEnumerator customLoadData(string scName)
    {
        if (scName == "Stage1" || scName == "Menu")
        {
            float progress = 0.5f;//進度條。
            // load enemies
            for (var i = 0; i < 10; i++)
            {
                progress += i * 0.01f;
                GameObject go = new GameObject();//會建立在新場景。
                LoadingProgress.Instance().UpdateProgress(progress);
                yield return 0;
            }
            progress = 0.6f;
            LoadingProgress.Instance().UpdateProgress(progress);
            yield return new WaitForSeconds(1.0f);

            // setup player
            progress = 0.7f;
            LoadingProgress.Instance().UpdateProgress(progress);
            yield return new WaitForSeconds(1.0f);

            // spawn enemy
            progress = 0.8f;
            LoadingProgress.Instance().UpdateProgress(progress);
            yield return new WaitForSeconds(1.0f);

            // setup camera 
            progress = 1.0f;
            LoadingProgress.Instance().UpdateProgress(progress);
            yield return new WaitForSeconds(1.0f);

            // Disable Loading
            LoadingProgress.Instance().EndProgress();
        }
        yield break;
    }

    void FinishLoadScene(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        Debug.Log("FinishLoadScene " + scene.name);
        StartCoroutine(customLoadData(scene.name));

    }

    // Start is called before the first frame update
    void Start()
    {
        //按下按鈕時，呼叫ClickEvent()
        //GetComponent<Button>().onClick.AddListener(() => {
        //    ClickEvent();
        //});

        SceneLoader sc = SceneLoader.Instance();
        if (sc == null)
        {
            sc = new SceneLoader();
            sc.Init();
        }
        sc.RegisterCallback(FinishLoadScene);
    }
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            StartCoroutine(SceneLoader.Instance().ChangeSceneAsync("Stage1"));
            // SceneManager.LoadScene("fps", LoadSceneMode.Single);
        }
        else
        {
            StartCoroutine(SceneLoader.Instance().ChangeSceneAsync("Menu"));
            //SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if (SceneManager.GetActiveScene().name == "Menu")
        //    {
        //        StartCoroutine(SceneLoader.Instance().ChangeSceneAsync("Stage1"));
        //        // SceneManager.LoadScene("fps", LoadSceneMode.Single);
        //    }
        //    else
        //    {
        //        StartCoroutine(SceneLoader.Instance().ChangeSceneAsync("Menu"));
        //        //SceneManager.LoadScene("menu", LoadSceneMode.Single);
        //    }
        //}
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
