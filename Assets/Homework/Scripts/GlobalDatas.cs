using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CPlayerData
{
    public float maxHp;
    public float hp;
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