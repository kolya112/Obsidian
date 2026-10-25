namespace Obsidian.Net.Packets.Play.Clientbound;

public partial class DamageEventPacket
{
    public required int EntityId { get; init; }

    public required int DamageTypeId { get; init; } // From damage_type registry

    public int? SourceCauseEntityId { get; init; }

    public int? SourceDirectEntityId { get; init; }

    public VectorD? SourcePosition { get; init; }

    public override void Serialize(INetStreamWriter writer)
    {
        writer.WriteVarInt(EntityId);
        writer.WriteVarInt(DamageTypeId);
        writer.WriteVarInt(SourceCauseEntityId is int causeId ? causeId + 1 : 0);
        writer.WriteVarInt(SourceDirectEntityId is int directId ? directId + 1 : 0);
        writer.WriteOptional(SourcePosition);
    }
}
