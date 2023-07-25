using GameComponentAttributes.Attributes;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class ChargeGunView : MonoBehaviour {
		[NotNullReference, SerializeField] Transform BulletPreview;
		[NotNullReference, SerializeField] ChargeableGun Gun;

		void Update() {
			if ( Gun.State == GunStatus.Charging ) {
				ShowChargeView(Gun.ChargeLevel);
			} else {
				HideChargeView();
			}
		}
		
		
		void ShowChargeView(float chargeLevelNormalized) {
			BulletPreview.localScale = Vector3.one * chargeLevelNormalized;
		}
		
		void HideChargeView() {
			BulletPreview.localScale = Vector3.zero;
		}
	}
}