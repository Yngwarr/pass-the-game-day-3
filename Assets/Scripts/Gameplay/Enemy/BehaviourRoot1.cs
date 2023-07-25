using GameJamEntry.Gameplay.WeaponsMechanic;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public class BehaviourRoot1 : IBehaviourNode {
		Transform _transform;
		BaseGun[] _guns;

		float _angleToRotate = 360;

		float _stepRotationAngle;
		
		public BehaviourRoot1(Transform transform, BaseGun[] guns, float rotationSpeed = 60f) {
			_transform         = transform;
			_guns              = guns;
			_stepRotationAngle = rotationSpeed;
		}

		public bool IsCompleted() => _angleToRotate < 0;

		public void Update() {
			var offset = _stepRotationAngle * Time.deltaTime;
			_angleToRotate -= offset;
			_transform.Rotate(Vector3.forward, -offset);
			foreach ( var gun in _guns ) {
				gun.StartFire();
			}
		}
	}
}