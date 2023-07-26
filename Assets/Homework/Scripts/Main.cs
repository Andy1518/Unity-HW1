using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance = null;
    public static Main Instance() { return _instance; }

    private GameObject bombObject = null;
    private GameObject heartObject = null;
    private GameObject starObject = null;

    //private LinkedList<GameObject> aaa;
    private List<GameObjectData> _enemies = new List<GameObjectData>();
    private GameObject[] _bombs;
    private GameObject[] _hearts;
    private GameObject[] _stars;

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

    }

    void FinishAsyncLoadGameObject(Object o)
    {
        bombObject = o as GameObject;
        //GenerateBombs(20);
        //GenerateHearts(10);
        //Debug.Log("FinishAsyncLoadObject " + o.name);
        //heartObject = o as GameObject;
        ObjectPool.Instance().InitObjectPool(50, bombObject);
        //ObjectPool.Instance().InitObjectPool(20, heartObject);
        // GenerateEnemies(20);
        Debug.Log("FinishAsyncLoadObject " + o.name);
    }

    // Start is called before the first frame update
    void Start()
    {
      

        //  if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("down");
        StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("CrateBomb", FinishAsyncLoadGameObject));//跑完就會執行FinishAsyncLoadGameObject。
        //StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("Heart", FinishAsyncLoadGameObject));
        // }
    }

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateBombs(5);
            GenerateHearts(2); 
            GenerateStars(2);
        }
    }

    public void RemoveBomb(GameObject go)
    {
        //for (int i = 0; i < _bombs.Length; i++)
        //{
        //    if (_bombs[i] == go)
        //    {
        //        _bombs[i] = null;
        //    }
        //}
        ObjectPool pool = ObjectPool.Instance();
        for (int i = 0; i < _enemies.Count; i++)
        {
            Debug.Log("RemoveEnemy " + go.name + ":" + _enemies[i].go.name);
            GameObjectData gData = _enemies[i];
            if (gData.go == go)
            {
                Debug.Log("RemoveEnemyIII  " + i);
                _enemies.RemoveAt(i);
                pool.UnLoadObjectToPool(gData);

            }
        }
    }

    private void GenerateBombs(int num) {

        if(bombObject == null)
        {
       //enemyObject = ResourceLoader.Instance().LoadGameObject("game1/BasicEnemy");
        }
        if(_bombs == null)
        {
            _enemies = new List<GameObjectData>();
        }
        ObjectPool pool = ObjectPool.Instance();
        //_bombs = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GameObjectData gData = pool.LoadObjectFromPool(false);
            GameObject go = gData.go;
            //GameObject go = GameObject.Instantiate(bombObject);
            //go.GetComponent<Renderer>().material.mainTexture = enemyTexture; 把讀進來的Texture(材質貼圖)變成Enemy的Texture。
            //go.GetComponent<Renderer>().material = enemyMaterial;//把讀進來的Material(材質球)變成Enemy的Material。
            Vector3 vdir = new Vector3(Random.Range(10.0f, 15.0f), Random.Range(1.0f, 1.2f), Random.Range(10.0f, 50.0f));
            if(vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(20.0f, 40.0f);
            //_bombs[i] = go;
            go.SetActive(true);
            _enemies.Add(gData);
        }
    }
    private void GenerateHearts(int num)
    {
        if (heartObject == null)
        {
            heartObject = ResourceLoader.Instance().LoadGameObject("Heart");
        }
        //if (_hearts == null)
        //{
        //    _enemies = new List<GameObjectData>();
        //}
        //ObjectPool pool = ObjectPool.Instance();
        _hearts = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            //GameObjectData gData = pool.LoadObjectFromPool(false);
            //GameObject go = gData.go;
            GameObject go = Instantiate(heartObject);
            Vector3 vdir = new Vector3(Random.Range(10.0f, 15.0f), Random.Range(1.0f, 1.2f), Random.Range(10.0f, 60.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(20.0f, 40.0f);
            _hearts[i] = go;
            //go.SetActive(true);
            //_enemies.Add(gData);
        }
    }
    private void GenerateStars(int num)
    {
        if (starObject == null)
        {
            starObject = ResourceLoader.Instance().LoadGameObject("BeveledStar");
        }
        //if (_hearts == null)
        //{
        //    _enemies = new List<GameObjectData>();
        //}
        //ObjectPool pool = ObjectPool.Instance();
        _stars = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            //GameObjectData gData = pool.LoadObjectFromPool(false);
            //GameObject go = gData.go;
            GameObject go = Instantiate(starObject);
            Vector3 vdir = new Vector3(Random.Range(10.0f, 15.0f), Random.Range(1.0f, 1.2f), Random.Range(10.0f, 60.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(20.0f, 40.0f);
            _stars[i] = go;
            //go.SetActive(true);
            //_enemies.Add(gData);
        }
    }
}
