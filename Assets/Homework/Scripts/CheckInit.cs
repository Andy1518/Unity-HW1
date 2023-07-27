using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInit : MonoBehaviour
{//���{�����b��l�Ƴ����H�~���C�ӳ������C
    public static string debugSceneName;//�qpublic static���j�a����Ū����
    public static int startPointNumber;
    GameObject playerObject;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (!playerObject)//�p�G�ӳ����䤣��Player�A
        {
            SceneManager.LoadScene("Init");//�^���l�Ƴ����A
            debugSceneName = SceneManager.GetActiveScene().name;//��{�b�����W�r�s��debugSceneName�C            
        }
        if (startPointNumber != 0)
        {
            GameObject g = GameObject.Find(startPointNumber.ToString()) as GameObject;//��startPointNumber������C
            if (g != null)
            { //��ڦ����Ӽ��Ҫ�����
                playerObject.transform.position = g.transform.position;//���a��m����Ӫ����m�A����⪱�a�ǰe��ک�Ӫ��󪺦�m�C
            }
            startPointNumber = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
