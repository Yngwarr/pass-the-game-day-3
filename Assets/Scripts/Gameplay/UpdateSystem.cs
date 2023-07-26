using UnityEngine;

namespace GameJamEntry.Gameplay {
	public sealed class UpdateSystem : MonoBehaviour {
		static UpdateSystem _instance;
		
		public static UpdateSystem Instance {
			get {
				if ( !_instance ) {
					_instance = FindObjectOfType<UpdateSystem>();
				}
				if ( !_instance ) {
					var go = new GameObject("[UpdateSystem]");
					_instance = go.AddComponent<UpdateSystem>();
				}
				return _instance;
			}
		}

		public float TimeSlowDownRate = 1f;

		float _timeScale = 1f;

		void OnDestroy() {
			if ( _instance == this ) {
				_instance = null;
			}
		}

		void Update() {
			_timeScale          = Mathf.Max(0f, _timeScale - TimeSlowDownRate * Time.deltaTime);
			Time.timeScale      = _timeScale;
			Time.fixedDeltaTime = 0.02f * _timeScale;
		}

		public void SpeedUpTime(float addTimeScale) {
			_timeScale = Mathf.Clamp01(_timeScale + addTimeScale);
		}
	}
}