using Cysharp.Threading.Tasks;

namespace Com.Shelinc.SceneTransitionEffects.Transitions.Implementations  {
	public class FakeSceneTransition : SceneTransitionSingleton<FakeSceneTransition> {
		public override UniTask HideScenes() => UniTask.CompletedTask;

		public override UniTask ShowScenes() => UniTask.CompletedTask;
	}
}