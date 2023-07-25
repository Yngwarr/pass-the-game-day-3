using GameComponentAttributes.Attributes;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class LaserBeamView : MonoBehaviour { 
		public float MaxDistance;
		
		[NotNullReference, SerializeField] LineRenderer LineRenderer;
		[NotNullReference, SerializeField] Laser        Laser;

		Vector3 MaxEndPositions => transform.position + transform.up * MaxDistance;

		void Update() {
			if ( Laser.Status == GunStatus.Firing ) {
				ShowBeam(Laser.HitTarget);
			} else {
				HideBeam();
			}
		}
		
		
		
		public void ShowBeam(Transform endPoint) {
			LineRenderer.enabled = true;
			var endPosition = endPoint ? endPoint.position : MaxEndPositions;
			LineRenderer.SetPosition(0, transform.position);
			LineRenderer.SetPosition(1, endPosition);
		}
		
		public void HideBeam() {
			LineRenderer.enabled = false;
		}
	}
}