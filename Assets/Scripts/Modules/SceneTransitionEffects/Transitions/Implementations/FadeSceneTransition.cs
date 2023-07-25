using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameComponentAttributes.Attributes;
using GameJamEntry.Utils;
using UnityEngine;

namespace Com.Shelinc.SceneTransitionEffects.Transitions.Implementations {
	public class FadeSceneTransition : SceneTransitionSingleton<FadeSceneTransition> {
		[NotNullReference] public Canvas      Canvas;
		[NotNullReference] public CanvasGroup CanvasGroup;

		protected void Start() {
			Canvas.gameObject.SetActive(false);
			Canvas.worldCamera = CameraUtility.Instance.Camera;
			CanvasGroup.alpha  = 0;
			CameraUtility.Instance.OnCameraChanged += OnCameraChanged;
		}

		protected void OnDestroy() {
			if ( CameraUtility.HasInstance ) {
				CameraUtility.Instance.OnCameraChanged -= OnCameraChanged;
			}
		}

		void OnCameraChanged(Camera cam) {
			Canvas.worldCamera = cam;
		}

		public override async UniTask HideScenes() {
			Canvas.gameObject.SetActive(true);
			await CanvasGroup.DOFade(1, 0.2f);
		}

		public override async UniTask ShowScenes() {
			await CanvasGroup.DOFade(0, 0.2f);
			Canvas.gameObject.SetActive(false);
		}
	}
}