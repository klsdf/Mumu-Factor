using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敌人攻击玩家 : IEnemyState
{
    private EnemyAIFSMSystem manager;
    public 敌人攻击玩家(EnemyAIFSMSystem manager)
    {
        this.manager = manager;
    }

    private float stayTime = 10;
    private float coldTime = 0;


    public void OnDecision()
    {
        //获得数量最少的玩家细胞
        CellController playerCell =  GameController.Instance.GetMinGeneumCell(0);
        CellController enemyCell = GameController.Instance.GetMaxGeneumCell(1);
        if (enemyCell == null || playerCell == null)
        {
            return;
        }
        enemyCell.Attack(playerCell.gameObject);


        //如果还有中立那么就先打中立
        coldTime++;
        if (coldTime > stayTime && GameController.Instance.GetCellsByParty(-1).Length >= 1)
        {
            EnemyAIFSMSystem.Instance.TransititionState(EnemyStateType.敌人攻击中立);
        }



    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
