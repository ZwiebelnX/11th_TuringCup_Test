using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour {

    public float PlayerID = 1; //当前玩家ID

    public int SkillScore = 30; //一个技能所花费的分数
    public int BoxDestroyScore = 5; //摧毁盒子获得的分数
    public int KillPlayerScore = 15; //击杀敌人获得的分数

    private int CurrentScore;//当前分数

    private void Start()
    {
        CurrentScore = 0;
        
    }

    //分数只读接口
    private int getScore()
    {
        return CurrentScore;
    }

    
    //增加分数接口 2018年8月5日截止有两种分数增加形式
    public void GainScore(string Type)
    {
        switch (Type)
        {
            case "BoxDestroy":
                CurrentScore += BoxDestroyScore;
                break;
            case "KillPlayer":
                CurrentScore += KillPlayerScore;
                break;
        }
    }

    //发动技能的扣分接口 在PlayerBuffer中调用
    public bool Upgrade()
    {
        if(CurrentScore >= SkillScore)
        {
            CurrentScore -= SkillScore;
            return true;
        }
        else
        {
            return false;
        }
    }
}
