using GameComponentAttributes.Attributes;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class ChargeableGun : BaseGun {
		[NotNullReference, SerializeField] ChargeableBullet _bulletPrefab;
		[NotNullReference, SerializeField] Transform        _bulletSpawnPoint;

		public GunStatus State { get; private set; } = GunStatus.Idle;

		[SerializeField] float ChargeTime;

		public float ChargeLevel { get; private set; }

		public override void PickUp() {
			// Do nothing
		}

		public override void Drop() {
			EndFire();
		}

		public override void StartFire() {
			if (State != GunStatus.Idle) {
				return;
			}
			State = GunStatus.Charging;
		}

		public override void EndFire() {
			FireBullet();
			State = GunStatus.Idle;
		}

		void FireBullet() {
			var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
			bullet.Init(ChargeLevel);
		}
		
		void Update() {
			Charge();
		}

		void Charge() {
			if ( State == GunStatus.Charging ) {
				ChargeLevel = Mathf.Clamp01(ChargeLevel + Time.deltaTime / ChargeTime);
				return;
			}
			ChargeLevel = 0;
		}
	}
}