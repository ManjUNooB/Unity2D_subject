using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//  Rigidbody2Dがアタッチされていないときの措置
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
	public bool IsActive = true;
	[Header("必要なコンポーネントを登録")]
	[SerializeField] Rigidbody2D rigidBody = null;
	[SerializeField] Transform bulletSpawn = null;
	[SerializeField] AudioSource audioSource = null;

	[Header("移動速度")]
	[SerializeField, Min(0)] float powerToMove = 10;
	[SerializeField, Min(0)] float maxPowerToMove = 15;

	[Header("射撃設定")]
	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField, Min(0)] float fireInterval = 0.5f;
	[SerializeField] AudioClip fireSE = null;

	[Header("入力設定")]
	[SerializeField] string verticalButtonName = "Vertical";
	[SerializeField] string fireButtonName = "Fire1";

	bool fire = false;
	bool firing = false;
	float forwardInput;
	Vector2 mousePos;
	Vector3 rightTop;
	Vector3 leftBottom;
	WaitForSeconds fireIntervalWait;
	Camera mainCamera;
	Transform thisTransform;
	Transform mainCameraTransform;

	VertexCollision vertexCollisionScript;


	// Start is called before the first frame update
	void Start()
	{
		//  Transformでトランスフォームを参照すると重いらしいのでキャッシュしておく
		thisTransform = transform;
		mainCamera = Camera.main;
		mainCameraTransform = mainCamera.transform;
		rightTop = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		leftBottom = mainCamera.ScreenToWorldPoint(Vector3.zero);

		vertexCollisionScript = this.gameObject.GetComponent<VertexCollision>();

		//  コルーチンの停止処理をキャッシュしておく
		//  こうするとメモリにゴミが発生しづらくなり高速化できるらしい
		fireIntervalWait = new WaitForSeconds(fireInterval);
	}

	void OnDisable()
	{
		StopCoroutine(nameof(Fire));
		firing = false;
	}

	void Update()
	{
		if (!IsActive) return;

		GetInput();

		if (fire && !firing) StartCoroutine(nameof(Fire));

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!IsActive) return;

		MovePlayer();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.tag == "Enemy")
		{
			Destroy(gameObject);
			SceneManager.LoadScene("GameOverScene");
		}
	}

	void GetInput()
	{
		//	移動
		forwardInput = Input.GetAxis(verticalButtonName);

		//	方向転換
		//	マウス座標(スクリーン座標)を取得し、ワールド座標に変換する
		Vector3 screenMousePos = Input.mousePosition;
		screenMousePos.z = mainCameraTransform.position.z;
		mousePos = mainCamera.ScreenToWorldPoint(screenMousePos);

		//	射撃
		fire = Input.GetButton(fireButtonName);
	}

	void MovePlayer()
	{
		//	マウスの方向へ向く
		thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation,
								 Quaternion.LookRotation(Vector3.forward,
								(Vector3)mousePos - thisTransform.position), 0.1f);

		//	移動
		rigidBody.AddForce(thisTransform.up * forwardInput * powerToMove * rigidBody.mass,
						   ForceMode2D.Force);
		if (transform.position.x > rightTop.x)
			transform.position = new Vector3(leftBottom.x, transform.position.y, 0);

		if (transform.position.x < leftBottom.x)
			transform.position = new Vector3(rightTop.x, transform.position.y, 0);

		if (transform.position.y > rightTop.y)
			transform.position = new Vector3(transform.position.x, leftBottom.y, 0);

		if (transform.position.y < leftBottom.y)
			transform.position = new Vector3(transform.position.x, rightTop.y, 0);
	}

	IEnumerator Fire()
	{
		firing = true;

		//　弾のゲームオブジェクトを生成
		Instantiate(bulletPrefab, bulletSpawn.position, thisTransform.rotation);

		yield return fireIntervalWait;

		firing = false;
	}
}
