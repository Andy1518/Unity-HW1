using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPlayer : MonoBehaviour
{
    public string startScece;//���{����b�u���H���M�e������l�Ƴ���Init�A�ñN�����]�w�ܲĤ@���������A�u�n�@�i��l�Ƴ����N�|���W���Ĥ@���C
    // Start is called before the first frame update
    void Start()
    {
        if (CheckInit.debugSceneName == null) {//�p�G�ثedebugSceceName�O�Ū��A
            SceneManager.LoadScene(startScece);//��ܥثe�S�����J��������A�ҥH�q��l�Ƴ������J��Ĥ@���C
        }
        else {//debugSceneName���x�s�����W�١A��ܧڭ̬O�q�O�������L�Ӫ�l�Ƴ������A
            SceneManager.LoadScene(CheckInit.debugSceneName);//�ҥH��l�ƫ�n���^��쥻�ݦb�����ӳ����C
            CheckInit.debugSceneName = null;//��debugSceneName�M�šC
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
