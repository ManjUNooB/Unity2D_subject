using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Rigidbody2Dがアタッチされていないときの措置
[RequireComponent(typeof(Rigidbody2D))]

public class BulletController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody = null;
    [SerializeField] float bulletSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * bulletSpeed;
    }

	void Update()
	{
        
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log(collision.transform.tag + ": HitCollisionTag");

        if (collision.gameObject.tag == "OutOfArea")
            Destroy(this.gameObject);

        if (collision.gameObject.tag == "Enemey")
		{
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
		}
	}
 
}
