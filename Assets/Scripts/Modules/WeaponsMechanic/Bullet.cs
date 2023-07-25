using System;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Bullet : MonoBehaviour {
		[SerializeField] float Speed = 10;
		
		void Update() {
			transform.position += transform.up * Time.deltaTime * Speed;
		}

		protected void OnCollisionEnter2D(Collision2D other) {
			Destroy(gameObject);
		}
	}
}