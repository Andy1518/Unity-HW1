using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool _instance = null;
    public static ObjectPool Instance() { return _instance; }

    private ListObjectData pObjectList;

    public void Awake()
    {
        _instance = this;
    }

    public void InitObjectPool(int iCount, Object o)
    {
        int i = 0;
        pObjectList = new ListObjectData();//new List<GameObjectData>();
        pObjectList.dataSrc = o;
        pObjectList.pDatas = new List<GameObjectData>();
        
        for(i= 0; i < iCount; i++) {
            GameObject go = GameObject.Instantiate(o,transform) as GameObject;
            GameObjectData gData = new GameObjectData();
            gData.go = go;
            gData.bUsing = false;
            gData.id = pObjectList.pDatas.Count;
            go.SetActive(false);
            pObjectList.pDatas.Add(gData);
        }
    }

    public GameObjectData LoadObjectFromPool(bool bForceLoad)
    {
        int iCount = pObjectList.pDatas.Count;
        int i = 0;
        for(i = 0; i < iCount; i++)
        {
            GameObjectData gData = pObjectList.pDatas[i];
            if(gData.bUsing)
            {
                continue;
            }

            gData.bUsing = true;
            return gData;
        }

        if(bForceLoad) //bForceLoad=true的話，可以強制產生超過數量上限的物件，給false則不能產生超過數量上限的物件。
        {
            GameObject go = GameObject.Instantiate(pObjectList.dataSrc) as GameObject;
            GameObjectData gData = new GameObjectData();
            gData.go = go;
            gData.bUsing = true;
            gData.id = pObjectList.pDatas.Count;
            go.SetActive(false);
            pObjectList.pDatas.Add(gData);
            return gData;
        }

        return null;
    }

    public void UnLoadObjectToPool(GameObjectData gData)
    {
        Debug.Log("UnLoadObjectToPool " + gData.id);
        int id = gData.id;
        pObjectList.pDatas[id].bUsing = false;
        pObjectList.pDatas[id].go.SetActive(false);
        /*int iCount = pObjectList.pDatas.Count;
        int i = 0;
        for (i = 0; i < iCount; i++)
        {
            GameObjectData gData = 
            if (gData.go == go)
            {
                gData.go.SetActive(false);
                gData.bUsing = false;
                return;
            }
        }*/
    }
}
