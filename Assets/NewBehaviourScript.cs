using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject text;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(IGetData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator IGetData()
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("msg", "Server");//设置发送数据
            WWW www = new WWW("http://127.0.0.1/index.php", form);//下载与上传数据
            yield return www; //等待Web服务器
            text.GetComponent<Text>().text = www.text;
            Debug.Log(www.text);
        }

    }
}