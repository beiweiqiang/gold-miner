using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalConfig;

public class SawSwing : MonoBehaviour
{
  public GameObject pointObject;
  public GameObject sawObject;

  // static private float _angleOffset = 0.05f;
  static private float _startAngle = Mathf.PI / 2 - SAW_SWING.ANGLE_OFFSET * Mathf.PI;
  static private float _endAngle = Mathf.PI / 2 * 3 + SAW_SWING.ANGLE_OFFSET * Mathf.PI;
  static private float _endPointX = 7.8f;
  static private float _endPointY = 5f;

  private float _distance;
  private Vector2 _center;
  private float _angle;
  private Vector2 offset;

  // true 顺时针摆动, false 逆时针摆动
  private bool _direction = true;
  // 是否正在伸缩
  private bool _stretching = false;
  // 是否在伸长, true: 伸长, false: 缩短
  private bool _elongation = true;

  private Fruit _catchingFruit;

  void Start()
  {
    _angle = _startAngle;
    _center = pointObject.transform.position;
    _distance = Vector3.Distance(pointObject.transform.position, sawObject.transform.position);
  }

  void Update()
  {
    // if (Input.touchCount > 0) {
    if (Input.GetMouseButtonDown(0) && !_stretching) {
      EnterStretch();
    }

    if (!_stretching) {
      SwingSaw();
    } else {
      Stretching();
    }

    if (_catchingFruit != null) {
      _catchingFruit.UpdatePosition(transform.position);
    }

    if (!_stretching && _catchingFruit != null) {
      _catchingFruit.destroyAndSpawNew();
      _catchingFruit = null;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other && other.CompareTag("Fruit")) {

      Fruit fruit = other.gameObject.GetComponent<Fruit>();

      // GameManager.getInstance().AddScore(fruit.getScore());
      
      // // 随机生成新的水果
      // GameManager.getInstance().SpawNewFruit();

      // if (other.gameObject != null) {
      //   Destroy(other.gameObject, 0.1f);
      // }
      
      // 往回拉
      _elongation = false;

      _catchingFruit = fruit;

      SlowdownStretchSpeed(fruit.getScore());
    }
  }

  void SlowdownStretchSpeed(int score) {
    
  }

  // 伸缩动作
  void Stretching() {
    if (_elongation) {
      offset += new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * SAW_SWING.STRETCH_SPEED;
    } else {
      offset -= new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * SAW_SWING.STRETCH_SPEED;
    }
    transform.position = _center + offset;

    float x = transform.position.x;
    float y = transform.position.y;
    if (
      _elongation && (y >= _endPointY ||
      y <= -_endPointY ||
      x >= _endPointX ||
      x <= -_endPointX)) {
        _elongation = false;
    }
    if (Vector3.Distance(pointObject.transform.position, sawObject.transform.position) <= _distance) {
      _elongation = true;
      offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _distance;
      transform.position = _center + offset;
      ExitStretch();
    }
  }

  // 进入伸缩过程
  void EnterStretch() {
    _stretching = true;
  }

  // 退出伸缩过程
  void ExitStretch() {
    _stretching = false;
  }

  // 摆动动作
  void SwingSaw() {
    if (_direction && _angle >= _endAngle) {
      _direction = false;
    }
    if (!_direction && _angle <= _startAngle) {
      _direction = true;
    }

    if (_direction) {
      _angle += SAW_SWING.ROTATE_SPEED * Time.deltaTime;
    } else {
      _angle -= SAW_SWING.ROTATE_SPEED * Time.deltaTime;
    }

    // mock
    // _angle = Mathf.PI;
    
    offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _distance;
    transform.position = _center + offset;
  }
}
