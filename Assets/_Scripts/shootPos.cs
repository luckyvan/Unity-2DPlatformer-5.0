using UnityEngine;
using System.Collections;

public class shootPos : MonoBehaviour {
	public Rigidbody2D rocket;
	public float speed = 20f;

	private PlayerController playerController;
	private Animator anim;
	// Use this for initialization
	void Start () {
		playerController = transform.root.GetComponent<PlayerController>();
		anim = transform.root.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			anim.SetTrigger ("Shoot");
			Quaternion bulletDirection;
			float bulletSpeed;
			if (playerController.facingRight) {
				bulletDirection = Quaternion.Euler (new Vector3(0, 0, 0));
				bulletSpeed = speed;
			}else{
				bulletDirection = Quaternion.Euler (new Vector3(0, 0, 180));
				bulletSpeed = -speed;
			}

			Rigidbody2D bulletInstance = Instantiate (rocket, transform.position, bulletDirection) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed, 0);
		}
	}
}
