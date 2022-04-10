﻿using System.Runtime.InteropServices;

namespace AltV.Net.CApi.ClientEvents
{
    public delegate void TickModuleDelegate();
        
    public delegate void ClientEventModuleDelegate(string name, IntPtr args, ulong size);
    public delegate void ServerEventModuleDelegate(string name, IntPtr args, ulong size);
    public delegate void ConsoleCommandModuleDelegate(string name, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] string[] args, int argsSize);
    
    public delegate void CreatePlayerModuleDelegate(IntPtr pointer, ushort id);
    public delegate void RemovePlayerModuleDelegate(IntPtr pointer);
    
    public delegate void CreateVehicleModuleDelegate(IntPtr pointer, ushort id);
    public delegate void RemoveVehicleModuleDelegate(IntPtr pointer);

    public delegate void PlayerSpawnModuleDelegate();
    public delegate void PlayerDisconnectModuleDelegate();
    public delegate void PlayerEnterVehicleModuleDelegate(IntPtr pointer, byte seat);

    public delegate void ResourceErrorModuleDelegate(string name);
    public delegate void ResourceStartModuleDelegate(string name);
    public delegate void ResourceStopModuleDelegate(string name);
    
    public delegate void KeyDownModuleDelegate(uint key);
    public delegate void KeyUpModuleDelegate(uint key);
}