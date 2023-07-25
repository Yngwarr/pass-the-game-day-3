using System;
using GameComponentAttributes.Attributes;
using GameJamEntry.General;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace GameJamEntry.MainMenu {
	public class SoundHelper : MonoBehaviour, IDisposable {
		[NotNullReference] [SerializeField] AudioMixer Mixer;

		SystemSettingsController _systemSettingsController;
		
		[Inject]
		public void Init(SystemSettingsController settingsController) {
			_systemSettingsController                     =  settingsController;
			_systemSettingsController.OnSoundParamChanged += OnSoundParamChanged;
			UpdateValues();
		}

		public void Dispose() {
			if ( _systemSettingsController == null ) {
				return;
			}
			_systemSettingsController.OnSoundParamChanged -= OnSoundParamChanged;
		}
		
		void OnSoundParamChanged((MixerParamName name, float value) paramChanged) {
			UpdateValues();
		}
		
		void UpdateValues() {
			foreach ( MixerParamName mixerParam in Enum.GetValues(typeof(MixerParamName)) ) {
				var normalizedVolume = _systemSettingsController.GetNormalizedVolume(mixerParam);
				var volume           = GetAbsoluteVolume(normalizedVolume);
				Mixer.SetFloat(mixerParam.ToString(), volume);
			}
		}

		float GetAbsoluteVolume(float normalizedVolume) {
			var minVolume = -80;
			var maxVolume = 20;
			return normalizedVolume * (maxVolume - minVolume) + minVolume;
		}
	}
}