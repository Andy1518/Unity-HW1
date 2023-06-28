using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance = null;
    public static Main Instance() { return _instance; }

    private GameObject bombObject = null;
    private GameObject heartObject = null;

    //private LinkedList<GameObject> aaa;
    private GameObject[] _bombs;
    private GameObject[] _hearts;

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
        //enemyMaterial = r.LoadObject("Materials/MetalTrim") as Material;//Ū���૬��Material�C
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
        bombObject = o as GameObject;
        GenerateBombs(20);
        GenerateHearts(10);
        Debug.Log("FinishAsyncLoadObject " + o.name);
    }

    // Start is called before the first frame update
    void Start()
    {
      

        //  if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("down");
        Debug.Log("Start 0");
        StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("CrateBomb", FinishAsyncLoadGameObject));//�]���N�|����FinishAsyncLoadGameObject�C
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

    public void RemoveBomb(GameObject go)
    {
        for (int i = 0; i < _bombs.Length; i++)
        {
            if (_bombs[i] == go)
            {
                _bombs[i] = null;
            }
        }
    }

    private void GenerateBombs(int num) {

        if(bombObject == null)
        {
       //enemyObject = ResourceLoader.Instance().LoadGameObject("game1/BasicEnemy");
        }
        _bombs = new GameObject[num]; 
        for (int i = 0; i < num; i++)
        {
            GameObject go = GameObject.Instantiate(bombObject);
            //go.GetComponent<Renderer>().material.mainTexture = enemyTexture; ��Ū�i�Ӫ�Texture(����K��)�ܦ�Enemy��Texture�C
            //go.GetComponent<Renderer>().material = enemyMaterial;//��Ū�i�Ӫ�Material(����y)�ܦ�Enemy��Material�C
            Vector3 vdir = new Vector3(Random.Range(10.0f, 15.0f), Random.Range(1.0f, 1.2f), Random.Range(1.0f, 50.0f));
            if(vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(20.0f, 40.0f);
            _bombs[i] = go;
        }
    }
    private void GenerateHearts(int num)
    {

        if (heartObject == null)
        {
            heartObject = ResourceLoader.Instance().LoadGameObject("Heart");
        }
        _hearts = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(heartObject);
            Vector3 vdir = new Vector3(Random.Range(10.0f, 15.0f), Random.Range(1.0f, 1.2f), Random.Range(1.0f, 50.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(20.0f, 40.0f);
            _hearts[i] = go;
        }
    }
}
