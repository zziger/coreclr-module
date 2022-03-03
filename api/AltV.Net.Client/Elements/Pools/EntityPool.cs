﻿using AltV.Net.Client.Elements.Entities;
using AltV.Net.Client.Elements.Factories;
using AltV.Net.Client.Elements.Interfaces;

namespace AltV.Net.Client.Elements.Pools
{
    public abstract class EntityPool<TEntity> : IEntityPool<TEntity> where TEntity : IEntity
    {
        private readonly Dictionary<ushort, TEntity> _entities = new();
        private IEntityFactory<TEntity> _entityFactory;

        public EntityPool(IEntityFactory<TEntity> entityFactory)
        {
            this._entityFactory = entityFactory;
        }

        protected abstract ushort GetId(IntPtr highestPointer);
        
        public void Create(ICore core, IntPtr entityPointer, ushort id)
        {
            if (_entities.ContainsKey(id)) return;
            Add(_entityFactory.Create(core, entityPointer, id));
        }
        
        public void Create(ICore server, IntPtr entityPointer, ushort id, out TEntity entity)
        {
            if (_entities.TryGetValue(id, out entity!)) return;
            entity = _entityFactory.Create(server, entityPointer, id);
            Add(entity);
        }
        
        public void Create(ICore core, IntPtr entityPointer, out TEntity entity)
        {
            Create(core, entityPointer, GetId(entityPointer), out entity);
        }
        
        public void Add(TEntity entity)
        {
            _entities[entity.Id] = entity;
            OnAdd(entity);
        }
        
        public bool Remove(TEntity entity)
        {
            return Remove(entity.Id);
        }
        
        public bool Remove(ushort id)
        {
            if (!_entities.Remove(id, out var entity) || !entity.Exists) return false;
            // todo call on remove
            // todo set exists false
            OnRemove(entity);
            return true;
        }
        
        public bool Get(ushort id, out TEntity entity)
        {
            return _entities.TryGetValue(id, out entity) && entity.Exists;
        }
        
        public bool GetOrCreate(ICore core, IntPtr entityPointer, ushort entityId, out TEntity entity)
        {
            if (_entities.TryGetValue(entityId, out entity!)) return entity.Exists;

            entity = _entityFactory.Create(core, entityPointer, entityId);
            Add(entity);

            return entity.Exists;
        }
        
        public bool GetOrCreate(ICore core, IntPtr entityPointer, out TEntity entity)
        {
            return this.GetOrCreate(core, entityPointer, GetId(entityPointer), out entity);
        }

        public ICollection<TEntity> GetAllEntities()
        {
            return _entities.Values;
        }
        
        public KeyValuePair<ushort, TEntity>[] GetEntitiesArray()
        {
            var arr = new KeyValuePair<ushort, TEntity>[_entities.Count];
            var i = 0;
            foreach (var (ptr, entity) in _entities)
            {
                arr[i++] = new KeyValuePair<ushort, TEntity>(ptr, entity);
            }

            return arr;
        }
        
        public virtual void OnAdd(TEntity entity)
        {
        }
        
        public virtual void OnRemove(TEntity entity)
        {
        }
        
        public void Dispose()
        {
            _entities.Clear();
        }
    }
}