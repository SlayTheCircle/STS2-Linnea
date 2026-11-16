# 莉奈娅角色资源与动作

角色资产由 [LinneaCharacterAssets](../../../src/STS2-Linnea/Content/Characters/LinneaCharacterAssets.cs) 注册：战斗、休息点、商店、能量计、选人背景／头像、地图标记、拖尾与四种手势均有自有资源路径。未交付专属音频，选人使用原版通用点击音效，其他反馈沿用已有通用音效。清除借用的 Spine 档案后，角色通过 `IModCreatureCombatAnimationStateMachineFactory` 创建自己的动画状态机。

## 姿态动画

内层 `rig/linnea_combat.tscn` 使用原稿 PNG 姿态、Sprite2D 与 AnimationPlayer。它以切换姿态配合短位移、受击闪色和待机起伏呈现动作；没有将整张 PNG 声称为骨骼动画。战斗外壳提供原生 NCreatureVisuals 所需的标记节点。

| 原生触发 | 状态与结束行为 |
|---|---|
| Attack | 攻击姿态，完成后返回当前适用待机 |
| Cast | 技能姿态，完成后返回待机 |
| Hit | 受击位移与闪色，完成后恢复 |
| Dead | 倒下并保持，普通动作不能覆盖 |
| Revive | 复位姿态，期间保护动作，完成后待机 |

原生露米牌的攻击根据本角色最近一次出牌历史选择 `lumi_attack`；普通攻击继续使用 `attack`。

[LinneaCombatVisuals](../../../src/STS2-Linnea/Content/Visuals/LinneaCombatVisuals.cs) 按视觉实例保存状态机，在 TreeExiting 时释放状态机和 backend。动作仅修改内部 Body；游戏拥有的 `%Visuals` 朝向符号由外部保留。完整素材契约见 [validation.json](../../../assets/validation.json)，输入映射见 [mappings.sh](../../../scripts/art/mappings.sh)。

## 验证边界

两角色已在实际 0.111.0 图形宿主完成共60项检查，包含原生 NCreature 触发、死亡保持、复活保护、朝向、真实露米出牌／凭依Power、休息点／商店，以及回到第二场战斗。标准版 Godot 的 PCK 验证与双目标编译单独执行；这些证据不代表网络多人、自然时间线揭示或完整局验收。当前状态统一见 [STATUS](../../../STATUS.md)。
