using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInit : MonoBehaviour
{//此程式掛在初始化場景以外的每個場景中。
    public static string debugSceneName;//訂public static讓大家都能讀取到
    public static int startPointNumber;
    GameObject playerObject;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (!playerObject)//如果該場景找不到Player，
        {
            SceneManager.LoadScene("Init");//回到初始化場景，
            debugSceneName = SceneManager.GetActiveScene().name;//把現在場景名字存到debugSceneName。            
        }
        if (startPointNumber != 0)
        {
            GameObject g = GameObject.Find(startPointNumber.ToString()) as GameObject;//找startPointNumber之物件。
            if (g != null)
            { //當我有找到該標籤的物件
                playerObject.transform.position = g.transform.position;//玩家位置等於該物件位置，等於把玩家傳送到我放該物件的位置。
            }
            startPointNumber = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
