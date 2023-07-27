using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    [SerializeField]
    public CPlayerData playerData;
    Animator animator;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.UpdatePlayerUIInfo(playerData);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UIManager.instance.UpdatePlayerUIInfo(playerData);
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                collision.gameObject.transform.GetComponent<Portal>().ChangeScene();
            }
        }
    }
    public void Hurt(float hurt)
    {
        animator.SetTrigger("Hurt");
        playerData.hp -= hurt;
        if (playerData.hp < 0)
        {
            playerData.hp = 0;
        }
        Debug.Log(playerData.hp);
    }
    public void Heal(float heal)
    {
        playerData.hp += heal;
        if (playerData.hp > playerData.maxHp)
        {
            playerData.hp = playerData.maxHp;
        }
    }
    public void GetStar()
    {
        playerData.star += 1;
    }
}
