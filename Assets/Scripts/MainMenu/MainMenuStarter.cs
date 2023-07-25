using GameJamEntry.MainMenu.UI;
using VContainer;
using VContainer.Unity;

namespace GameJamEntry.MainMenu {
	public class MainMenuStarter : IStartable {
		ScreenHelper _screenHelper;
		
		[Inject]
		public MainMenuStarter(ScreenHelper screenHelper) => _screenHelper = screenHelper;

		public void Start() {
			_screenHelper.ShowMainMenuScreen();
		}
	}
}