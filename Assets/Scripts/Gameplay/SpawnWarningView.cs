using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameJamEntry.Gameplay {
	public sealed class SpawnWarningView : MonoBehaviour {
		[SerializeField] Transform _animTransform;

		public async UniTask Show(float time) {
			_animTransform.localScale = Vector3.zero;
			await _animTransform
				.DOScale(Vector3.one, time)
				.SetEase(Ease.Linear);
		}
	}
}