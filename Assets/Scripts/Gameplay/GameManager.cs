using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {  
    public static GameManager instance;
    public static int level=1;

    public Text levelTxt;
    public GameObject ui;
    public int enemies = 2;
    public int remainingEnemies = 2;
    public Enemy[] enemyPrefabs;
    public List<Transform> enemySpawnPoints;
    ShufflerQueue<Transform> spawnPointsQueue;


    private void Awake() {
        instance = this;
        levelTxt.text = "" + level;

        enemies = Mathf.FloorToInt(Mathf.Lerp(1, enemySpawnPoints.Count + 2, level / 10f));
        enemies = Mathf.Clamp(enemies, 1, enemySpawnPoints.Count);
        remainingEnemies = enemies;

        spawnPointsQueue = new ShufflerQueue<Transform>(enemySpawnPoints);
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(.5f);
        ui.SetActive(true);
        for(int i=0; i< enemies; i++) {
            var spawnPoint = spawnPointsQueue.next;
            var foe = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            foe.left = spawnPoint.GetChild(0);
            foe.right = spawnPoint.GetChild(1);
            foe.transform.position = spawnPoint.position;
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(1);
        }
	}

    public void Die() {
        print("game over");
        level = 1;
        SceneManager.LoadScene(0);
    }

    public void EnemyDeath() {
        print("EnemyDeath over");
        if(--remainingEnemies <= 0) {
            //win
            level++;
            SceneManager.LoadScene(1);
        }
    }
}
