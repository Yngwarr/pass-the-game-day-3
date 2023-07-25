using GameJamEntry.Gameplay.WeaponsMechanic;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public class Player : MonoBehaviour {
		[SerializeField] BaseGun Gun;

		[SerializeField] float     MovementSpeed = 1f;
		[SerializeField] float     OverlapRadius = 1f;
		[SerializeField] Transform GunHolder;
		
		void Update() {
			TryPickUpWeapon();
			TryOperateGun();
			TryMove();
			TryLookToCursor();
		}

		void TryLookToCursor() {
			var dir   = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			var angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
		}

		void TryMove() {
			var move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
			transform.position += move * MovementSpeed * Time.deltaTime;
		}
		
		void TryOperateGun() {
			if ( !Gun ) {
				return;
			}
			if ( Input.GetKeyDown(KeyCode.Mouse0) ) {
				Gun.StartFire();
			}
			if ( Input.GetKeyUp(KeyCode.Mouse0) ) {
				Gun.EndFire();
			}
		}

		void TryPickUpWeapon() {
			if ( !Input.GetKeyDown(KeyCode.E) ) {
				return;
			}
			var weapons = Physics2D.OverlapCircleAll(transform.position, OverlapRadius, LayerMask.GetMask("Guns"));
			var weapon  = FindWeaponCollision(weapons);
			if ( !weapon ) {
				return;
			}
			var gunComp = weapon.GetComponent<BaseGun>();
			if ( !gunComp ) {
				return;
			}
			var newGunPos = gunComp.transform.position;
			DropOldGun(newGunPos);
			PickUpGun(gunComp);
			
		}

		void PickUpGun(BaseGun gun) {
			Gun = gun;
			Gun.transform.SetParent(GunHolder);
			Gun.transform.position = GunHolder.position;
			Gun.PickUp();
		}

		void DropOldGun(Vector3 pos) {
			// drop old gun if exists
			if ( Gun ) {
				Gun.Drop();
				Gun.transform.SetParent(null);
				Gun.transform.position = pos;
			}
		}

		BaseGun FindWeaponCollision(Collider2D[] collisions) {
			foreach ( var collision in collisions ) {
				var gunComp = collision.GetComponent<BaseGun>();
				if ( gunComp && (gunComp != Gun) ) {
					return gunComp;
				}
			}
			return null;
		}
	}
}