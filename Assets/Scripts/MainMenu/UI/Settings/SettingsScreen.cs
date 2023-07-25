using Com.Shelinc.FullscreenCanvasController;
using GameComponentAttributes.Attributes;
using GameJamEntry.General;
using GameJamEntry.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamEntry.MainMenu.UI.Settings {
	public class SettingsScreen : BaseScreen {
		[NotNullReference] [SerializeField] Button             ReturnButton;
		[NotNullReference] [SerializeField] SoundSettingsBlock MasterSettingsBlocks;
		[NotNullReference] [SerializeField] SoundSettingsBlock MusicSettingsBlocks;
		[NotNullReference] [SerializeField] SoundSettingsBlock SfxSettingsBlocks;
		
		public void Init(ScreenHelper helper, SystemSettingsController settingsController) {
			ReturnButton.RemoveAllAndAddListener(helper.ShowMainMenuScreen);
			MasterSettingsBlocks.Init(settingsController, MixerParamName.MasterVolume);
			MusicSettingsBlocks.Init(settingsController, MixerParamName.MusicVolume);
			SfxSettingsBlocks.Init(settingsController, MixerParamName.SfxVolume);
		}
	}
}