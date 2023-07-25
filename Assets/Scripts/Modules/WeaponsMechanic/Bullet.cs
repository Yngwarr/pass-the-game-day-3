using System;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Bullet : MonoBehaviour {
		[SerializeField] float Speed = 10;

		void Start() {
			var rb = GetComponent<Rigidbody2D>();
			rb.velocity = transform.up * Speed;
		}

		protected void OnCollisionEnter2D(Collision2D other) {
			Destroy(gameObject);
			// TODO: add hit logic here
		}
	}
}