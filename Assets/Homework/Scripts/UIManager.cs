using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image playerHpBar;
    public TMP_Text starCount;
    private Canvas _canvas;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePlayerUIInfo(CPlayerData data)
    {
        playerHpBar.fillAmount = Mathf.Lerp(playerHpBar.fillAmount, data.hp / data.maxHp, 0.1f);
        starCount.text = "Star:" + data.star;
    }
}
