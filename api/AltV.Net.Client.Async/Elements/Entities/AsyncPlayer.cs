using System.Diagnostics.CodeAnalysis;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Client.Async.Elements.Entities
{
    [SuppressMessage("ReSharper",
        "InconsistentlySynchronizedField")] // we sometimes use object in lock and sometimes not
    public class AsyncPlayer<TPlayer> : AsyncEntity<TPlayer>, IPlayer where TPlayer : class, IPlayer
    {
        public IntPtr PlayerNativePointer => BaseObject.PlayerNativePointer;

        public AsyncPlayer(TPlayer player, IAsyncContext asyncContext) : base(player, asyncContext)
        {
        }

        public IVehicle? Vehicle
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return null;
                    return BaseObject.Vehicle;
                }
            }
        }
        ISharedVehicle ISharedPlayer.Vehicle => Vehicle;

        public IEntity? EntityAimingAt
        {
            get
            {
                IEntity? entity = null;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return;
                    entity = BaseObject.EntityAimingAt;
                });

                return entity;
            }
        }
        ISharedEntity ISharedPlayer.EntityAimingAt => EntityAimingAt;

        public bool IsTalking
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return false;
                    return BaseObject.IsTalking;
                }
            }
        }

        public float MicLevel
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.MicLevel;
                }
            }
        }

        public float NonSpatialVolume
        {
            get
            {
                AsyncContext.RunAll();
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.NonSpatialVolume;
                }
            }

            set
            {
                AsyncContext.Enqueue(() => BaseObject.NonSpatialVolume = value);
            }
        }

        public float SpatialVolume
        {
            get
            {
                AsyncContext.RunAll();
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.SpatialVolume;
                }
            }

            set
            {
                AsyncContext.Enqueue(() => BaseObject.SpatialVolume = value);
            }
        }

        public bool IsLocal => BaseObject.IsLocal;

        public string Name
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return "";
                    return BaseObject.Name;
                }
            }

        }

        public Position AimPosition
        {
            get
            {
                var pos = Position.Zero;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    pos = BaseObject.Position;
                });
                
                return pos;
            }
        }

        public ushort Armor
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.Armor;
                });

                return value;
            }
        }

        public uint CurrentWeapon
        {
            get
            {
                uint value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.CurrentWeapon;
                });

                return value;
            }
        }

        public Position EntityAimOffset
        {
            get
            {
                Position value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.EntityAimOffset;
                });

                return value;
            }
        }

        public bool IsFlashlightActive
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsFlashlightActive;
                });

                return value;
            }
        }

        public Rotation HeadRotation
        {
            get
            {
                Rotation value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.HeadRotation;
                });

                return value;
            }
        }

        public ushort Health
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.Health;
                });

                return value;
            }
        }

        public bool IsAiming
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsAiming;
                });

                return value;
            }
        }

        public bool IsDead
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsDead;
                });

                return value;
            }
        }

        public bool IsInRagdoll
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsInRagdoll;
                });

                return value;
            }
        }

        public bool IsInVehicle
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsInVehicle;
                });

                return value;
            }
        }

        public bool IsReloading
        {
            get
            {
                bool value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.IsReloading;
                });

                return value;
            }
        }

        public ushort MaxArmor
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.MaxArmor;
                });

                return value;
            }
        }

        public ushort MaxHealth
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.MaxHealth;
                });

                return value;
            }
        }

        public float MoveSpeed
        {
            get
            {
                float value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.MoveSpeed;
                });

                return value;
            }
        }

        public byte Seat
        {
            get
            {
                byte value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.Seat;
                });

                return value;
            }
        }
        
        public void GetCurrentWeaponComponents(out uint[] weaponComponents)
        {
            uint[] components = null;
            AsyncContext.RunOnMainThreadBlocking(() =>
            {
                BaseObject.GetCurrentWeaponComponents(out components);
            });

            weaponComponents = components;
        }
    }
}