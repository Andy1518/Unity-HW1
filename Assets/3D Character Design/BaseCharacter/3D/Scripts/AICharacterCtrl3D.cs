using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AICharacterCtrl3D : MonoBehaviour {    
    [SerializeField] LayerMask lockOnFilter;//�Ψӳ]�w��w��H�O����Layer���C�HAI�ӻ��N�O��wPlayer�C
    [SerializeField] float stoppingDistance = 2;    
    [SerializeField] float m_seeRadius = 20;//�i���b�|�C���a�i��o�ӽd��AI�~�|�ݨ쪱�a�C
    public float seeRadius { //���Ѥ@�Ӥ��}�ݩʡA��m_seeRadius�ȶǥX�h�C
        get { return m_seeRadius; } //�o�˨䥦�{���N�i�HŪ���o�ӭȡA������g�J�ק�C
    }
    [SerializeField] float m_lockOnRadius = 15;
    public float lockOnRadius {
        get { return m_lockOnRadius; }
    }
    [SerializeField] float m_atkRadius = 10;//�����b�|�C
    public float atkRadius {
        get { return m_atkRadius; }
    }
    [SerializeField] float m_safeRadius = 5;//�w���b�|�C
    public float safeRadius {
        get { return m_safeRadius; }
    }
    
    Character3D character;
    [HideInInspector] public Vector3 destPosition;//���F�ʺA�������a��m�ҥH�o�˳]�w�A�ܼƤ��e�Ѩ䥦�{���M�w�C�p�G����ӥH�W���a�AAI�~���|�u����w�@�ӡC
    [HideInInspector] public Vector2 axisInput;
    //[HideInInspector] public float speedScale;
    public Transform lockTarget {//��w���ؼЪ��y�СC�]���O�ݩʪ��q�k�A�ҥH���|�X�{�b�Ѽƭ��O�W�A����HideInInspector�C�W���ܼƤ~�ݭn�C
        get;//�i�H�Q�~��Ū���C
        private set;//���i�Q�~���{���g�J�ק�C
    }
    public bool lockOn {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start() {
        character = GetComponent<Character3D>();//��Character 3D�{���C
        destPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        /*if (lockTarget == null) {//if�̭��@��񪺬O����A���G�|�Otrue��false�C
            �b�S����w�ؼЮɡA�ˬd����O�_�����a�s�b�H
        }
        else { 
            ����w�Y�ӥؼЮɡA�ˬd���a�O�_�]�X�i���d�򤧥~�H
        }*/
        /*if (lockTarget) {//�@��lockTarget�᭱�n�[==null�������A�]��lockTarget�O�Ѧҫ��O�A�ҥH�i�H�����o�˼g�C
                           //if(lockTarget)�N��N�O�p�GlockTarget�����V�Y�ӰO�����m�A�N�N������true�C
                           //�]�i�H�gif(!lockTarget)�A�N��O�p�GlockTarget�S�����V�O�����m�C
            ����w�Y�ӥؼЮɡA�ˬd���a�O�_�]�X�i���d�򤧥~�H
        }
        else { 
            �b�S����w�ؼЮɡA�ˬd����O�_�����a�s�b�H
        }*/
        if (lockTarget) { //�b��w�Y�ӥؼЮɡA
            if (Vector3.Distance(transform.position, lockTarget.position) > m_seeRadius + 0.5f) {//�ˬd���a���S���]�X�i���d�򤧥~�C
                lockTarget = null;//��lockTarget�]��null�ȡA�Y�Ѱ���w�C
            }
        }
        else {            
            Collider[] lockTargets = Physics.OverlapSphere(transform.position, seeRadius, lockOnFilter);//OverlapSphere:�HAI������ˬd�d�򤺦��S�����a�C
            if (lockTargets.Length > 0) {
                IEnumerable<Collider> colliders =//���y�k�Ӧ�System.Linq�A�Ψӭ��s�Ƨǰ}�C�Ccolliders�����@�Ӷ��X�C�]�i�H�Ψ�Ӱj�鵥�g�k�Ӱ��A�����·СC
                    from e in lockTargets
                  //where �z�����C�Ҧp�z�缾�a���S���bAI���e�@�w�d�򪺵����̭��A�άOť�쪱�a���n���A
                  //�����O�u���n�����n���A�i�H�]�w�p�G���a�ζ]���AspeedScale�O1�A�N����AI�|ť���n���õo�{���a�C
                    orderby Vector3.Distance(transform.position, e.transform.position) ascending
                    select e; //�D�XAI��Ҧ����a�������Z���A�M��Ѥp��j�ƧǡC
                if (colliders.Count() > 0) {//�p�G�z�粒���٦��Ѿl�ŦX���󪺡A
                    lockTarget = colliders.First().transform;//First:�춰�X�̭��Ĥ@�ӥؼСA�]�N�O��w�Z��AI�̪񪺪��a�C�n��̻����h�i�H��Last�C
                }//lockTarget��w�ؼЪ���ƬO�n�����A��MovementState�{���ϥΪ��C
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
        Vector3 move = Vector3.zero;//�@�}�l�S���t�סC
        if(NavMesh.CalculatePath(transform.position, destPosition, -1, navMeshPath)) {//transform.position:�_�I�A�qAI����m�CdestPosition:�ؼСA�q���a����m�C
                                                                 //-1:�N��i�H�p�⪺�ɯ����ϰ�A-1�򥻤W��������F�C
        //CalculatePath�����|�^�Ǥ@��Bool�ȡA�T�{���S���p��X���o�F���a��m�����|�C
        //���u��|�p��̵u���|�A�ҥH�|������I�A�]�p�ߪk�N�O������I��ثeAI�y�Ф���A�o��@�ӦV�q��A�Υ���@move�ǵ�AI�A�������ʤ�V�C
            if (navMeshPath.corners.Length >= 2) {//corners.Length��ܰ}�C������I�ƶq�C�N�䬰�ˬd���ʸ��|������I�O�_���j��2�A�o�ˤ~��ΤU�@���I��W�@���I�o��V�q�C
                float remainingDistance = 0;//�ѤU�٨S�������Z���C
                for (int i = 0; i < navMeshPath.corners.Length - 1; i++) {
                //���]�`�@�|���I�A�}�C���ޭȴN��0~3�A�o�˹�ڤW�p��̦h�N��[3]�A���|�A�X�{[4]�Ӹ�W�@���I[3]�۴�A�]��i < navMeshPath.corners.Length�n-1�C
                    remainingDistance += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i + 1]);
                }
                //if (remainingDistance > stoppingDistance)//stoppingDistance:����ʪ��Z���C
                move = (navMeshPath.corners[1] - navMeshPath.corners[0]).normalized;//�U���I��W���I�����V�q�C
                if (remainingDistance < stoppingDistance) {
                    axisInput = Vector2.zero;
                }
            }
        }        
        //character.Move(move * speedScale, false);//speedScale:�t�ת��Y��A0:���ʡA0.5:�����A1:�]�B�Cfalse:AI���|���C
        character.Move(move, axisInput, false, lockOn);
    }

    private void OnDrawGizmosSelected() {//��H�W�]�w���U�ؽd��e�X�ӡC
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_seeRadius);//DrawWireSphere:�e�@�ӥu���u�ت��y��X�ӡC
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_lockOnRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_atkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_safeRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destPosition, 0.2f);//��ت��a�e�X�ӡC��¬��F����@�I�ҥH�e�T��C
        Gizmos.DrawWireSphere(destPosition, 0.25f);
        Gizmos.DrawWireSphere(destPosition, 0.3f);
    }
}