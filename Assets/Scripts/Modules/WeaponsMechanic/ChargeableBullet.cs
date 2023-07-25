using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class ChargeableBullet : Bullet {
		[SerializeField] float MinSize;
		[SerializeField] float MaxSize;
		
		public void Init(float chargeLevel) {
			var size = Mathf.Lerp(MinSize, MaxSize, chargeLevel);
			transform.localScale = Vector3.one * size;
		}
	}
}