using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectData
{
    public GameObject go;
    public bool bUsing;
    public int id;
}
public class ListObjectData
{
    public Object dataSrc;
    public List<GameObjectData> pDatas;
}
[System.Serializable]
public class CPlayerData
{
    public float maxHp;
    public float hp;
    public float star;
}
[System.Serializable]
public class CEnemyData
{
    public float maxHp;
    public float hp;
}
public class CMessageData
{
    public int playerId;
    public string message;
}

public class CServerData
{
    public string serverName;
}