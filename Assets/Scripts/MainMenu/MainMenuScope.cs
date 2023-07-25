using Com.Shelinc.FullscreenCanvasController;
using Com.Shelinc.SceneTransitionEffects;
using Com.Shelinc.SceneTransitionEffects.Transitions;
using Com.Shelinc.SceneTransitionEffects.Transitions.Implementations;
using GameComponentAttributes.Attributes;
using GameJamEntry.General;
using GameJamEntry.MainMenu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameJamEntry.MainMenu {
	public class MainMenuScope : LifetimeScope {
		[NotNullReference] [SerializeField] ScreenManager ScreenManager;
		
		protected override void Configure(IContainerBuilder builder) {
			base.Configure(builder);
			builder.RegisterInstance(GameState.Instance.SystemSettingsController);
			builder.RegisterInstance(FadeSceneTransition.Instance).As<ISceneTransition>();
			builder.Register<SceneLoader>(Lifetime.Scoped);
			builder.RegisterInstance(ScreenManager);
			builder.Register<ScreenHelper>(Lifetime.Scoped);
			builder.RegisterEntryPoint<MainMenuStarter>();
		}
	}
}