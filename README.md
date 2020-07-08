# unity-study
Record the unity learning process  
记录我的unity学习历程
  
roll a ball  
一个小球滚动拾取物品获取胜利的demo  
  
创建背景和游戏玩家对象：  
1.3d利用plane创建一个平面,reset 操作可以控制游戏对象初始于000坐标起点  
notice:  
+ 平面没有y轴  
+ 当y轴为负时，平面会面向反方向  

2.创建一个3d元素球：Sphere，并重置其坐标于平面之上。  
3.创建纹理-材质material ,Albedo创建一个颜色，然后改变plane组件Mesh Readerer/element 0属性  
4.旋转方向光，便于球体更好被照亮。将 light组件的transform属性y字段改成60
  
移动玩家对象，移动边界和碰撞事件:  
1.利用刚体（Rigidbody）组件实现物理效果  
2.创建并绑定script脚本于球上  
3.利用Start 和 FixedUpdate 方法  

视角跟随：  
1.摄像机调整定位和角度  
2.如果将摄像机和角色捆绑，视角将随着角色滚动而滚动  
3.摄像机跟随角色，可以用摄像机的 transform.postion 关联角色的 transform.postion（LateUpdate在每一帧update执行之后出发）  

设置场地边界：  
直接利用 cube 元素即可设置边界  

设置拾取物品：  

优化拾取物品特殊性采取以下几步=》
+ 浮空
+ 倾斜
+ 自行旋转  类似真三国无双  
+ 碰撞检测
  
旋转后的动态碰撞如果没有刚体，则会被视为静态碰撞导致不停刷新缓存   
为了避免移动刚体收到物理力影响，可以设置刚体为运动刚体

显示统计：  
创建一个UI的元素TEXT ： UI元素采取canvas的方式绘制（该方式主要是面向摄像机的）  
动态显示需要利用脚本，该类调试效果最好利用game窗口来调试  