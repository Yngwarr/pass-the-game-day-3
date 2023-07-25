using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Com.Shelinc.SceneTransitionEffects.Transitions  {
	public abstract class SceneTransitionSingleton<T> : MonoBehaviour, ISceneTransition where T : MonoBehaviour {
		static T _instance;

		public static T Instance {
			get {
				if ( _instance ) {
					return _instance;
				}
				_instance = Instantiate(Resources.Load<T>($"SceneTransitions/{typeof(T).Name}"));
				DontDestroyOnLoad(_instance.gameObject);
				return _instance;
			}
		}

		public abstract UniTask HideScenes();

		public abstract UniTask ShowScenes();
	}
}