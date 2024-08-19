## 帧同步框架

使用库：Mirror

Unity版本：2021.3.13f1

### 登陆验证
同号登录会顶掉之前的客户端

ServerPlayerSystem.TryAddPlayer()


### 帧率控制
逻辑帧间隔：ConstVariables.LogicFrameIntervalSeconds, 默认值：1/15f，即 1 秒跑 15 个逻辑帧

ServerTimerSystem

ClientTimerSystem

### 指令同步
每过多少帧进行一次指令集合广播：ConstVariables.CommandSetSyncIntervalFrames, 默认值：5

ServerCommandSyncSystem.SyncCommands()

ClientFrameSyncSystem.OnSyncCommands()

### 加速追帧
追帧时一秒追多少逻辑帧：ConstVariables.CommandChasingPerFrame, 默认值：100

ClientChasingFrameSystem

GameHelper_Client.ChasingOneFrame() : 这里跑一次逻辑帧的流程

### 断线重连
ServerCommandStorageSystem.SyncAllCommands()

ClientFrameSyncSystem.OnSyncCommandsAll()

### 指令逻辑层快照
功能开关：GameFacade.enableCommandSnapshot, 默认开

快照文件位置：PersistentDataPath/

文件名为配置项：GameFacade.commandSnapshotLogName, 默认值："commandSnapshot-启动时间.log"

![image](https://github.com/user-attachments/assets/08a833ae-c818-4fbd-a609-0bbe6d1b3b10)


快照可以在逻辑帧变动时/指令执行前后，记录战斗状态

可以用diff工具快速定位到不一致的位置

![image](https://github.com/user-attachments/assets/744b0c80-ee9b-4148-9105-ff047643a919)
![image](https://github.com/user-attachments/assets/ad57e756-9ece-4c79-93fd-d6e14d3f0e8d)


### GC控制
过程中协议全部使用 struct，容器均已池化


### TODO：
同步全部指令时，控制包体大小、分包

输出log加缓存区，控制时机写入
