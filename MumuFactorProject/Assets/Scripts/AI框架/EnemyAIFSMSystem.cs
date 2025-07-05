using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyStateType
{
    敌人攻击中立,
    敌人攻击玩家
}

public class EnemyAIFSMSystem : MonoBehaviour
{
    //单例一下

    static EnemyAIFSMSystem instance;
    public static EnemyAIFSMSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyAIFSMSystem>();

            }
            return instance;
        }
    }




    private float decisionTime = 1.0f;//Ai所进行决策的时间

    private float decisionColdTime;//冷却时间



    //字典，用来存储不同状态所对应的对象
    private Dictionary<EnemyStateType,IEnemyState> states = new Dictionary<EnemyStateType, IEnemyState>();

    IEnemyState nowState;//敌人的状态是某一个具体的类




    private void Awake()
    {
        states.Add(EnemyStateType.敌人攻击中立, new 敌人攻击中立(this));
        states.Add(EnemyStateType.敌人攻击玩家, new 敌人攻击玩家(this));
    }

    void Start()
    {

        TransititionState(EnemyStateType.敌人攻击中立);
    }


    void Update()
    {
        decisionColdTime += Time.deltaTime;
        if (decisionColdTime >= decisionTime)
        {

            //执行AI的决策
            nowState.OnDecision();
            decisionColdTime = 0;
        }
    }


    //过渡状态
    public void TransititionState(EnemyStateType enemyStateType)
    {
        nowState?.OnExit();

        nowState = states[enemyStateType];
        nowState.OnEnter();
    }









}
