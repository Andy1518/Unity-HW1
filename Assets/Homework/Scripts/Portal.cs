using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public int pointNumber;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.tag = "Portal";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene() {
        SceneManager.LoadScene(sceneName);
        CheckInit.startPointNumber = pointNumber;//��ǰe���s���s��startPointNumber���C����@���ǰe���N�|����ک񪺶ǰe�I��m�C
    }
}
