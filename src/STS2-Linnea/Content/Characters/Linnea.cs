using LinneaMod.Content.Visuals;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Scaffolding.Visuals.StateMachine;
using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using LinneaMod.Content.CardPools;
using LinneaMod.Content.Cards;
using LinneaMod.Content.PotionPools;
using LinneaMod.Content.RelicPools;
using LinneaMod.Content.Relics;

namespace LinneaMod.Content.Characters;

/// <summary>
/// 莉奈娅角色；自有资产由 LinneaCharacterAssets 注册，战斗姿态使用独立动画状态机。
/// </summary>
[RegisterCharacter]
public sealed class Linnea : CharacterModel, IModCreatureCombatAnimationStateMachineFactory
{
    public override Color NameColor => LinneaTheme.Primary;

    public override CharacterGender Gender => CharacterGender.Feminine;

    // 角色订阅后直接可用；世界线第一章只记录进度，不门控角色。
    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override int StartingHp => 88;

    public override int StartingGold => 99;

    public override CardPoolModel CardPool => ModelDb.CardPool<LinneaCardPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<LinneaRelicPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<LinneaPotionPool>();

    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<LinneaStrike>(),
        ModelDb.Card<LinneaStrike>(),
        ModelDb.Card<LinneaStrike>(),
        ModelDb.Card<LinneaStrike>(),
        ModelDb.Card<LinneaDefend>(),
        ModelDb.Card<LinneaDefend>(),
        ModelDb.Card<LinneaDefend>(),
        ModelDb.Card<LinneaDefend>(),
        ModelDb.Card<LumiGo>(),
        ModelDb.Card<TrainingBegins>(),
    };

    public override IReadOnlyList<RelicModel> StartingRelics => new RelicModel[] { ModelDb.Relic<AdventureNotes>() };

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.4f;

    public override Color EnergyLabelOutlineColor => LinneaTheme.Outline;

    public override Color DialogueColor => LinneaTheme.Ink;

    public override Color MapDrawingColor => LinneaTheme.Ink;

    public override Color RemoteTargetingLineColor => LinneaTheme.Primary;

    public override Color RemoteTargetingLineOutline => LinneaTheme.Outline;

    public override string CharacterSelectSfx => LinneaCharacterAssets.SelectionSfx;

    // 原稿没有音频，转场使用原版通用擦拭反馈。
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

    // 0.107.1 无此虚属性,其 GenerateAnimator 硬编码的默认映射与本覆写逐项相同,省略即等价。
    protected override List<(AnimState, string)> AnimationStates => new List<(AnimState, string)>
    {
        (new AnimState("attack"), "Attack"),
        (new AnimState("hurt"), "Hit"),
        (new AnimState("cast"), "Cast"),
    };

    public override List<string> GetArchitectAttackVfx()
    {
        return new List<string> { "vfx/vfx_attack_slash", "vfx/vfx_heavy_blunt", "vfx/vfx_bloody_impact" };
    }
    public ModAnimStateMachine TryCreateCombatAnimationStateMachine(Node visualsRoot)
        => LinneaCombatVisuals.Create(visualsRoot);
}
