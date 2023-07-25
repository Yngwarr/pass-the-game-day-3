using System;
using UnityEngine;

namespace GameJamEntry.General {
	public enum MixerParamName {
		MasterVolume = 1,
		MusicVolume = 2,
		SfxVolume = 3,
	}
	
	public class SystemSettingsController {
		SystemSettingsState _state = new();
		

		public event Action<(MixerParamName paramName, float value)> OnSoundParamChanged;

		public float GetNormalizedVolume(MixerParamName mixerParamName) => _state.GetOrCreateEntry(mixerParamName).Volume;

		public void SetNormalizedVolume(MixerParamName mixerParamName, float volume) {
			var clampedVolume = Mathf.Clamp01(volume);
			_state.GetOrCreateEntry(mixerParamName).Volume = clampedVolume;
			OnSoundParamChanged?.Invoke((mixerParamName, volume));
			_state.Save();
		}
	}
}