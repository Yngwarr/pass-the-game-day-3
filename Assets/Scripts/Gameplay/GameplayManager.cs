using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameJamEntry.Gameplay {
	public sealed class GameplayManager : MonoBehaviour {
		enum Mode {
			Idle,
			WaveTimer,
			Spawn,
		}
		
		[SerializeField] float _waveInterval             = 5f;
		[SerializeField] float _waveIntervalDecrease     = 0.1f;
		[SerializeField] float _waveIntervalMin          = 0.5f;
		[SerializeField] float _spawnInterval            = 0.5f;
		[SerializeField] float _spawnIntervalDecrease    = 0.1f;
		[SerializeField] float _spawnIntervalMin         = 0f;
		[SerializeField] float _spawnWarningTime         = 1f;
		[SerializeField] float _spawnWarningTimeDecrease = 0.1f;
		[SerializeField] float _spawnWarningTimeMin      = 0.1f;
		[SerializeField] float _playZoneRadius           = 35 / 2f;
		[Space]
		[SerializeField] Player _player;
		[Space]
		[SerializeField] SpawnWarningView _spawnWarningViewPrefab;
		[SerializeField] GameObject[] _enemyPrefabs = {};

		readonly List<GameObject> _activeEnemies = new();

		float _waveTimer;

		int _waveEnemiesCount = 1;

		Mode _mode = Mode.Idle;

		void Update() {
			if ( !_player.IsAlive ) {
				return;
			}
			switch ( _mode ) {
				case Mode.Idle: {
					if ( _activeEnemies.Count > 0 ) {
						for ( var i = _activeEnemies.Count - 1; i >= 0; i-- ) {
							if ( !_activeEnemies[i] ) {
								_activeEnemies.RemoveAt(i);
							}
						}
					}
					if ( _activeEnemies.Count == 0 ) {
						RestartWaveTimer();
						_mode = Mode.WaveTimer;
					}
					break;
				}
				case Mode.WaveTimer: {
					_waveTimer -= Time.deltaTime;
					if ( _waveTimer <= 0 ) {
						_mode = Mode.Spawn;
						SpawnWave().Forget();
					}
					break;
				}
				case Mode.Spawn: {
					break;
				}
			}
		}

		void RestartWaveTimer() {
			_waveTimer = _waveInterval;
		}

		async UniTaskVoid SpawnWave() {
			var tasks = new UniTask[_waveEnemiesCount];
			for ( var i = 0; i < _waveEnemiesCount; i++ ) {
				await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval));
				tasks[i] = SpawnEnemy();
			}
			_waveEnemiesCount++;
			_waveInterval     = Mathf.Max(_waveIntervalMin, _waveInterval - _waveIntervalDecrease);
			_spawnInterval    = Mathf.Max(_spawnIntervalMin, _spawnInterval - _spawnIntervalDecrease);
			_spawnWarningTime = Mathf.Max(_spawnWarningTimeMin, _spawnWarningTime - _spawnWarningTimeDecrease);

			await UniTask.WhenAll(tasks);
			_mode = Mode.Idle;
		}

		Vector3 GetRandomSpawnPosition() => Random.insideUnitCircle.normalized * Random.Range(0, _playZoneRadius);

		async UniTask SpawnEnemy() {
			var spawnPosition = GetRandomSpawnPosition();
			var spawnWarningView = Instantiate(_spawnWarningViewPrefab, spawnPosition, Quaternion.identity);
			await spawnWarningView.Show(_spawnWarningTime);
			Destroy(spawnWarningView.gameObject);
			var enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
			_activeEnemies.Add(enemy);
		}
	}
}