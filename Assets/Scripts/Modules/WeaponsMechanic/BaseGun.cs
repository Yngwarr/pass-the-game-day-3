using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public abstract class BaseGun : MonoBehaviour {
		public abstract void PickUp();
		
		public abstract void Drop();
		
		public abstract void StartFire();

		public abstract void EndFire();
	}
}