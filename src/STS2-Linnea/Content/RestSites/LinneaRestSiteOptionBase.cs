using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.RestSites;

public abstract class LinneaRestSiteOptionBase : ModRestSiteOptionTemplate
{
    protected LinneaRestSiteOptionBase(Player owner) : base(owner) { }
    public override string? CustomIconPath => $"res://{ModEntry.ModId}/images/ui/{GetType().Name}.png";
}
