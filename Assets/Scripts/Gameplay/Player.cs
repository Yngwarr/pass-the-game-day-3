using GameJamEntry.Gameplay.WeaponsMechanic;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public class Player : MonoBehaviour {
		[SerializeField] int StartHp = 100;
		[Space]
		[SerializeField] float     PlayZoneRadius = 35f;
		[SerializeField] float     MovementSpeed = 1f;
		[SerializeField] float     OverlapRadius = 1f;
		[SerializeField] BaseGun   Gun;
		[SerializeField] Transform GunHolder;

		Camera _camera;
		
		Vector3 _mousePosition;

		public int MaxHp => StartHp;
		public int Hp    { get; private set; }
		
		public bool IsAlive => Hp > 0;

		void Start() {
			_camera        = Camera.main;
			_mousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);

			Hp = StartHp;
		}

		void Update() {
			TryPickUpWeapon();
			TryOperateGun();
			TryMove();
			TryLookToCursor();
		}

		void OnCollisionEnter2D(Collision2D other) {
			if ( other.gameObject.GetComponent<Bullet>() ) {
				Hp = Mathf.Max(0, Hp - 10);
				if ( Hp == 0 ) {
					enabled = false;
					Debug.Log("You died");
					// TODO: die, return to the main menu
				}
			}
		}

		void TryLookToCursor() {
			var dir   = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
			var angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
			
			var mousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
			if ( _mousePosition != mousePosition ) {
				UpdateSystem.Instance.SpeedUpTime(Vector2.Distance(mousePosition, _mousePosition) * 0.1f);
				_mousePosition = mousePosition;
			}
		}

		void TryMove() {
			var move        = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
			var oldPosition = transform.position;
			var newPosition = oldPosition + move * (MovementSpeed * Time.deltaTime);
			if ( newPosition.magnitude > PlayZoneRadius ) {
				newPosition = newPosition.normalized * PlayZoneRadius;
			}
			transform.position = newPosition;
			if ( oldPosition != newPosition ) {
				UpdateSystem.Instance.SpeedUpTime(0.1f);
			}
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
			if ( Input.GetKey(KeyCode.Mouse0) ) {
				UpdateSystem.Instance.SpeedUpTime(0.2f);
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