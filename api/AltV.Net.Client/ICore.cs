using System.Numerics;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Client.Elements.Pools;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Shared;

namespace AltV.Net.Client
{
    public interface ICore : ISharedCore
    {
        new IPlayerPool PlayerPool { get; }
        new IEntityPool<IVehicle> VehiclePool { get; }
        IBaseEntityPool BaseEntityPool { get; }
        IBaseBaseObjectPool BaseBaseObjectPool { get; }
        IBaseObjectPool<IWebView> WebViewPool { get; }
        IBaseObjectPool<IBlip> BlipPool { get; }
        IBaseObjectPool<IRmlDocument> RmlDocumentPool { get; }
        IBaseObjectPool<IRmlElement> RmlElementPool { get; }
        LocalStorage LocalStorage { get; }
        new INativeResource Resource { get; }
        INatives Natives { get; }
        // HandlingData? GetHandlingByModelHash(uint modelHash); todo
        IBlip CreatePointBlip(Position position);
        IBlip CreateRadiusBlip(Position position, float radius);
        IBlip CreateAreaBlip(Position position, int width, int height);
        IntPtr CreateWebViewPtr(string url, bool isOverlay = false, Vector2? pos = null, Vector2? size = null);
        IWebView CreateWebView(string url, bool isOverlay = false, Vector2? pos = null, Vector2? size = null);
        IntPtr CreateWebViewPtr(string url, uint propHash, string targetTexture);
        IWebView CreateWebView(string url, uint propHash, string targetTexture);
        IntPtr CreatePointBlipPtr(Position position);
        IntPtr CreateRadiusBlipPtr(Position position, float radius);
        IntPtr CreateAreaBlipPtr(Position position, int width, int height);
        new IEntity GetEntityById(ushort id);
        void ShowCursor(bool state);
        void TriggerServerEvent(string eventName, params object[] args);
        IntPtr CreateRmlDocumentPtr(string url);
        IRmlDocument CreateRmlDocument(string url);
        Vector2 WorldToScreen(Vector3 position);
        string[] MarshalStringArrayPtrAndFree(IntPtr ptr, uint size);
        DiscordUser? GetDiscordUser();
        void LoadRmlFont(string path, string name, bool italic = false, bool bold = false);
    }
}