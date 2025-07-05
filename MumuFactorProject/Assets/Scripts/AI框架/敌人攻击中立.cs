using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敌人攻击中立 : IEnemyState
{
    private EnemyAIFSMSystem manager;
    public 敌人攻击中立(EnemyAIFSMSystem manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        //Debug.Log("进入");
    }

    public void OnDecision()
    {

        //获取到数量最大的自己细胞
       CellController enemyCell =   GameController.Instance.GetMaxGeneumCell(1);
       CellController neutralCell = GameController.Instance.GetMinGeneumCell(-1);
        if (neutralCell == null)
        {
            EnemyAIFSMSystem.Instance.TransititionState(EnemyStateType.敌人攻击玩家);
        }
        if (enemyCell == null)
        {
            return;
        }


        //Debug.Log("中立细胞"+ neutralCell.name);
        //Debug.Log("敌人细胞" + enemyCell.name);
        enemyCell.Attack(neutralCell.gameObject);


        if (GameController.Instance.GetCellsByParty(-1).Length <= 2)
        {
            EnemyAIFSMSystem.Instance.TransititionState(EnemyStateType.敌人攻击玩家);
        }


        //Debug.Log("敌人攻击中立");
    }

    public void OnExit()
    {
        //Debug.Log("离开");
    }


}
