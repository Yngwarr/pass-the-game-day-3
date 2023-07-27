using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJamEntry
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource playerMusic;
        [SerializeField] AudioSource[] enemyMusic;
        int enemyCount;

        void Start()
        {
            enemyCount = enemyMusic.Length;
        }

        public void setup() {
            playerMusic.Play();
            foreach (var sound in enemyMusic) {
                sound.Play();
                sound.volume = 0;
            }
        }

        public void setPitch(float value) {
            playerMusic.pitch = value;
            foreach (var sound in enemyMusic) {
                sound.pitch = value;
            }
        }

        public void unmuteEnemy() {
            int start = Random.Range(0, enemyCount);
            for (int i = (start + 1) % enemyCount; i % enemyCount != start; i = (i + 1) % enemyCount) {
                if (enemyMusic[i].volume > 0) continue;
                enemyMusic[i].volume = 1;
                break;
            }
        }

        public void muteEnemy(int activeEnemies) {
            if (activeEnemies >= enemyMusic.Count()) return;

            int start = Random.Range(0, enemyCount);
            for (int i = (start + 1) % enemyCount; i % enemyCount != start; i = (i + 1) % enemyCount) {
                if (Mathf.Approximately(enemyMusic[i].volume, 0)) continue;
                enemyMusic[i].volume = 0;
                break;
            }
        }

        public void muteEnemies() {
            foreach (var sound in enemyMusic) {
                sound.volume = 0;
            }
        }
    }
}
