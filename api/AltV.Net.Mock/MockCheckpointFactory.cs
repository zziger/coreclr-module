using System;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Mock
{
    public class MockCheckpointFactory<TEntity> : IBaseObjectFactory<ICheckpoint> where TEntity : ICheckpoint
    {
        private readonly IBaseObjectFactory<ICheckpoint> checkpointFactory;

        public MockCheckpointFactory(IBaseObjectFactory<ICheckpoint> checkpointFactory)
        {
            this.checkpointFactory = checkpointFactory;
        }

        public ICheckpoint Create(ICore core, IntPtr entityPointer)
        {
            return MockDecorator<TEntity, ICheckpoint>.Create((TEntity) checkpointFactory.Create(core, entityPointer),
                new MockCheckpoint(core, entityPointer));
        }
    }
}