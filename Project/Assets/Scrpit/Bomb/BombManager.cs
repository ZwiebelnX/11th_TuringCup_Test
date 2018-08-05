 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour {
    public float BoomTime = 3f; //仅供测试 炸弹爆炸时间
    public int BombPower = 30;//仅供测试 炸弹威力

    private bool HadSetInfo = false; //炸弹是否设置了基本信息
    private int BombOwner = 0; //炸弹所有者编号
    private float Timing = 0; //炸弹倒计时  

    private float BombRadius; //仅供测试 炸弹爆炸范围 

    /*
     * ----------------------注意----------------------
     * 在Unity中 炸弹的Inspector排序中
     * 默认未启用的Sphere Collider必须在默认启用的Sphere Collider的上面
     * 否则在当前代码的条件下无法实现玩家退出炸弹后
     * 不能再次进入炸弹模型的功能
     */
    private SphereCollider spherecollider; //获取炸弹的物理碰撞器

	void Start () {
        //炸弹所有者由放置炸弹的玩家的脚本调用设置
        Timing = 0f;
        spherecollider = GetComponent<SphereCollider>();
	}

    private void FixedUpdate()
    {
        if (Timing >= BoomTime)
        {
            Timing = 0f;//计时器置0 防止Explode被多次调用 造成玩家多次受到伤害
            Explode(); //时间到 爆炸
            Destroy(gameObject, 0.5f); //延时销毁以播放动画
        }
        else
        {
            BoomTiming(); //时间未到 继续计时
        }

    }
    
    //单次设置炸弹拥有者
    //本函数会在炸弹创建时同时调用
    public void SetBombInfo(int PlayerIndex, float Radius)
    {
        if (!HadSetInfo)//只有在没有设置信息的情况下才允许设置
        {
            BombOwner = PlayerIndex;
            HadSetInfo = true;
            BombRadius = Radius; //设置炸弹范围
        }
    }

    //计时函数
    private void BoomTiming()
    {
            Timing += Time.deltaTime;
    }

    //本函数只会被调用一次 当玩家退出炸弹的碰撞体后激活炸弹的碰撞体 玩家不能再次进入
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spherecollider.enabled = true;//启动物理碰撞器
        }
    }

    private void Explode()
    {
        //获取爆炸范围内的所有在Attackable层的物体（包括可炸方块和玩家）
        Collider[] colliders = Physics.OverlapSphere(transform.position, BombRadius,LayerMask.GetMask("Attackable"));
        foreach(Collider hit in colliders)
        {
            if (hit.CompareTag("Player"))//如果有玩家处于爆炸范围内 造成伤害
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(BombPower, BombOwner);//传递伤害数值和伤害人
            }
            else
            {
                hit.SendMessage("InExplode");//调用获取物体的爆炸函数
                GameObject.Find(BombOwner.ToString()).GetComponent<PlayerScoreManager>().GainScore("BoxDestroy");//获得分数
            }
        }
    }

    //仅用于调试
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, BombRadius);
    }
}
