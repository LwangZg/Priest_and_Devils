##### 游戏对象运动的本质

​	游戏对象运动的本质是对象Transform属性的变化，position决定位置，rotation决定旋转角度。



#####请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）

​	第一种方法是为this.transform.position加上两个方向上的向量值，两个向量叠加从而形成抛物线运动。

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private int force = 1;
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * Time.deltaTime * (force / 10);
        this.transform.position += Vector3.right * Time.deltaTime * 4;
        force++;
	}
}

```

​	第二种是通过创建一个Vector3变量，并且在没帧都让物体的transform.position加上这个变量。

```c#
public class NewBehaviourScript : MonoBehaviour {

    private int force = 1;
	// Update is called once per frame
	void Update () {
        Vector3 drop = new Vector3(Time.deltaTime * 4, -Time.deltaTime * (force / 10), 0);
        this.transform.position += drop;
        force++;
	}
}
```

​	第三种是用translate方法改变物体位置得到，translate方法接收一个Vector3的参数。

```c#
public class NewBehaviourScript : MonoBehaviour {

    private int force = 1;
	// Update is called once per frame
	void Update () {
        Vector3 drop = new Vector3(Time.deltaTime * 4, -Time.deltaTime * (force / 10), 0);
        this.transform.Translate(drop);
        force++;
	}
}
```



##### 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。

​	创建10个sphere物体，让所有星球都在同一X轴上,目的是为了使球体旋转法线为(0, Y, Z)，		只要Y/Z的值不同，旋转法线就不相同，则行星不在同一法平面上。

​	调节每个球体的scale改变大小。

​	将星球的图片直接拖到物体上。

![](C:\Users\79860\Desktop\TIM截图20180402173227.png)



​	以下为公转代码，定义一个public Transform对象sun，sun为公转的对象。speed设置为public对象，便于为不同的球体设置不同的速度。让变量y,z为随机数，使得每个球体y/z不同，则法平面不同。

```c#
public class Rotate : MonoBehaviour {

    public Transform sun;
    public float speed = 40;
    float y, z;

    void Start()
    {
        y = Random.Range(1, 360);
        z = Random.Range(1, 60);
    }

    void Update()
    {
        Vector3 axis = new Vector3(0, y, z);
        this.transform.RotateAround(sun.position, axis, speed * Time.deltaTime);
    }
}
```

​	如果要显示行星运行的轨迹，则要为每个行星添加trailer render的组件。其中Time为轨迹持续时间，Width为轨迹粗细程度。

![](C:\Users\79860\Desktop\TIM截图20180402174907.png)