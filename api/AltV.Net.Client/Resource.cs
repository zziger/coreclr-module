using AltV.Net.CApi;
using AltV.Net.Client.Elements;
using AltV.Net.Client.Elements.Factories;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Client.Elements.Pools;

namespace AltV.Net.Client
{
    public abstract class Resource : IResource
    {
        public abstract void OnStart();

        public abstract void OnStop();

        public virtual void OnTick()
        {
        }

        public virtual IPlayerFactory GetPlayerFactory()
        {
            return new PlayerFactory();
        }

        public virtual INatives GetNatives(string dllName)
        {
            return new Natives(dllName);
        }

        public virtual IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new VehicleFactory();
        }

        public virtual IBaseObjectFactory<IBlip> GetBlipFactory()
        {
            return new BlipFactory();
        }

        public virtual IBaseObjectFactory<ICheckpoint> GetCheckpointFactory()
        {
            return new CheckpointFactory();
        }

        public IBaseObjectFactory<IWebView> GetWebViewFactory()
        {
            return new WebViewFactory();
        }

        public IBaseObjectFactory<IAudio> GetAudioFactory()
        {
            return new AudioFactory();
        }

        public IBaseObjectFactory<IHttpClient> GetHttpClientFactory()
        {
            return new HttpClientFactory();
        }

        public IBaseObjectFactory<IWebSocketClient> GetWebSocketClientFactory()
        {
            return new WebSocketClientFactory();
        }

        public virtual INativeResourceFactory GetResourceFactory()
        {
            return new NativeResourceFactory();
        }

        public virtual ILogger GetLogger(ILibrary library, IntPtr corePointer)
        {
            return new Logger(library, corePointer);
        }

        public virtual ICore GetCore(ILibrary library,
            IntPtr nativePointer,
            IntPtr resourcePointer,
            IPlayerPool playerPool,
            IEntityPool<IVehicle> vehiclePool,
            IBaseObjectPool<IBlip> blipPool,
            IBaseObjectPool<ICheckpoint> checkpointPool,
            IBaseObjectPool<IAudio> audioPool,
            IBaseObjectPool<IHttpClient> httpClientPool,
            IBaseObjectPool<IWebSocketClient> webSocketClientPool,
            IBaseObjectPool<IWebView> webViewPool,
            IBaseObjectPool<IRmlDocument> rmlDocumentPool,
            IBaseObjectPool<IRmlElement> rmlElementPool,
            IBaseBaseObjectPool baseBaseObjectPool,
            IBaseEntityPool baseEntityPool,
            INativeResourcePool nativeResourcePool,
            ITimerPool timerPool,
            ILogger logger,
            INatives natives)
        {
            return new Core(library, nativePointer, resourcePointer, playerPool, vehiclePool, blipPool, checkpointPool,
                audioPool, httpClientPool, webSocketClientPool, webViewPool, rmlDocumentPool, rmlElementPool,
                baseBaseObjectPool, baseEntityPool, nativeResourcePool, timerPool, logger, natives);
        }
    }
}