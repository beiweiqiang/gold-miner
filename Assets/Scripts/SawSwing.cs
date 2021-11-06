using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalConfig;

public class SawSwing : MonoBehaviour
{
  public GameObject pointObject;
  public GameObject sawObject;

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
  private float _stretchSpeed = SAW_SWING.STRETCH_SPEED;

  private Fruit _catchingFruit;

  void Start()
  {
    _angle = SAW_SWING.START_ANGLE;
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
      ResetStretchSpeed();
      _catchingFruit.destroyAndSpawNew();
      _catchingFruit = null;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other && other.CompareTag("Fruit")) {
      Fruit fruit = other.gameObject.GetComponent<Fruit>();

      // 往回拉
      _elongation = false;

      _catchingFruit = fruit;

      SlowdownStretchSpeed(fruit.getWeight());
    }
  }

  // 按一定规则减缓伸缩速度
  void SlowdownStretchSpeed(int weight) {
    _stretchSpeed *= 1f / ((float)weight + 1f);
  }

  // 恢复伸缩速度
  void ResetStretchSpeed() {
    _stretchSpeed = SAW_SWING.STRETCH_SPEED;
  }

  // 伸缩动作
  void Stretching() {
    if (_elongation) {
      offset += new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _stretchSpeed;
    } else {
      offset -= new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _stretchSpeed;
    }
    transform.position = _center + offset;

    float x = transform.position.x;
    float y = transform.position.y;
    
    // 边界测定
    if (
      _elongation && (
        y >= SCENCE.BOUNDARY_END_Y ||
        y <= SCENCE.BOUNDARY_START_Y ||
        x >= SCENCE.BOUNDARY_END_X ||
        x <= SCENCE.BOUNDARY_START_X
      )) {
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
    if (_direction && _angle >= SAW_SWING.END_ANGLE) {
      _direction = false;
    }
    if (!_direction && _angle <= SAW_SWING.START_ANGLE) {
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
