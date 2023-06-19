using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AICharacterCtrl3D : MonoBehaviour {    
    [SerializeField] LayerMask lockOnFilter;//用來設定鎖定對象是哪個Layer的。以AI來說就是鎖定Player。
    [SerializeField] float stoppingDistance = 2;    
    [SerializeField] float m_seeRadius = 20;//可見半徑。玩家進到這個範圍內AI才會看到玩家。
    public float seeRadius { //提供一個公開屬性，把m_seeRadius值傳出去。
        get { return m_seeRadius; } //這樣其它程式就可以讀取這個值，但不能寫入修改。
    }
    [SerializeField] float m_lockOnRadius = 15;
    public float lockOnRadius {
        get { return m_lockOnRadius; }
    }
    [SerializeField] float m_atkRadius = 10;//攻擊半徑。
    public float atkRadius {
        get { return m_atkRadius; }
    }
    [SerializeField] float m_safeRadius = 5;//安全半徑。
    public float safeRadius {
        get { return m_safeRadius; }
    }
    
    Character3D character;
    [HideInInspector] public Vector3 destPosition;//為了動態偵測玩家位置所以這樣設定，變數內容由其它程式決定。如果有兩個以上玩家，AI才不會只能鎖定一個。
    [HideInInspector] public Vector2 axisInput;
    //[HideInInspector] public float speedScale;
    public Transform lockTarget {//鎖定的目標的座標。因為是屬性的訂法，所以不會出現在參數面板上，不用HideInInspector。上面變數才需要。
        get;//可以被外部讀取。
        private set;//不可被外部程式寫入修改。
    }
    public bool lockOn {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start() {
        character = GetComponent<Character3D>();//抓Character 3D程式。
        destPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        /*if (lockTarget == null) {//if裡面一般放的是條件，結果會是true或false。
            在沒有鎖定目標時，檢查附近是否有玩家存在？
        }
        else { 
            當鎖定某個目標時，檢查玩家是否跑出可見範圍之外？
        }*/
        /*if (lockTarget) {//一般lockTarget後面要加==null之類的，因為lockTarget是參考型別，所以可以直接這樣寫。
                           //if(lockTarget)意思就是如果lockTarget有指向某個記憶體位置，就將它視為true。
                           //也可以寫if(!lockTarget)，意思是如果lockTarget沒有指向記憶體位置。
            當鎖定某個目標時，檢查玩家是否跑出可見範圍之外？
        }
        else { 
            在沒有鎖定目標時，檢查附近是否有玩家存在？
        }*/
        if (lockTarget) { //在鎖定某個目標時，
            if (Vector3.Distance(transform.position, lockTarget.position) > m_seeRadius + 0.5f) {//檢查玩家有沒有跑出可見範圍之外。
                lockTarget = null;//把lockTarget設成null值，即解除鎖定。
            }
        }
        else {            
            Collider[] lockTargets = Physics.OverlapSphere(transform.position, seeRadius, lockOnFilter);//OverlapSphere:以AI為圓心檢查範圍內有沒有玩家。
            if (lockTargets.Length > 0) {
                IEnumerable<Collider> colliders =//此語法來自System.Linq，用來重新排序陣列。colliders視為一個集合。也可以用兩個迴圈等寫法來做，但較麻煩。
                    from e in lockTargets
                  //where 篩選條件。例如篩選玩家有沒有在AI面前一定範圍的視角裡面，或是聽到玩家的聲音，
                  //但不是真的要偵測聲音，可以設定如果玩家用跑的，speedScale是1，就等於AI會聽到聲音並發現玩家。
                    orderby Vector3.Distance(transform.position, e.transform.position) ascending
                    select e; //求出AI跟所有玩家之間的距離，然後由小到大排序。
                if (colliders.Count() > 0) {//如果篩選完後還有剩餘符合條件的，
                    lockTarget = colliders.First().transform;//First:抓集合裡面第一個目標，也就是鎖定距離AI最近的玩家。要抓最遠的則可以用Last。
                }//lockTarget鎖定目標的資料是要給狀態機MovementState程式使用的。
            }
        }        

        if (lockTarget) {
            if (lockOn) {
                if (Vector3.Distance(transform.position, lockTarget.position) > m_lockOnRadius + 0.5f) {
                    lockOn = false;
                }
            }
            else {
                if (Vector3.Distance(transform.position, lockTarget.position) < m_lockOnRadius) {
                    lockOn = true;
                }
            }
        }
        else lockOn = false;
        
        NavMeshPath navMeshPath = new NavMeshPath();
        Vector3 move = Vector3.zero;//一開始沒有速度。
        if(NavMesh.CalculatePath(transform.position, destPosition, -1, navMeshPath)) {//transform.position:起點，訂AI的位置。destPosition:目標，訂玩家的位置。
                                                                 //-1:代表可以計算的導航網格區域，-1基本上等於全部了。
        //CalculatePath本身會回傳一個Bool值，確認有沒有計算出能到得了玩家位置的路徑。
        //此工具會計算最短路徑，所以會有轉折點，設計心法就是讓轉折點跟目前AI座標互減，得到一個向量後，用它當作move傳給AI，給它移動方向。
            if (navMeshPath.corners.Length >= 2) {//corners.Length表示陣列內轉折點數量。意思為檢查移動路徑的轉折點是否有大於2，這樣才能用下一個點減掉上一個點得到向量。
                float remainingDistance = 0;//剩下還沒走完的距離。
                for (int i = 0; i < navMeshPath.corners.Length - 1; i++) {
                //假設總共四個點，陣列索引值就有0~3，這樣實際上計算最多就到[3]，不會再出現[4]來跟上一個點[3]相減，因此i < navMeshPath.corners.Length要-1。
                    remainingDistance += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i + 1]);
                }
                //if (remainingDistance > stoppingDistance)//stoppingDistance:停止移動的距離。
                move = (navMeshPath.corners[1] - navMeshPath.corners[0]).normalized;//下個點減上個點的單位向量。
                if (remainingDistance < stoppingDistance) {
                    axisInput = Vector2.zero;
                }
            }
        }        
        //character.Move(move * speedScale, false);//speedScale:速度的縮放，0:不動，0.5:走路，1:跑步。false:AI不會跳。
        character.Move(move, axisInput, false, lockOn);
    }

    private void OnDrawGizmosSelected() {//把以上設定的各種範圍畫出來。
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_seeRadius);//DrawWireSphere:畫一個只有線框的球體出來。
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_lockOnRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_atkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_safeRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destPosition, 0.2f);//把目的地畫出來。單純為了明顯一點所以畫三圈。
        Gizmos.DrawWireSphere(destPosition, 0.25f);
        Gizmos.DrawWireSphere(destPosition, 0.3f);
    }
}