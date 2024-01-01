# AdventureProject

## ChapterOne瓦片地图

### 1.功能

> 1.将一些地图素材导入瓦片地图中，使用了Unity中RuleTile的功能，方便地图的绘制。
>
> 2.素材中有一些瀑布的地图素材，使用Unity的AnimatedTile功能，让瀑布流畅的运动起来。

### 2.方法

#### 2.1瓦片地图组件位置

> 1.瓦片地图的组件在unity的Windows--->2D--->TilePalette。

#### 2.2创建画布

>在TilePalette创建地图素材的画布
>
>将地图素材的SpriteMode从single改为multiple。
>
>Pixels Per Unit 改为16。
>
>filterMode改为Point。
>
>Compression改为None。
>
>注意：
>
>1.Unity2022.3.4f1版本可能会出现ArgumentException: Unable to set invalid palette错误，关闭重启可以解决。
>
>2.不同的素材有不同的修改，仅供参考
>
>3.创建画布会选择文件夹，TileMap--->Palette,创建tile文件夹然后再创建Palette文件夹存放画布

#### 2.3分割图片素材

> 1.点Sprite Editor--->slice--->type--->Grid by cell size--->apply.
>
> 2.将图片素材拖入创建好的Palette中。
>
> 注意：
>
> 1.分割地图的重点在center，分割人物和敌人的中心点是bottom。
>
> 2.拖入素材会让你再次创建文件夹，TileMap-->Tiles-->Forest。把素材保存到Forest文件夹中。

#### 2.4绘制瓦片地图

>1.在Hierarchy中创建2d Object -->TileMap-->Rectangular

#### 2.5规则瓦片

> 1.在Tile-->Tiles文件夹中创建RuleTile文件夹存放RuleTile。
>
> 2.Great-->2D-->Tiles--->RuleTile
>
> 3.设置好之后将RuleTile拖入Palette中。

![image-20231226173233714](C:\Users\15302\AppData\Roaming\Typora\typora-user-images\image-20231226173233714.png)