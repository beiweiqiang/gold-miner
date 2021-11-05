using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalConfig;

public class GameManager : MonoBehaviour
{
  public Text ScoreText;
  public List<GameObject> fruits = new List<GameObject>();

  static GameManager instance;

  private int _score;

  static public GameManager getInstance()
  {
    return instance;
  }

  private void Awake()
  {
    if (instance != null)
    {
      Destroy(gameObject);
    }
    instance = this;
  }

  void Start()
  {
    _score = 0;

    for (int i = 0; i < GAME_MANAGER.INIT_COUNT; i++)
    {
      SpawNewFruit();
    }
  }

  void Update()
  {
    ScoreText.text = _score.ToString("00");
  }

  public void SpawNewFruit()
  {
    int index = Random.Range(0, fruits.Count);

    Vector3 spawnPosition = generateRandomPosition();

    GameObject newFruit = Instantiate(fruits[index], spawnPosition, Quaternion.identity);
    newFruit.transform.SetParent(this.gameObject.transform);
  }

  private Vector3 generateRandomPosition() {
    float x, y;
    // -6 < y < 0
    // -4.2 < x < 10
    // 如果 x, y 在 0.8 < x < 5.3 && -0.9 < y < 0.6 之前, 重新随机

    do {
      x = Random.Range((float)-4.2, 9);
      y = Random.Range(-5f, 0);
    } while(x > 0.8 && x < 5.3 && y > -0.9 && y < 0.6);

    return new Vector3(x, y, 0);
  }

  public void AddScore(int count)
  {
    print(count);
    _score += count;
  }
}
