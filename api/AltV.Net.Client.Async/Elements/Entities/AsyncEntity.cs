using System.Diagnostics.CodeAnalysis;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Client.Async.Elements.Entities
{
    [SuppressMessage("ReSharper",
        "InconsistentlySynchronizedField")] // we sometimes use object in lock and sometimes not
    public class AsyncEntity<TEntity> : AsyncWorldObject<TEntity>, IEntity where TEntity : class, IEntity
    {
        public IntPtr EntityNativePointer => BaseObject.EntityNativePointer;
        
        public ushort Id => BaseObject.Id;

        public IPlayer NetworkOwner
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return null;
                    return BaseObject.NetworkOwner;
                }
            }
        }
        ISharedPlayer ISharedEntity.NetworkOwner => NetworkOwner;
        
        public Rotation Rotation
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.Rotation;
                }
            }
        }

        public virtual uint Model
        {
            get
            {
                lock (BaseObject)
                {
                    if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                    return BaseObject.Model;
                }
            }
        }
        
        public int ScriptId
        {
            get
            {
                var scriptId = 0;
                AsyncContext.RunOnMainThreadBlocking(() =>
                {
                    scriptId = BaseObject.ScriptId;
                });
                return scriptId;
            }
        }

        public bool Spawned => ScriptId != 0;

        public AsyncEntity(TEntity entity, IAsyncContext asyncContext) : base(entity, asyncContext)
        {
        }
        public bool GetSyncedMetaData<T1>(string key, out T1 result)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    result = default;
                    return false;
                }

                return BaseObject.GetSyncedMetaData(key, out result);
            }
        }
        
        public bool GetStreamSyncedMetaData<T1>(string key, out T1 result)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    result = default;
                    return false;
                }

                return BaseObject.GetStreamSyncedMetaData(key, out result);
            }
        }

        public void GetSyncedMetaData(string key, out MValueConst value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = MValueConst.Nil;
                    return;
                }

                BaseObject.GetSyncedMetaData(key, out value);
            }
        }

        public bool GetSyncedMetaData(string key, out int value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetSyncedMetaData(key, out value);
            }
        }

        public bool GetSyncedMetaData(string key, out uint value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetSyncedMetaData(key, out value);
            }
        }

        public bool GetSyncedMetaData(string key, out float value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetSyncedMetaData(key, out value);
            }
        }

        public void GetStreamSyncedMetaData(string key, out MValueConst value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = MValueConst.Nil;
                    return;
                }

                BaseObject.GetStreamSyncedMetaData(key, out value);
            }
        }

        public bool GetStreamSyncedMetaData(string key, out int value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetStreamSyncedMetaData(key, out value);
            }
        }

        public bool GetStreamSyncedMetaData(string key, out uint value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetStreamSyncedMetaData(key, out value);
            }
        }

        public bool GetStreamSyncedMetaData(string key, out float value)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    value = default;
                    return false;
                }

                return BaseObject.GetStreamSyncedMetaData(key, out value);
            }
        }

        public bool HasSyncedMetaData(string key)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                return BaseObject.HasSyncedMetaData(key);
            }
        }

        public bool HasStreamSyncedMetaData(string key)
        {
            lock (BaseObject)
            {
                if (!AsyncContext.CheckIfExists(BaseObject)) return default;
                return BaseObject.HasStreamSyncedMetaData(key);
            }
        }
    }
}