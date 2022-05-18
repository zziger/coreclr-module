using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using AltV.Net.Client.Elements.Data;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Client.Async.Elements.Entities
{
    [SuppressMessage("ReSharper",
        "InconsistentlySynchronizedField")] // we sometimes use object in lock and sometimes not
    public class AsyncVehicle<TVehicle> : AsyncEntity<TVehicle>, IVehicle where TVehicle : class, IVehicle
    {
        public IntPtr VehicleNativePointer => BaseObject.VehicleNativePointer;

        public AsyncVehicle(TVehicle vehicle, IAsyncContext asyncContext) : base(vehicle, asyncContext)
        {
        }
        public byte WheelsCount
        {
            get
            {
                byte value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.WheelsCount;
                });

                return value;
            }
        }

        public ushort Gear
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.Gear;
                });

                return value;
            }
        }

        public byte IndicatorLights
        {
            get
            {
                byte value = default;
                AsyncContext.RunOnMainThreadBlockingAndRunAll(() =>
                {
                    value = BaseObject.IndicatorLights;
                });

                return value;
            }
            
            set
            {
                AsyncContext.Enqueue(() => BaseObject.IndicatorLights = value);
            }
        }

        public ushort MaxGear
        {
            get
            {
                ushort value = default;
                AsyncContext.RunOnMainThreadBlockingAndRunAll(() =>
                {
                    value = BaseObject.MaxGear;
                });

                return value;
            }

            set
            {
                AsyncContext.Enqueue(() => BaseObject.MaxGear = value);
            }
        }

        public float Rpm
        {
            get
            {
                float value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.Rpm;
                });

                return value;
            }
        }

        public byte SeatCount
        {
            
            get
            {
                byte value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.SeatCount;
                });

                return value;
            }
        }

        public float WheelSpeed
        {
            get
            {
                float value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.WheelSpeed;
                });

                return value;
            }
        }

        public Vector3 SpeedVector
        {
            get
            {
                Vector3 value = default;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    value = BaseObject.SpeedVector;
                });

                return value;
            }
        }
        
        public Handling GetHandling()
        {
            Handling handling = null;
            AsyncContext.RunOnMainThreadBlocking(() =>
            {
                handling = BaseObject.GetHandling();
            });

            return handling;
        }
    }
}