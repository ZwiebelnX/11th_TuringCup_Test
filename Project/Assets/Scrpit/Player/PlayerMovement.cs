using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 6.0f;//角色移动的速度

    private Rigidbody playerRigid;//角色刚体
    private Vector3 Position;//玩家现在所处位置
    

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>(); //获取玩家刚体以操控角色
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
    }

    //玩家移动,h控制东西，v控制南北
    private void Move(float h, float v)
    {
        Vector3 movement = new Vector3();
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        playerRigid.MovePosition(transform.position + movement);
    }


}
