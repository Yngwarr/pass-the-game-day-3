using UnityEngine;

namespace GameJamEntry.Gameplay {
	public sealed class UpdateSystem : MonoBehaviour {
		static UpdateSystem _instance;

		[SerializeField] SoundPlayer soundPlayer;
		
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
		bool _normalTime = true;

		void OnDestroy() {
			if ( _instance == this ) {
				_instance = null;
			}
		}

        void Start() {
			soundPlayer.setup();
        }

        void Update() {
			if (_normalTime) {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                soundPlayer.setPitch(1);
				return;
            }

            _timeScale = Mathf.Max(.05f, _timeScale - TimeSlowDownRate * Time.deltaTime);
			Time.timeScale = _timeScale;
			Time.fixedDeltaTime = 0.02f * _timeScale;
			soundPlayer.setPitch(_timeScale >= .85 ? 1 : _timeScale);
		}

		public void SpeedUpTime(float addTimeScale) {
			_timeScale = Mathf.Clamp01(_timeScale + addTimeScale);
		}

		public void setNormalTime(bool value) {
			_normalTime = value;
		}
	}
}