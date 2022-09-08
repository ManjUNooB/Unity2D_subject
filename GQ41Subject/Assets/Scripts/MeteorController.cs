using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Rigidbody2Dがアタッチされていないときの措置
[RequireComponent(typeof(Rigidbody2D))]

public class MeteorController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody = null;

    [SerializeField] BoxCollider2D meteorCollider2D = null;

    [Min(1), Space]
    public int HP = 1;
    public float Speed = 5;

    [Min(0)]
    public int Score = 100;

    Vector3 meteorVec;
    Transform spriteTransform;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * Speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
