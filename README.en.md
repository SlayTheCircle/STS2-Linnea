# STS2-Linnea · Linnea

[简体中文](README.md)

A playable Linnea character mod for Slay the Spire 2, built around Lumi, Encouragement, Foreknowledge, and Ore. It supports both game branches: 0.107.1 (stable) and 0.113.0 (beta). See [STATUS.md](STATUS.md) for implemented state and limits.

## Download and installation

The current version is **0.1.0**. Download the package for your game branch from [GitHub Releases](https://github.com/SlayTheCircle/STS2-Linnea/releases), place the included `STS2-Linnea/` directory in the game's `mods/` folder, and install **RitsuLib 0.6.12 or newer**. The variant-layout package selects the appropriate content DLL for the active game branch.

Close the game before installing or replacing files, and keep the previous package for rollback. Art masters are not included in the source repository; compiling the DLL alone does not produce an installable package.

## Development

Requirements: Bash, Python 3.11+, and the .NET SDK selected by `global.json`. Full asset builds also require the Godot 4.5.1 standard editor and local artwork. Obtain game reference assemblies from your own matching game installation. RitsuLib must include `compat/`, `shared/`, and `RitsuLib.References.props`.

```bash
cp .local-dev.env.example .local-dev.env   # Configure local dependency and tool locations
./scripts/check.sh                 # Source checks; no game DLLs or artwork required
./scripts/restore-refs.sh          # Copy references from GAME_DIR
./scripts/build.sh --dll-only      # Compile the DLL only
./scripts/check.sh --full          # Full assets, compilation, and PCK checks
```

See the [contributor guide](CONTRIBUTING.md) and [build pipeline](docs/dev/pipeline.md). Read the private entry `local_dev/README.md` first when a local workspace exists.

## Documentation

- [Documentation index](docs/README.md) · [Developer docs](docs/dev/README.md) · [Design docs](docs/design/README.md) · [Technical history](docs/history/README.md) · [Changelog](CHANGELOG.md) · [Roadmap](docs/roadmap.md)
- Community: [Code of conduct](CODE_OF_CONDUCT.md), [Security policy](SECURITY.md), [Support](SUPPORT.md), [Credits](CREDITS.md).

## License and artwork

Original software is licensed under [MIT](LICENSE). See the [licensing scope](LICENSING.md) and [third-party notices](THIRD_PARTY_NOTICES.md). Art masters are supplied by this mod's contributors in their own local directory or private repository; asset permissions must be recorded separately. The public source repository stays media-free; Git LFS is not used.

Derived from [STS2-Template](https://github.com/SlayTheCircle/STS2-Template).
