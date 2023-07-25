using System.Collections.Generic;

namespace GameJamEntry.Utils {
	public static class RandomUtils {
		public static T GetRandomElementInList<T>(List<T> values) {
			if ( values.Count == 0 ) {
				return default;
			}
			var randomIndex = UnityEngine.Random.Range(0, values.Count);
			return values[randomIndex];
		}
	}
}