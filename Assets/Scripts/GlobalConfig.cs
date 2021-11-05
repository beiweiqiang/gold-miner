using UnityEngine;

namespace GlobalConfig {
  class SCENCE {
    
  }

  class SAW_SWING {
    // 摆动偏移
    static private float ANGLE_OFFSET = 0.05f;
    // 默认旋转摆动的速度
    static public float ROTATE_SPEED = 1f;
    // 默认伸缩速度
    static public float STRETCH_SPEED = 0.02f;
  
    // 摆动开始的角度
    static public float START_ANGLE = Mathf.PI / 2 - ANGLE_OFFSET * Mathf.PI;
    // 摆动结束的角度
    static public float END_ANGLE = Mathf.PI / 2 * 3 + ANGLE_OFFSET * Mathf.PI;

    static public float END_POINT_X = 7.8f;
    static public float END_POINT_Y = 5f;
  }

  class FRUIT {
    
  }

  class GAME_MANAGER {
    static public int INIT_COUNT = 10;
  }
}