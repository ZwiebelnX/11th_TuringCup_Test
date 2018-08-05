using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int IncreaseHPValue = 30;//仅供测试 技能增加的血量数值

    private int CurrentHP;//当前角色血量

    private void Start()
    {
        CurrentHP = 100; //如果出现异常（例如开场时血量不为100）则改为Awake内触发
    }

    private void FixedUpdate()
    {
        if(CurrentHP <= 0)//角色血量小于0时死亡
        {
            CurrentHP = 0;
            PlayerDeath();
        }
    }

    //血量的只读接口
    public int GetHP()
    {
        return CurrentHP;
    }

    /*
     * 重点封装接口
     * 伤害数值应该只局限于几个固定数值
     * 防止此函数被利用
     */
    public void TakeDamage(int Damage, int AttackerID)
    {
        CurrentHP -= Damage;
    }

    //血量增加函数
    private void IncreaseHP()
    {
        CurrentHP += IncreaseHPValue;
    }

    private void PlayerDeath()
    {
        // TODO 角色死亡 设置动画 禁用脚本 传递分数
    }
}
