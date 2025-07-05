using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    高初始值,
    高速度,
}

public class PlayerInfo
{
    public string title;
    public string info;
    public PlayerInfo(string title, string info)
    {
        this.title = title;
        this.info = info;
    }
}


public class StartSceneController : MonoBehaviour
{

    static StartSceneController instance;
    public static StartSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StartSceneController>();

            }
            return instance;
        }
    }



    //UI控制
    public BasePanel startPanel;
    public BasePanel levelPanel;


    public Dictionary<PlayerType, PlayerInfo> playerDictionary = new Dictionary<PlayerType, PlayerInfo>();


    public PlayerInfo getPlayerInfo(PlayerType playerType)
    {
        return playerDictionary[playerType];
    }

    private void Start()
    {




        //跨场景不销毁
        //DontDestroyOnLoad(gameObject);

        playerDictionary.Add(PlayerType.高初始值,new PlayerInfo("AaBb", "该性状可以使您的宿主再初始时拥有更多的基因数量。增加20点初始基因！"));
        playerDictionary.Add(PlayerType.高速度, new PlayerInfo("AABB", "该性状可以使您的初始宿主更快！加1点速度。"));
        UISystem.Instance.PushPanel(startPanel);

    }


}
