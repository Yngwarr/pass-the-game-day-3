using UnityEngine;

namespace GameJamEntry.Utils {
	public class BehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour {
		static T _instance;

		public static bool HasInstance => _instance;
		
		public static T Instance {
			get {
				if ( _instance ) {
					return _instance;
				}
				var obj = new GameObject($"[{typeof(T)}]");
				_instance = obj.AddComponent<T>();
				DontDestroyOnLoad(obj);
				return _instance;
			}
		}
	}
}