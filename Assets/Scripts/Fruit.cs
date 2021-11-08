using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
  private int score;
  private CircleCollider2D cc;
  private int _weight;

  void Start()
  {
    cc = GetComponent<CircleCollider2D>();
    score = Random.Range(1, 10);
    _weight = score;
    float bigger = (float) score / 10f;
    transform.localScale += new Vector3(bigger, bigger, 0);
    cc.radius *= bigger;
  }

  public virtual int getScore() {
    return score;
  }

  public int getWeight() {
    return _weight;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdatePosition(Vector3 v) {
    transform.position = v;
  }

  public void destroyAndSpawNew() {
    cc.enabled = false;
    Destroy(gameObject, 0.1f);
    GameManager.getInstance().AddScore(getScore());
    GameManager.getInstance().SpawNewFruit();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.name == "Saw") {
      // 被抓住
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.name == "Saw")
    {

      // Destroy(gameObject, 0.1f);

      // GameManager.getInstance().AddScore(1);
      // 随机生成新的水果
      // GameManager.getInstance().SpawNewFruit();
    }
  }
}
