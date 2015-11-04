using UnityEngine;
using System.Collections;

public class SwanSpawner : MonoBehaviour {
	#region public fields
	public Rigidbody2D swanSpawner;
	public float leftSpawnPosX;
	public float rightSpawnPosX;
	public float minSpawnPosY;
	public float maxSpawnPosY;
	public float minTimeBetweenSpawns;
	public float maxTimeBetweenSpawns;
	public float minSpeed;
	public float maxSpeed;
	#endregion
	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Today.Millisecond;
		StartCoroutine ("Spawn");
	}
	
	IEnumerator Spawn(){
		float waitTime = Random.Range (minTimeBetweenSpawns, maxTimeBetweenSpawns);
		yield return new WaitForSeconds(waitTime);

		bool facingLeft = (Random.Range (0,2) == 0); //number between min [inclusive] and max [exclusive] (Read Only). 50%
		float posX = facingLeft?rightSpawnPosX:leftSpawnPosX;
		float posY = Random.Range (minSpawnPosY,maxSpawnPosY);

		Vector3 spawnPos = new Vector3(posX,posY,transform.position.z);
		Rigidbody2D newSwan = Instantiate (swanSpawner, spawnPos, Quaternion.identity) as Rigidbody2D;
		if (!facingLeft) {
			//facing
			Vector3 scale = newSwan.transform.localScale;
			scale.x *= -1;
			newSwan.transform.localScale = scale;
		}

		//speed
		float speed = Random.Range (minSpeed, maxSpeed);
		speed *= facingLeft?-1f:1f;
		newSwan.velocity = new Vector2(speed, 0);

		StartCoroutine("Spawn");

		//clean
		while (newSwan != null) {
			float pos_x = newSwan.transform.position.x;
			if ((facingLeft && (pos_x<leftSpawnPosX - .5f))
			    || (!facingLeft && (pos_x > rightSpawnPosX + .5f))) {
				Destroy (newSwan.gameObject);
				newSwan = null;
			}
			yield return null;
		}

	}
}
