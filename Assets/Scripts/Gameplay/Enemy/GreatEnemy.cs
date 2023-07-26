using GameComponentAttributes.Attributes;
using GameJamEntry.Gameplay.WeaponsMechanic;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public class GreatEnemy : MonoBehaviour {
		[NotNullReference] public BaseGun[] Guns;

		IBehaviourNode _activeBehaviour;

		int _iteration = 0;
		
		protected void Start() {
			_activeBehaviour = new BehaviourRoot1(transform, Guns);
		}
		
		protected void Update() {
			if ( _activeBehaviour == null ) {
				return;
			}
			_activeBehaviour.Update();
			if ( !_activeBehaviour.IsCompleted() ) {
				return;
			}
			_activeBehaviour = _iteration switch {
				0 => new BehaviourRoot1(transform, Guns),
				1 => new BehaviourRoot2(transform, Guns),
				2 => new BehaviourRoot2(transform, Guns, 120),
				3 => new BehaviourRoot1(transform, Guns, 120),
				4 => new BehaviourRoot2(transform, Guns, 90),
				5 => new BehaviourRoot1(transform, Guns, 900),
				_ => null
			};
			_iteration = (_iteration + 1) % 6;
		}

		void OnCollisionEnter2D(Collision2D other) {
			if ( other.gameObject.GetComponent<Bullet>() ) {
				Destroy(gameObject);
			}
		}
	}
}