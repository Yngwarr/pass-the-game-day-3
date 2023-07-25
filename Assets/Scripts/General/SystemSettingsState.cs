using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamEntry.General {
	[Serializable]
	public class SystemSettingsState {
		public List<MixerEntry> Entries = new();

		public SystemSettingsState() {
			foreach ( MixerParamName name in Enum.GetValues(typeof(MixerParamName)) ) {
				GetOrCreateEntry(name).Volume = PlayerPrefs.GetFloat(name.ToString(), 1);
			}
		}
		
		public void Save() {
			foreach ( var entry in Entries ) {
				PlayerPrefs.SetFloat(entry.MixerParamName.ToString(), entry.Volume);
			}
		}

		public MixerEntry GetOrCreateEntry(MixerParamName mixerParamName) {
			var res = Entries.Find(x => x.MixerParamName == mixerParamName);
			if ( res != null ) {
				return res;
			}
			res = new MixerEntry(mixerParamName, 1);
			Entries.Add(res);
			return res;
		}
	}

	public class MixerEntry {
		public MixerParamName MixerParamName;
		public float     Volume;

		public MixerEntry(MixerParamName name, float volume) {
			MixerParamName = name;
			Volume    = volume;
		}
	}
}