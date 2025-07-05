using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();

        DontDestroyOnLoad(gameObject);
    }






    private static BGMController instance;   // 单例
    public BGMController Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null)
        {
            //这里一定要是销毁this.gameObject
            Destroy(this.gameObject);
            return;
        }
        //这句话只执行一次，第二次上面return了
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
