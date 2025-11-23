using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
	public GameObject enemyobj;
	float randomX;
	Vector2 spawn;
	float nextSpawn;
	public float timeForSpawn;

	private void Start() {
		nextSpawn = timeForSpawn;		
	}

	void FixedUpdate()
    {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + timeForSpawn;
			randomX = Random.Range(-8, 8);
			spawn = new Vector2(randomX, transform.position.y);
			GameObject Enemy = Instantiate(enemyobj, spawn, Quaternion.identity);
			Destroy(Enemy, 10f);
		}
    }
}
