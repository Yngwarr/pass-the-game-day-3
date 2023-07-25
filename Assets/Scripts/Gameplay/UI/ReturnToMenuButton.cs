using Com.Shelinc.SceneTransitionEffects;
using Cysharp.Threading.Tasks;
using GameComponentAttributes.Attributes;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameJamEntry.Gameplay.UI {
	public class ReturnToMenuButton : MonoBehaviour {
		[NotNullReference] [SerializeField] Button Button;
		
		[Inject]
		public void Init(SceneLoader sceneLoader) {
			Button.onClick.AddListener(() => sceneLoader.LoadScene("MainMenu").Forget());
		}
	}
}