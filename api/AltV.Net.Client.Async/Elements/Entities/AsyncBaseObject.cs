using System.Diagnostics.CodeAnalysis;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;
using AltV.Net.Shared;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Net.Client.Async.Elements.Entities
{
    [SuppressMessage("ReSharper",
        "InconsistentlySynchronizedField")] // we sometimes use object in lock and sometimes not
    public class AsyncBaseObject<TBase> : IBaseObject where TBase : class, IBaseObject
    {
        public IntPtr NativePointer => BaseObject.NativePointer;
        public IntPtr BaseObjectNativePointer => BaseObject.BaseObjectNativePointer;

        public ICore Core => BaseObject.Core;
        ISharedCore ISharedBaseObject.Core => BaseObject.Core;

        public bool Exists
        {
            get
            {
                lock (BaseObject)
                {
                    return BaseObject.Exists;
                }
            }
        }

        public BaseObjectType Type => BaseObject.Type;

        protected readonly TBase BaseObject;

        protected readonly IAsyncContext AsyncContext;

        public AsyncBaseObject(TBase baseObject, IAsyncContext asyncContext)
        {
            this.BaseObject = baseObject;
            this.AsyncContext = asyncContext;
        }
        
        public void SetMetaData(string key, object value)
        {
            AsyncContext.Enqueue(() => BaseObject.SetMetaData(key, value));
        }

        public bool GetMetaData<T>(string key, out T result)
        {
            T res = default;
            var success = false;
            AsyncContext.RunOnMainThreadBlockingAndRunAll(() =>
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    success = false;
                    return;
                }

                success = BaseObject.GetMetaData(key, out res);   
            });
            result = res;
            return success;
        }

        public bool GetMetaData(string key, out int result)
        {
            CheckIfEntityExists();
            GetMetaData(key, out MValueConst mValue);
            using (mValue)
            {
                if (mValue.type != MValueConst.Type.Int)
                {
                    result = default;
                    return false;
                }
                result = (int) mValue.GetInt();
            }

            return true;
        }

        public bool GetMetaData(string key, out uint result)
        {
            CheckIfEntityExists();
            GetMetaData(key, out MValueConst mValue);
            using (mValue)
            {
                if (mValue.type != MValueConst.Type.Uint)
                {
                    result = default;
                    return false;
                }
                result = (uint) mValue.GetUint();
            }

            return true;
        }

        public bool GetMetaData(string key, out float result)
        {
            
            CheckIfEntityExists();
            GetMetaData(key, out MValueConst mValue);
            using (mValue)
            {
                if (mValue.type != MValueConst.Type.Double)
                {
                    result = default;
                    return false;
                }
                result = (float) mValue.GetDouble();
            }

            return true;
        }

        public void SetMetaData(string key, in MValueConst value)
        {
            var @const = value;
            AsyncContext.Enqueue(() => BaseObject.SetMetaData(key, in @const));
        }

        public void GetMetaData(string key, out MValueConst value)
        {
            MValueConst val = default;
            AsyncContext.RunOnMainThreadBlockingAndRunAll(() =>
            {
                if (!AsyncContext.CheckIfExists(BaseObject))
                {
                    val = MValueConst.Nil;
                    return;
                }

                BaseObject.GetMetaData(key, out val);
            });
            
            value = val;
        }

        public void SetData(string key, object value)
        {
            BaseObject.SetData(key, value);
        }

        public bool GetData<T>(string key, out T result)
        {
            return BaseObject.GetData(key, out result);
        }

        public bool HasData(string key)
        {
            return BaseObject.HasData(key);
        }

        public IEnumerable<string> GetAllDataKeys()
        {
            return BaseObject.GetAllDataKeys();
        }

        public void DeleteData(string key)
        {
            BaseObject.DeleteData(key);
        }

        public void ClearData()
        {
            BaseObject.ClearData();
        }

        public bool HasMetaData(string key)
        {
            var res = false;
            AsyncContext.RunOnMainThreadBlockingAndRunAll(() =>
            {
                if (!AsyncContext.CheckIfExists(BaseObject)) return;
                res = BaseObject.HasMetaData(key);
            });

            return res;
        }

        public void DeleteMetaData(string key)
        {
            AsyncContext.Enqueue(() => BaseObject.DeleteMetaData(key));
        }

        public void Remove()
        {
            AsyncContext.Enqueue(() => BaseObject.Remove());
        }

        public void CheckIfEntityExists()
        {
            BaseObject.CheckIfEntityExists();
        }

        public void OnRemove()
        {
            BaseObject.OnRemove();
        }

        public bool AddRef()
        {
            return BaseObject.AddRef();
        }

        public bool RemoveRef()
        {
            return BaseObject.RemoveRef();
        }
    }
}