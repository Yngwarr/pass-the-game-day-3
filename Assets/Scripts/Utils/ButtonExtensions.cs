using System;
using UnityEngine.UI;

namespace GameJamEntry.Utils {
	public static class ButtonExtensions {
		public static void RemoveAllAndAddListener(this Button button, Action callback) {
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => callback?.Invoke());
		}
	}
}