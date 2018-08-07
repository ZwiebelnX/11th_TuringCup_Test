﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个类是选手们书写代买的父类
/// 绑定在四个游戏角色上
/// 通过事件驱动模式操作角色
/// 通过只读接口获取数据
/// </summary>
public class TuringOperate : MonoBehaviour {
    /*
     * 目前属于人为测试阶段
     * 所有的操作均通过键盘输入
     * 故通过FixedUpdate进行操作监控
     */
    private void FixedUpdate()
    {
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if(h != 0 || v != 0)
        {
            Movement(h, v);//选手操作：移动
        }
        SetBomb(); //选手操作：放置炸弹
        Buff(); //选手操作：加强
    }

    // TODO 拆分Movement成四个方向的移动函数
    private bool Movement(float h, float v)
    {
        Dictionary<string, object> TempDic = new Dictionary<string, object>(); //创建临时Dictionary 传递参数
        TempDic.Add("Horizontal", h);
        TempDic.Add("Vertical", v);
        //发送移动事件（指定了执行对象）
        bool isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.TURING_MOVE, this, gameObject, TempDic); 
        TempDic.Clear(); //清理临时Dictionary
        return isSuccess;
    }

    /// <summary>
    /// 在角色当前的座标位置放置一颗炸弹
    /// </summary>
    /// <returns>操作是否成功</returns>
    private bool SetBomb()
    {
        bool isSuccess = false;
        if (Input.GetButton("Fire1"))
        {
            //发送放置炸弹（指定了执行对象）
            isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.TURING_SET_BOMB, this, gameObject);
        }
        return isSuccess;
    }

    //仅供测试 用于监测是否有加强对应的按键输入
    private void Buff()
    {
        BuffBomb();
        BuffHP();
        BuffShoot();
    }

    /// <summary>
    /// 扣除一定积分 扩大炸弹的爆炸范围 持续5秒
    /// </summary>
    /// <returns>操作是否成功</returns>
    private bool BuffBomb()
    {
        bool isSuccess = false;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //发送选手加强炸弹事件（指定了执行对象）
            isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_BOMB, this, gameObject);
            return isSuccess;//返回操作结果
        }
        else
            return false;
    }

    /// <summary>
    /// 扣除一定积分 增加射击威力 持续5秒
    /// </summary>
    /// <returns></returns>
    //备注见上
    private bool BuffShoot()
    {
        bool isSuccess = false;
        if (Input.GetKey(KeyCode.Alpha2))
        {
            isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_SHOOT, this, gameObject);
            return isSuccess;
        }
        else
            return false;
    }

    /// <summary>
    /// 扣除一定积分 恢复一定血量
    /// </summary>
    /// <returns></returns>
    //备注见上
    private bool BuffHP()
    {
        bool isSuccess = false;
        if (Input.GetKey(KeyCode.Alpha3))
        {
            isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_HP, this, gameObject);
            return isSuccess;
        }
        else
            return false;
    }
}
