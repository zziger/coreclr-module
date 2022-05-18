using AltV.Net.CApi;
using AltV.Net.Client.Elements;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Client.Elements.Pools;
using AltV.Net.Client.Events;
using AltV.Net.Shared.Events;

namespace AltV.Net.Client.Async
{
    public class AsyncCore : Core
    {
        
        internal readonly IEventHandler<TickDelegate> TickAsyncEventHandler =
            new AsyncEventHandler<TickDelegate>();

        internal readonly IEventHandler<ConsoleCommandDelegate> ConsoleCommandAsyncEventHandler =
            new AsyncEventHandler<ConsoleCommandDelegate>();

        internal readonly IEventHandler<PlayerSpawnDelegate> SpawnAsyncEventHandler =
            new AsyncEventHandler<PlayerSpawnDelegate>();

        internal readonly IEventHandler<PlayerDisconnectDelegate> DisconnectAsyncEventHandler =
            new AsyncEventHandler<PlayerDisconnectDelegate>();

        internal readonly IEventHandler<GameEntityCreateDelegate> GameEntityCreateAsyncEventHandler =
            new AsyncEventHandler<GameEntityCreateDelegate>();

        internal readonly IEventHandler<GameEntityDestroyDelegate> GameEntityDestroyAsyncEventHandler =
            new AsyncEventHandler<GameEntityDestroyDelegate>();

        internal readonly IEventHandler<PlayerEnterVehicleDelegate> EnterVehicleAsyncEventHandler =
            new AsyncEventHandler<PlayerEnterVehicleDelegate>();

        internal readonly IEventHandler<AnyResourceErrorDelegate> AnyResourceErrorAsyncEventHandler =
            new AsyncEventHandler<AnyResourceErrorDelegate>();

        internal readonly IEventHandler<AnyResourceStartDelegate> AnyResourceStartAsyncEventHandler =
            new AsyncEventHandler<AnyResourceStartDelegate>();

        internal readonly IEventHandler<AnyResourceStopDelegate> AnyResourceStopAsyncEventHandler =
            new AsyncEventHandler<AnyResourceStopDelegate>();

        internal readonly IEventHandler<KeyUpDelegate> KeyUpAsyncEventHandler =
            new AsyncEventHandler<KeyUpDelegate>();

        internal readonly IEventHandler<KeyDownDelegate> KeyDownAsyncEventHandler =
            new AsyncEventHandler<KeyDownDelegate>();

        internal readonly IEventHandler<ConnectionCompleteDelegate> ConnectionCompleteAsyncEventHandler =
            new AsyncEventHandler<ConnectionCompleteDelegate>();

        internal readonly IEventHandler<PlayerChangeVehicleSeatDelegate> PlayerChangeVehicleSeatAsyncEventHandler =
            new AsyncEventHandler<PlayerChangeVehicleSeatDelegate>();

        internal readonly IEventHandler<PlayerLeaveVehicleDelegate> PlayerLeaveVehicleAsyncEventHandler =
            new AsyncEventHandler<PlayerLeaveVehicleDelegate>();

        internal readonly IEventHandler<GlobalMetaChangeDelegate> GlobalMetaChangeAsyncEventHandler =
            new AsyncEventHandler<GlobalMetaChangeDelegate>();

        internal readonly IEventHandler<GlobalSyncedMetaChangeDelegate> GlobalSyncedMetaChangeAsyncEventHandler =
            new AsyncEventHandler<GlobalSyncedMetaChangeDelegate>();

        internal readonly IEventHandler<LocalMetaChangeDelegate> LocalMetaChangeAsyncEventHandler =
            new AsyncEventHandler<LocalMetaChangeDelegate>();

        internal readonly IEventHandler<StreamSyncedMetaChangeDelegate> StreamSyncedMetaChangeAsyncEventHandler =
            new AsyncEventHandler<StreamSyncedMetaChangeDelegate>();

        internal readonly IEventHandler<SyncedMetaChangeDelegate> SyncedMetaChangeAsyncEventHandler =
            new AsyncEventHandler<SyncedMetaChangeDelegate>();

        internal readonly IEventHandler<TaskChangeDelegate> TaskChangeAsyncEventHandler =
            new AsyncEventHandler<TaskChangeDelegate>();

        internal readonly IEventHandler<WindowResolutionChangeDelegate> WindowResolutionChangeAsyncEventHandler =
            new AsyncEventHandler<WindowResolutionChangeDelegate>();

        internal readonly IEventHandler<WindowFocusChangeDelegate> WindowFocusChangeAsyncEventHandler =
            new AsyncEventHandler<WindowFocusChangeDelegate>();

        internal readonly IEventHandler<RemoveEntityDelegate> RemoveEntityAsyncEventHandler =
            new AsyncEventHandler<RemoveEntityDelegate>();

        internal readonly IEventHandler<NetOwnerChangeDelegate> NetOwnerChangeAsyncEventHandler =
            new AsyncEventHandler<NetOwnerChangeDelegate>();
        
        public AsyncCore(ILibrary library, IntPtr nativePointer, IntPtr resourcePointer, IPlayerPool playerPool, IEntityPool<IVehicle> vehiclePool, IBaseObjectPool<IBlip> blipPool, IBaseObjectPool<ICheckpoint> checkpointPool, IBaseObjectPool<IAudio> audioPool, IBaseObjectPool<IHttpClient> httpClientPool, IBaseObjectPool<IWebSocketClient> webSocketClientPool, IBaseObjectPool<IWebView> webViewPool, IBaseObjectPool<IRmlDocument> rmlDocumentPool, IBaseObjectPool<IRmlElement> rmlElementPool, IBaseBaseObjectPool baseBaseObjectPool, IBaseEntityPool baseEntityPool, INativeResourcePool nativeResourcePool, ITimerPool timerPool, ILogger logger, INatives natives) : base(library, nativePointer, resourcePointer, playerPool, vehiclePool, blipPool, checkpointPool, audioPool, httpClientPool, webSocketClientPool, webViewPool, rmlDocumentPool, rmlElementPool, baseBaseObjectPool, baseEntityPool, nativeResourcePool, timerPool, logger, natives)
        {
            AltAsync.Setup(this);
        }
    }
}