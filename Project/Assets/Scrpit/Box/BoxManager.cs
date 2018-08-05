using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour {
    public int BoxType;//仅供测试用

    private Transform trans;//方块位置 用于获取方块类型
    private GameObject Global;//目前MapManager脚本绑定在Floor上 后期可考虑改成全局单例类
    private MapManager Map;//MapManager脚本引用

    private void Start()
    {
        trans = GetComponent<Transform>();
        Global = GameObject.FindGameObjectWithTag("Global");//此处Tag后期注意修改
        Map = Global.GetComponent<MapManager>();
        setBoxType(Map); 
    }
    /*
     * 获取当前管理方块的类型
     * 具体方法为降级当前位置类型至int
     * 传递至MapManager.GetBoxType（地图信息在Awake阶段加载）
     */
    private void setBoxType(MapManager Map)
    {
       
        BoxType = Map.GetBoxType((int)trans.position.x, (int)trans.position.z);
    }

    //方块爆炸函数
    //当方块处于爆炸区域内
    private void InExplode()
    {
        if(BoxType == 1)
        {
            Destroy(gameObject);
            //TODO 爆炸粒子特效
        }
    }




}
