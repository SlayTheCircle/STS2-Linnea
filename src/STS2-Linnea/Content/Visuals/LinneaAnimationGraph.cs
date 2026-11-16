using STS2RitsuLib.Scaffolding.Visuals.StateMachine;

namespace LinneaMod.Content.Visuals;

internal sealed class LinneaAnimationGraph
{
    private readonly Creature _creature;
    private readonly ModAnimState _idle = new("idle", true);
    private readonly ModAnimState _dead = new("dead");
    private readonly ModAnimState _revive = new("revive");
    public ModAnimStateMachine Machine { get; }

    public LinneaAnimationGraph(IAnimationBackend backend, Creature creature)
    {
        _creature = creature;
        Machine = new ModAnimStateMachine(backend);
        var attack = new ModAnimState("attack");
        var cast = new ModAnimState("cast");
        var hit = new ModAnimState("hit");
        var specialAttack = new ModAnimState("lumi_attack");
        bool CanAct() => creature.IsAlive && Machine.Current != _dead && Machine.Current != _revive;
        bool SpecialAttack() => CombatManager.Instance.History.CardPlaysStarted.LastOrDefault(e => e.Actor == creature)?.CardPlay.Card is Cards.Lumi;
        Machine.AddAnyState("Attack", specialAttack, () => CanAct() && SpecialAttack());
        Machine.AddAnyState("Attack", attack, () => CanAct() && !SpecialAttack());
        Machine.AddAnyState("Cast", cast, CanAct);
        Machine.AddAnyState("Hit", hit, CanAct);
        Machine.AddAnyState("Dead", _dead, () => Machine.Current != _dead);
        Machine.AddAnyState("Revive", _revive, () => creature.IsAlive && Machine.Current == _dead);
        Machine.AnimationCompleted += state =>
        {
            if (Machine.Current == state && !state.IsLooping && state != _dead)
                Machine.Start(creature.IsAlive ? Idle() : _dead);
        };
        Machine.Start(creature.IsAlive ? Idle() : _dead);
    }
    private ModAnimState Idle() => _idle;
}
