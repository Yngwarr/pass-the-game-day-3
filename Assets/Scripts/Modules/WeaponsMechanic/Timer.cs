using System;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class Timer {
		public bool IsActive => _action != null;
		
		Action _action;
		float  _delaySeconds;
		bool   _loop;

		float _leftTime;
		
		public float ProgressNormalized => IsActive ? (1 - _leftTime / _delaySeconds) : 0;
		
		public void Init(Action action, float delaySeconds, bool loop) {
			_action       = action;
			_delaySeconds = delaySeconds;
			_loop         = loop;
			_leftTime     = delaySeconds;
		}

		public void Deinit() {
			_action = null;
		}
		
		public void Tick(float deltaTime) {
			_leftTime -= deltaTime;
			if (_leftTime > 0) {
				return;
			}
			_action?.Invoke();
			if (!_loop) {
				Deinit();
				return;
			}
			_leftTime = _delaySeconds;
		}
	}
}