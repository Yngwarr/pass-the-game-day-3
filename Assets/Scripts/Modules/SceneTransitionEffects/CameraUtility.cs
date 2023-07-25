using System;
using UnityEngine;

namespace GameJamEntry.Utils {
	public class CameraUtility : BehaviourSingleton<CameraUtility> {
		public Camera Camera;

		public event Action<Camera> OnCameraChanged;

		protected void Start() {
			Camera = Camera.main;
		}

		protected void Update() {
			if ( Camera ) {
				return;
			}
			Camera = Camera.main;
			OnCameraChanged?.Invoke(Camera);
		}
	}
}