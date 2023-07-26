using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Bullet : MonoBehaviour {
		[SerializeField] float Speed    = 10;
		[SerializeField] float LifeTime = 5;

		float _lifeTimer;

		void Start() {
			var rb = GetComponent<Rigidbody2D>();
			rb.velocity = transform.up * Speed;
		}

		void Update() {
			_lifeTimer += Time.deltaTime;
			if ( _lifeTimer >= LifeTime ) {
				Destroy(gameObject);
			}
		}

		protected void OnCollisionEnter2D(Collision2D other) {
			Destroy(gameObject);
			// TODO: add hit logic here
		}
	}
}