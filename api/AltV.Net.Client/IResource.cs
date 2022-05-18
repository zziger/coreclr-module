using AltV.Net.CApi;
using AltV.Net.Client.Elements;
using AltV.Net.Client.Elements.Factories;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Client.Elements.Pools;

namespace AltV.Net.Client
{
    public interface IResource
    {
        public void OnStart();
        public void OnStop();
        public void OnTick();
        public INatives GetNatives(string dllName);
        public IPlayerFactory GetPlayerFactory();
        public IEntityFactory<IVehicle> GetVehicleFactory();
        public IBaseObjectFactory<IBlip> GetBlipFactory();
        public IBaseObjectFactory<ICheckpoint> GetCheckpointFactory();
        public IBaseObjectFactory<IAudio> GetAudioFactory();
        public IBaseObjectFactory<IHttpClient> GetHttpClientFactory();
        public IBaseObjectFactory<IWebSocketClient> GetWebSocketClientFactory();
        public IBaseObjectFactory<IWebView> GetWebViewFactory();
        public INativeResourceFactory GetResourceFactory();
        public ILogger GetLogger(ILibrary library, IntPtr corePointer);
        ICore GetCore(ILibrary library,
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
            INatives natives);
    }
}