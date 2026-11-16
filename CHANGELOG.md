# 变更记录

本文件记录玩家与贡献者能够感知的变化。版本号以模组清单为来源，约定见[版本约定](docs/dev/versioning.md)。Release 工作流按 tag 提取对应版本段落作为发行说明。

## [Unreleased]

## [0.1.0] - 2026-11-17

首个公开版本。 / First public release.

### 新增 / Added

- 可玩角色莉奈娅，以及露米、鼓励、预知、矿石等角色机制；包含 91 张主卡、4 张生成牌、6 件遗物和 3 种药水。
- Playable Linnea with Lumi, Encouragement, Foreknowledge, and Ore mechanics: 91 main cards, 4 generated cards, 6 relics, and 3 potions.
- 3 个新事件、2 个原版事件扩展、四章世界线与 16 组先古对话；提供完整中英文本地化。
- Three new events, two vanilla event extensions, four timeline chapters, and 16 Ancient dialogue groups, with Chinese and English localization.
- 自有角色姿态、卡图与图标、能量计、四种手势和主题表现；美术素材包含 AI 辅助生成内容。
- Custom character poses, card art, icons, energy counter, four gestures, and theme assets, including AI-assisted artwork.

### 试玩反馈 / Playtest feedback

- 落实多轮 MirrorSeek 试玩反馈，调整鼓励结算、露米独立伤害、预知、矿石交互及多张卡牌的数值、升级与时点。
- Applied multiple MirrorSeek playtest passes to Encouragement, Lumi damage, Foreknowledge, Ore interactions, and card values, upgrades, and timing.
- 海螺帮的遗赠生成露米+，并在每回合开始时鼓励4。
- Conch Gang Legacy generates Lumi+ and grants Encourage 4 at the start of each turn.

### 兼容 / Compatibility

- 支持游戏 0.107.1／0.113.0，最低依赖 RitsuLib 0.6.12；工坊变体布局按当前游戏分支选择内容 DLL。
- Supports game versions 0.107.1 / 0.113.0 and requires RitsuLib 0.6.12 or newer. The variant package selects the content DLL for the active game branch.

### 验证边界 / Validation limits

- 0.113.1 独立宿主已通过角色机制、房间边界恢复与普通对局回归；0.107.1 已完成配对构建，但其实机游玩与真实双客户端联机仍待复验。
- Character mechanics, room-boundary restoration, and an ordinary run passed in an isolated 0.113.1 host. The 0.107.1 build passed compilation and packaging, while live play on that branch and real two-client multiplayer still require verification.
