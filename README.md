# STS2-Linnea · 莉奈娅

[English](README.en.md)

《杀戮尖塔 2》的可玩角色模组「莉奈娅」，围绕露米、鼓励、预知与矿石构筑。支持游戏 0.107.1（稳定版）与 0.113.0（测试版）双分支。当前实现与限制见 [STATUS.md](STATUS.md)。

## 获取与安装

当前版本为 **0.1.0**。从 [GitHub Releases](https://github.com/SlayTheCircle/STS2-Linnea/releases) 下载对应游戏目标的安装包，将其中 `STS2-Linnea/` 目录放入游戏 `mods/`，并安装 **RitsuLib 0.6.12 或更高版本**。工坊变体布局包可根据当前游戏分支自动选择内容 DLL。

安装或覆盖前请关闭游戏，并保留旧版本包以便回退。源码仓不包含美术母版，单独编译 DLL 不构成可安装的完整包。

## 开发

前置:Bash、Python 3.11+、由 `global.json` 选择的 .NET SDK。完整素材构建还需要 Godot 4.5.1 标准版及本地美术。游戏引用来自贡献者自己安装的对应游戏分支;RitsuLib 需要完整的 `compat/`、`shared/` 和 `RitsuLib.References.props`。

```bash
cp .local-dev.env.example .local-dev.env   # 在本机配置中填写依赖和工具位置
./scripts/check.sh                 # 源码检查,不要求游戏 DLL 或美术
./scripts/restore-refs.sh          # 从 GAME_DIR 复制对应分支编译引用
./scripts/build.sh --dll-only      # 只编译 DLL
./scripts/check.sh --full          # 完整素材、编译、PCK 检查
```

详细配置、平台范围和构建步骤见[贡献指南](CONTRIBUTING.md)及[构建管线](docs/dev/pipeline.md)。本地开发工作区存在时,先阅读私有入口 `local_dev/README.md`。

## 文档

- [文档导航](docs/README.md) · [开发规范](docs/dev/README.md) · [设计资料](docs/design/README.md) · [技术历史](docs/history/README.md) · [变更记录](CHANGELOG.md) · [路线图](docs/roadmap.md)
- 社区文件:[行为准则](CODE_OF_CONDUCT.md)、[安全策略](SECURITY.md)、[获取帮助](SUPPORT.md)、[贡献署名](CREDITS.md)。

## 许可与素材

原创软件采用 [MIT](LICENSE),具体范围见[许可说明](LICENSING.md)和[第三方说明](THIRD_PARTY_NOTICES.md)。美术母版由本 Mod 自行提供并确认授权范围，可使用自己的本地目录或私有美术仓,不随源码仓公开,也不使用 Git LFS。

本仓库由 [STS2-Template](https://github.com/SlayTheCircle/STS2-Template) 派生。
