using UnityEngine;

namespace GlobalConfig {
  class SCENCE {

    // 场景边界
    static public float BOUNDARY_END_X = 8f;
    static public float BOUNDARY_START_X = -8f;
    static public float BOUNDARY_END_Y = 5f;
    static public float BOUNDARY_START_Y = -5f;
  }

  class SAW_SWING {
    // 摆动偏移
    static private float ANGLE_OFFSET = 0.05f;
    // 默认旋转摆动的速度
    static public float ROTATE_SPEED = 1f;
    // 默认伸缩速度
    static public float STRETCH_SPEED = 0.04f;
  
    // 摆动开始的角度
    static public float START_ANGLE = Mathf.PI / 2 - ANGLE_OFFSET * Mathf.PI;
    // 摆动结束的角度
    static public float END_ANGLE = Mathf.PI / 2 * 3 + ANGLE_OFFSET * Mathf.PI;
  }

  class FRUIT {
    
  }

  class GAME_MANAGER {
    static public int INIT_COUNT = 10;

    // 随机生成算法:
    // - 定义一个行列表格, 随机生成在其中的格子内
    // - 基于格子中心位置, 有一定的偏移
    static public int ROW_COUNT = 10;
    static public int COL_COUNT = 10;
  }
}