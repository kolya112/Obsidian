using Obsidian.API.Registry.Codecs.DamageTypes;
using Obsidian.Entities;

namespace Obsidian.Utilities;

internal static class CombatDamageTypeConverter
{
    /// <summary>
    /// Converts a combat damage kind and its source entities to a damage type registry entry
    /// </summary>
    internal static DamageTypeCodec Convert(CombatDamageKind kind, IEntity source, IEntity? directSource)
    {
        DamageTypeCodec damageType;

        switch (kind)
        {
            case CombatDamageKind.Melee:
                damageType = source is IPlayer ? CodecRegistry.DamageType.PlayerAttack : CodecRegistry.DamageType.MobAttack;
                break;
            case CombatDamageKind.Projectile:
                if (directSource != null && directSource.Type != null)
                {
                    switch (directSource.Type)
                    {
                        case EntityType.Arrow:
                        case EntityType.SpectralArrow:
                            damageType = CodecRegistry.DamageType.Arrow;
                            break;
                        case EntityType.Trident:
                            damageType = CodecRegistry.DamageType.Trident;
                            break;
                        case EntityType.ShulkerBullet:
                            damageType = CodecRegistry.DamageType.MobProjectile;
                            break;
                        case EntityType.LlamaSpit:
                            damageType = CodecRegistry.DamageType.Spit;
                            break;
                        case EntityType.SmallFireball:
                        case EntityType.Fireball:
                            if (directSource is MobProjectile directProjectile && directProjectile.Owner != null)
                                damageType = CodecRegistry.DamageType.Fireball;
                            else
                                damageType = CodecRegistry.DamageType.UnattributedFireball;
                            break;
                        case EntityType.WitherSkull:
                            damageType = CodecRegistry.DamageType.WitherSkull;
                            break;
                        case EntityType.WindCharge:
                            damageType = CodecRegistry.DamageType.WindCharge;
                            break;
                        case EntityType.BreezeWindCharge:
                            damageType = CodecRegistry.DamageType.WindCharge;
                            break;
                        case EntityType.Snowball:
                            damageType = CodecRegistry.DamageType.Thrown;
                            break;
                        default:
                            damageType = CodecRegistry.DamageType.Generic;
                            break;
                    }
                }
                else
                    damageType = CodecRegistry.DamageType.Generic;
                break;
            case CombatDamageKind.Fall:
                damageType = CodecRegistry.DamageType.Fall;
                break;
            case CombatDamageKind.Drowning:
                damageType = CodecRegistry.DamageType.Drown;
                break;
            case CombatDamageKind.Explosion:
                // It's not very precise yet, but common cases are handled
                if (source is IPlayer || source.Type is EntityType.Creeper or EntityType.Ghast)
                    damageType = CodecRegistry.DamageType.PlayerExplosion;
                else
                    damageType = CodecRegistry.DamageType.Explosion;
                break;
            case CombatDamageKind.Thorns:
                damageType = CodecRegistry.DamageType.Thorns;
                break;
            case CombatDamageKind.Freezing:
                damageType = CodecRegistry.DamageType.Freeze;
                break;
            case CombatDamageKind.Fire:
                damageType = CodecRegistry.DamageType.OnFire;
                break;
            case CombatDamageKind.FireContact:
                damageType = CodecRegistry.DamageType.InFire;
                break;
            case CombatDamageKind.Magic:
                damageType = CodecRegistry.DamageType.Magic;
                break;
            case CombatDamageKind.SonicBoom:
                damageType = CodecRegistry.DamageType.SonicBoom;
                break;
            case CombatDamageKind.Starvation:
                damageType = CodecRegistry.DamageType.Starve;
                break;
            case CombatDamageKind.Void:
                damageType = CodecRegistry.DamageType.OutOfWorld;
                break;
            case CombatDamageKind.Suffocation:
                damageType = CodecRegistry.DamageType.InWall;
                break;
            default:
                damageType = CodecRegistry.DamageType.Generic;
                break;
        }

        return damageType;
    }
}
