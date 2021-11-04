using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
  LineRenderer line;
  public Transform startPoint;
  public Transform endPoint;

  void Start()
  {
    line = GetComponent<LineRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    line.SetPosition(0, endPoint.position);
    line.SetPosition(1, startPoint.position);
  }
}
