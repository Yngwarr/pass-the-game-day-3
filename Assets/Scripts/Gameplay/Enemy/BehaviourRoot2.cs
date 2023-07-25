using GameJamEntry.Gameplay.WeaponsMechanic;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public class BehaviourRoot2 : IBehaviourNode {
		Transform _transform;
		BaseGun[] _guns;

		float _angleToRotate = 0;

		float _stepRotationAngle;
		
		public BehaviourRoot2(Transform transform, BaseGun[] guns, float rotationSpeed = 60) {
			_stepRotationAngle = rotationSpeed;
			_transform         = transform;
			_guns              = guns;
		}

		public bool IsCompleted() => _angleToRotate > 360;

		public void Update() {
			var offset = _stepRotationAngle * Time.deltaTime;
			_angleToRotate += offset;
			_transform.Rotate(Vector3.forward, offset);
			foreach ( var gun in _guns ) {
				gun.StartFire();
			}
		}
	}
}