using Com.Shelinc.FullscreenCanvasController;
using GameJamEntry.Gameplay.UI;
using VContainer.Unity;

namespace GameJamEntry.Gameplay {
	public class GameplayStarter : IStartable {
		ScreenManager _screenManager;
		
		public GameplayStarter(ScreenManager screenManager) => _screenManager = screenManager;

		public void Start() {
			_screenManager.ShowScreen<GameplayScreen>().Forget();
		}
	}
}