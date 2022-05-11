﻿using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Client.Async
{
    public interface IAsyncContext : IAsyncDisposable
    {
        void Enqueue(Action action);

        void RunOnMainThreadBlocking(Action action);

        void RunOnMainThreadBlockingAndRunAll(Action action);

        void RunAll();

        bool CheckIfExists(IBaseObject baseObject);

        bool CheckIfExists(IWorldObject worldObject);

        bool CheckIfExists(IEntity entity);

        bool CreateRef(IBaseObject baseObject, bool safe = false);
    }

    public class AsyncContext : IAsyncContext
    {
        public static IAsyncContext Create(bool throwOnExistsCheck = true, bool createRefAutomatically = true)
        {
            return new AsyncContext(throwOnExistsCheck, createRefAutomatically);
        }

        private readonly LinkedList<Action> actions;

        private readonly LinkedList<IBaseObject> baseObjectRefs;

        private readonly SemaphoreSlim semaphoreSlim;

        private readonly bool throwOnExistsCheck;

        private readonly bool createRefAutomatically;

        private AsyncContext(bool throwOnExistsCheck, bool createRefAutomatically)
        {
            actions = new LinkedList<Action>();
            baseObjectRefs = new LinkedList<IBaseObject>();
            semaphoreSlim = new SemaphoreSlim(0, 1);
            this.throwOnExistsCheck = throwOnExistsCheck;
            this.createRefAutomatically = createRefAutomatically;
        }

        public void Enqueue(Action action)
        {
            actions.AddLast(action);
        }

        public bool CreateRef(IBaseObject baseObject, bool safe)
        {
            if (baseObject == null) return false;
            if (!createRefAutomatically) return true;
            try
            {
                if (!baseObject.AddRef())
                {
                    // Check safe to prevent throw on TryToAsync calls
                    if (!safe && throwOnExistsCheck)
                    {
                        throw baseObject switch
                        {
                            IEntity entity => new EntityRemovedException(entity),
                            IWorldObject worldObject => new WorldObjectRemovedException(worldObject),
                            _ => new BaseObjectRemovedException(baseObject)
                        };
                    }

                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }

            Alt.CoreImpl.CountUpRefForCurrentThread(baseObject);
            baseObjectRefs.AddLast(baseObject);
            return true;
        }

        public void RunOnMainThreadBlocking(Action action)
        {
            if (throwOnExistsCheck)
            {
                AltAsync.RunOnMainThreadBlockingThrows(action, semaphoreSlim);
            }
            else
            {
                AltAsync.RunOnMainThreadBlocking(action, semaphoreSlim);
            }
        }

        public void RunOnMainThreadBlockingAndRunAll(Action action)
        {
            RunOnMainThreadBlocking(() =>
            {
                if (actions.Count != 0)
                {
                    // Thread safe linked list loop
                    var first = actions.First;
                    var current = first;
                    var last = actions.Last;
                    var done = false;
                    if (current == null) return; //empty list
                    do
                    {
                        current.Value();
                        if (current == last) done = true;
                        current = current.Next;
                    } while (!done && current != null);

                    actions.Clear();
                }

                action();
            });
        }

        public bool CheckIfExists(IBaseObject baseObject)
        {
            if (baseObject.Exists) return true;
            if (throwOnExistsCheck)
            {
                throw new BaseObjectRemovedException(baseObject);
            }

            return false;
        }

        public bool CheckIfExists(IWorldObject worldObject)
        {
            if (worldObject.Exists) return true;
            if (throwOnExistsCheck)
            {
                throw new WorldObjectRemovedException(worldObject);
            }

            return false;
        }

        public bool CheckIfExists(IEntity entity)
        {
            if (entity.Exists) return true;
            if (throwOnExistsCheck)
            {
                throw new EntityRemovedException(entity);
            }

            return false;
        }

        public void RunAll()
        {
            RunAll(false);
        }

        public void RunAll(bool dispose)
        {
            if (actions.Count == 0 && !dispose) return;
            RunOnMainThreadBlocking(() =>
            {
                if (actions.Count != 0)
                {
                    // Thread safe linked list loop
                    var first = actions.First;
                    var current = first;
                    var last = actions.Last;
                    var done = false;
                    if (current == null) return; //empty list
                    do
                    {
                        current.Value();
                        if (current == last) done = true;
                        current = current.Next;
                    } while (!done && current != null);
                    actions.Clear();
                }
                
                if (dispose)
                {
                    var firstBaseObject = baseObjectRefs.First;
                    var currentBaseObject = firstBaseObject;
                    var lastBaseObject = baseObjectRefs.Last;
                    var doneBaseObject = false;
                    if (currentBaseObject != null) // check if empty
                    {
                        do
                        {
                            try
                            {
                                currentBaseObject.Value.RemoveRef();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                            }

                            Alt.CoreImpl.CountDownRefForCurrentThread(currentBaseObject.Value);
                            if (currentBaseObject == lastBaseObject) doneBaseObject = true;
                            currentBaseObject = currentBaseObject.Next;
                        } while (!doneBaseObject && currentBaseObject != null);
                    }
                    baseObjectRefs.Clear();
                }
            });
        }

        public ValueTask DisposeAsync()
        {
            RunAll(true);
            semaphoreSlim.Dispose();

            GC.SuppressFinalize(this);
            return ValueTask.CompletedTask;
        }
    }
}