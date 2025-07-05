using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPanel : BasePanel
{
    public TMP_Text textPlayerTitle;
    public TMP_Text textPlayerInfo;

    public GameObject 选关区域;


    private PlayerType[] playerInfos = { PlayerType.高初始值,PlayerType.高速度};
    private int index = 0;

    public override void OnExit()
    {

    }

    public override void OnEnter()
    {
        updateInfo();

        SaveSystem.Instance.LoadByJson();
        GameObject levelButton = Resources.Load<GameObject>("关卡");

        for (int i = 0; i < SaveSystem.Instance.levelNum; i++)
        {
            GameObject temp =  Instantiate(levelButton, transform.position, Quaternion.identity);

            temp.GetComponentInChildren<TMP_Text>().text = string.Format("第{0}关", i + 1);
            int index = i;


            //如果是第一关，需要加一个过场动画
            if (index + 1 == 1)
            {
                temp.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene("过场动画"); });
            }
            else
            {
                temp.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(index + 1); });
            }

            temp.transform.SetParent(选关区域.transform);
        }




  



        //Button[] buttons =  选关区域.GetComponentsInChildren<Button>();
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text  = string.Format("第{0}关",i+1);
        //    int index = i;
        //    buttons[i].onClick.AddListener(() => { SceneManager.LoadScene(index + 1); });
        //    //
        //}



        gameObject.SetActive(true);

    }

    public override void OnPause()
    {
        //gameObject.SetActive(false);
    }

    public override void OnContinue()
    {
        //gameObject.SetActive(true);
    }



    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void LeftSelect()
    {
        index++;
        if (index >= playerInfos.Length)
        {
            index = playerInfos.Length-1;
        }

        updateInfo();
    }

    public void RightSelect()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        updateInfo();
    }

    private void updateInfo()
    {
        PlayerInfo playerInfo = StartSceneController.Instance.getPlayerInfo(playerInfos[index]);
        SaveSystem.Instance.playerType = playerInfos[index];//获得玩家的类型
  textPlayerTitle.text = playerInfo.title;
        textPlayerInfo.text = playerInfo.info;

        //textPlayerInfo.text = Application.dataPath;

    }


    public void SelectLevel()
    {
        int levelNum = 选关区域.transform.childCount;
        //选关区域.transform.GetChild();
    }
}
