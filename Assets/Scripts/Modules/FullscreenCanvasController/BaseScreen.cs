using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameComponentAttributes.Attributes;
using UnityEngine;

namespace Com.Shelinc.FullscreenCanvasController {
	public class BaseScreen : MonoBehaviour {
		[Header("transition params")]
		[NotNullReference] public CanvasGroup CanvasGroup;

		public async UniTask Show() {
			CanvasGroup.blocksRaycasts = false;
			await CanvasGroup.DOFade(1, 0.3f);
			CanvasGroup.blocksRaycasts = true;
		}

		public async UniTask Hide() {
			CanvasGroup.blocksRaycasts = true;
			await CanvasGroup.DOFade(0, 0.3f);
			CanvasGroup.blocksRaycasts = false;
		}
		
		public void HideImmediately() {
			CanvasGroup.blocksRaycasts = false;
			CanvasGroup.alpha = 0;
		}
	}
}