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
        CheckInit.startPointNumber = pointNumber;//把傳送門編號存到startPointNumber中。之後一走傳送門就會走到我放的傳送點位置。
    }
}
