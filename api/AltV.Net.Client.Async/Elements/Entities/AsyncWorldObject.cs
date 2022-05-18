using System.Diagnostics.CodeAnalysis;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;

namespace AltV.Net.Client.Async.Elements.Entities
{
    [SuppressMessage("ReSharper",
        "InconsistentlySynchronizedField")] // we sometimes use player in lock and sometimes not
    public class AsyncWorldObject<TWorld> : AsyncBaseObject<TWorld>, IWorldObject where TWorld : class, IWorldObject
    {
        public IntPtr WorldObjectNativePointer => BaseObject.WorldObjectNativePointer;
        public Position Position
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.Position;
                }
            }
        }

        public AsyncWorldObject(TWorld worldObject, IAsyncContext asyncContext) : base(worldObject, asyncContext)
        {
        }
    }
}