using GameComponentAttributes.Attributes;
using UnityEngine;

namespace GameJamEntry.Gameplay.WeaponsMechanic {
	public class LaserView : MonoBehaviour {
		[NotNullReference, SerializeField] SpriteRenderer LaserSprite;
		[NotNullReference, SerializeField] Laser          Laser;

		[SerializeField] Color OverHeatColor;
		[SerializeField] Color IdleColor;
		
		void Update() {
			LaserSprite.color = Color.Lerp(IdleColor, OverHeatColor, Laser.OverheatNormalized);
		}
	}
}