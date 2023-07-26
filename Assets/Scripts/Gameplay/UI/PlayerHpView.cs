using UnityEngine;

namespace GameJamEntry.Gameplay.UI {
	public sealed class PlayerHpView : MonoBehaviour {
		[SerializeField] Player _player;
		[Space]
		[SerializeField] RectTransform ForegroundTransform;

		void Update() {
			ForegroundTransform.anchorMax = new Vector2(Mathf.Clamp01((float)_player.Hp / _player.MaxHp), 1f);
		}
	}
}