using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameComponentAttributes.Attributes;
using UnityEngine;

namespace Com.Shelinc.FullscreenCanvasController {
	public class ScreenManager : MonoBehaviour {
		[NotNullReference] [SerializeField] List<BaseScreen> Screens;

		bool _inTransit;

		public void Init() {
			Screens.ForEach(FirstInit);
		}

		public async UniTaskVoid ShowScreen<T>(Action<T> initAction = null) where T : BaseScreen {
			if ( _inTransit ) {
				return;
			}
			var neededScreen = Screens.Find(x => x is T) as T;
			if ( !neededScreen ) {
				return;
			}
			_inTransit = true;
			var hideTasks = Screens.Select(x => x.Hide());
			await UniTask.WhenAll(hideTasks);
			initAction?.Invoke(neededScreen);
			await neededScreen.Show();
			_inTransit = false;
		}

		void FirstInit(BaseScreen screen) {
			screen.gameObject.SetActive(true);
			screen.HideImmediately();
		}
	}
}