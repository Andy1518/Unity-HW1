using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance = null;
    public static Main Instance() { return _instance; }

    private GameObject enemyObject = null;

    //private LinkedList<GameObject> aaa;
    private GameObject[] _enemies;

    //private Texture2D enemyTexture;
    //private Material enemyMaterial;
   // public godViewCamCtrl camCtrl;

    private void Awake()
    {
        _instance = this;
        ResourceLoader r = new ResourceLoader();
        r.Init();
        //r.LoadObject("mats/Flash");
        // enemyTexture = r.LoadObject("textures/Lightning") as Texture2D;
        //enemyTexture = r.LoadTextureObject("Textures/MetalTrim_Albedo");
        //enemyMaterial = r.LoadObject("Materials/MetalTrim") as Material;//讀完轉型成Material。
        r.LoadAllObject("Resources");

        //r.LoadTextObject("Test/Data");

        /*int aaa = 0;
        int bbb = 0;
        for(int a = 0; a < 99999; a++)
        {
            for (int b = 0; b < 99999; b++)
            {
                bbb++;
            }
            aaa++;
        }
        Debug.Log("Awake" + aaa + bbb);*/
        //  Debug.Log("Finish load data " + enemyObject.name);
    }

    void FinishAsyncLoadGameObject(Object o)
    {
        enemyObject = o as GameObject;
        GenerateEnemies(20);
        Debug.Log("FinishAsyncLoadObject " + o.name);
    }

    // Start is called before the first frame update
    void Start()
    {
      

        //  if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("down");
        Debug.Log("Start 0");
        StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("CrateBomb", FinishAsyncLoadGameObject));//跑完就會執行FinishAsyncLoadGameObject。
        Debug.Log("Start 1");

       
        // }
    }
    /*private IEnumerator Start()
    {
        int aaa = 0;
        int bbb = 0;
        for (int a = 0; a < 99999; a++)
        {
            Debug.Log("Start" + aaa);
            for (int b = 0; b < 999; b++)
            {
                bbb++;
                Debug.Log("Start" + bbb);
                yield return 0;
            }
            aaa++;
          
            yield return 0;
        }
        StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("game1/BasicEnemy", FinishAsyncLoadGameObject));
        Debug.Log("Start" + aaa + bbb);
    }*/

    // Update is called once per frame
    void Update()
    {
        // aaa = new LinkedList<GameObject>();
      //  if(Input.GetMouseButtonDown(0))
      //  {
       //     Debug.Log("down");
        //    StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("game1/BasicEnemy"));
       // }
       // Debug.Log("time " + Time.deltaTime);
        
    }

    public void RemoveEnemy(GameObject go)
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] == go)
            {
                _enemies[i] = null;
            }
        }
    }

    private void GenerateEnemies(int num) {

        if(enemyObject == null)
        {
       //enemyObject = ResourceLoader.Instance().LoadGameObject("game1/BasicEnemy");
        }
        _enemies = new GameObject[num]; 
        for (int i = 0; i < num; i++)
        {
            GameObject go = GameObject.Instantiate(enemyObject);
            //go.GetComponent<Renderer>().material.mainTexture = enemyTexture; 把讀進來的Texture(材質貼圖)變成Enemy的Texture。
            //go.GetComponent<Renderer>().material = enemyMaterial;//把讀進來的Material(材質球)變成Enemy的Material。
            Vector3 vdir = new Vector3(Random.Range(0.8f, 1.0f), Random.Range(0.01f, 0.02f), Random.Range(0.5f, 0.6f));
            if(vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(90.0f, 100.0f);
            _enemies[i] = go;
        }
    }
}
