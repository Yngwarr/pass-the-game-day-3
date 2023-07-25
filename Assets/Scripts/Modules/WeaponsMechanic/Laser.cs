using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Laser : BaseGun {
		[SerializeField] float FireMaxDuration = 2f;
		[SerializeField] float ReloadTime = 1f;

		public int AmmoTotal = 3;
		public int AmmoLeft;
		

		Timer _reloadTimer = new();

		bool _isFiring;

		float _heatTime;

		public float OverheatNormalized => Mathf.Clamp01(_heatTime / FireMaxDuration);

		public GunStatus Status { get; private set; } = GunStatus.Idle;
		
		public Transform HitTarget { get; private set; }

		
		public override void PickUp() {
			AmmoLeft = AmmoTotal;
		}

		public override void Drop() {
			EndFire();
		}

		public override void StartFire() {
			if ( (Status != GunStatus.Idle) || (AmmoLeft <= 0) ) {
				return;
			}
			_isFiring = true;
			GoToFiring();
		}

		public override void EndFire() {
			Status = GunStatus.Idle;
			_reloadTimer.Deinit();
			_isFiring = false;
		}

		void Update() {
			_reloadTimer.Tick(Time.deltaTime);
			UpdateHeat();
			if ( Status == GunStatus.Firing ) {
				if (OverheatNormalized >= 1) {
					GoToReload();
				}
				TryFire();
			}
		}

		void UpdateHeat() {
			if ( Status == GunStatus.Firing ) {
				_heatTime = Mathf.Min(_heatTime + Time.deltaTime, FireMaxDuration);
			} else {
				_heatTime = Mathf.Max(_heatTime - Time.deltaTime * FireMaxDuration / ReloadTime, 0);
			}
		}

		Transform RayCastFire() {
			// TODO: add physics raycast
			return null;
		}

		void TryFire() {
			if ( Status != GunStatus.Firing ) {
				return;
			}
			HitTarget = RayCastFire();
		}
		
		void GoToIdle() {
			Status = GunStatus.Idle;
		}
		
		void GoToFiring() {
			Status = GunStatus.Firing;
		}

		#region ReloadBlock
		
		void GoToReload() {
			Status = GunStatus.ReloadMagazine;
			_reloadTimer.Init(ExitReloadState, ReloadTime, false);
		}
		
		void ExitReloadState() {
			AmmoLeft--;
			_heatTime = 0;
			if ( AmmoLeft <= 0 ) {
				EndFire();
				return;
			}
			// try to start new fire automatically 
			if ( _isFiring ) {
				GoToIdle();
				StartFire();
			}
		}

		#endregion
	}
}