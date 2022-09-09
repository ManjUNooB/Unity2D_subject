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

    SpriteRenderer meteorSpriteRenderer;

    Vector3 meteorVec;
    Transform spriteTransform;
    List<Vector3> meteorVertex;
    Vector3 topLeft;
    Vector3 topRight;
    Vector3 bottomLeft;
    Vector3 bottomRight;
    Matrix4x4 spriteMatrix;
    float halfX = 0;
    float halfY = 0;

    [SerializeField] GameObject playerObj;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * Speed;

        meteorSpriteRenderer = GetComponent<SpriteRenderer>();
        spriteMatrix = meteorSpriteRenderer.localToWorldMatrix;
        halfX = meteorSpriteRenderer.bounds.extents.x;
        halfY = meteorSpriteRenderer.bounds.extents.y;

        playerController = playerObj.GetComponent<PlayerController>();

    }
    // Update is called once per frame
    void Update()
    {
        spriteMatrix = meteorSpriteRenderer.localToWorldMatrix;
        halfX = meteorSpriteRenderer.bounds.extents.x;
        halfY = meteorSpriteRenderer.bounds.extents.y;

        topLeft = SpriteVertex(-halfX, halfY);
        topRight = SpriteVertex(halfX * 2, halfY);
        bottomLeft = SpriteVertex(-halfX, 0);
        bottomRight = SpriteVertex(halfX * 2, 0);

        meteorVertex = SpriteVertexList(topLeft, topRight, bottomLeft, bottomRight);

        if (playerController.SelfCollision(meteorVertex))
		{
            Destroy(gameObject);
		}
    }

    Vector3 SpriteVertex(float X,float Y)
	{
        return spriteMatrix.MultiplyPoint3x4(new Vector3(X,Y,0));
    }

    List<Vector3> SpriteVertexList(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight)
    {
        List<Vector3> Vector3s = new List<Vector3>();
        Vector3s.Add(topLeft);
        Vector3s.Add(topRight);
        Vector3s.Add(bottomLeft);
        Vector3s.Add(bottomRight);

        return Vector3s;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.transform.tag == "OutOfArea")
		{
            Destroy(gameObject);
		}
	}
}
