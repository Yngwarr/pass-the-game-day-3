namespace GameJamEntry.General {
	public class GameState {
		public SystemSettingsController SystemSettingsController = new();
		
		
		static GameState _instance;
		
		public static GameState Instance => _instance ?? (_instance = new GameState());
	}
}