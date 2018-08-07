using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public int Damage = 15;// 可调节伤害
    public float TimeBetweenBullets = 0.5f;
    public float range = 100f;
    public float EffectsDisplayTime = 0.2f;

    float Timing;
    Ray ShootRay;
    RaycastHit ShootHit;
    int ShootableMask;
    LineRenderer GunLine;


    // Use this for initialization
    void Start () {
        ShootableMask = LayerMask.GetMask("Attackable");
        GunLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Timing += Time.deltaTime;

	}
}
