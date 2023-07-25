using GameComponentAttributes.Attributes;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Pistol : BaseGun {
		[NotNullReference, SerializeField] GameObject _bulletPrefab;
		[NotNullReference, SerializeField] Transform  _bulletSpawnPoint;

		GunStatus _gunStatus = GunStatus.Idle;

		public override void PickUp() {
			// Do nothing
		}

		public override void Drop() {
			EndFire();
		}

		public override void StartFire() {
			if (_gunStatus != GunStatus.Idle) {
				return;
			}
			Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
			_gunStatus = GunStatus.ReloadBetweenShots;
		}

		public override void EndFire() {
			_gunStatus = GunStatus.Idle;
		}
	}
}