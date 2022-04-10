#include <utils.h>
#include <fstream>
#include <vector>
#include "CSharpScriptRuntime.h"
#include "CSharpResourceImpl.h"
#include "Log.h"
#include "CRC.h"
#include <sstream>
#include <string>
#include <iomanip>
#include "natives.h"

using namespace std;

bool CSharpResourceImpl::Start()
{
    Log::Info << "Starting resource" << Log::Endl;
    resource->EnableNatives();
    auto scope = resource->PushNativesScope();
    ResetDelegates();
    GetRuntime()->clr.start_resource(resource, core);
    return true;
}

bool CSharpResourceImpl::Stop()
{
    auto scope = resource->PushNativesScope();
    GetRuntime()->clr.stop_resource(resource);
    ResetDelegates();
    return true;
}

bool CSharpResourceImpl::OnEvent(const alt::CEvent* ev)
{
    if (ev == nullptr) return true;
    auto scope = resource->PushNativesScope();
    switch(ev->GetType()) {
        case alt::CEvent::Type::SERVER_SCRIPT_EVENT:
        {
            auto serverScriptEvent = (alt::CServerScriptEvent*)ev;
            alt::MValueArgs serverArgs = serverScriptEvent->GetArgs();
            uint64_t size = serverArgs.GetSize();
            if(size == 0)
            {
                OnServerEventDelegate(serverScriptEvent->GetName().CStr(), nullptr, 0);
            }
            else
            {
                auto constArgs = new alt::MValueConst*[size];
                for(uint64_t i = 0; i < size; i++)
                {
                    constArgs[i] = &serverArgs[i];
                }
                OnServerEventDelegate(serverScriptEvent->GetName().CStr(), constArgs, size);
                delete[] constArgs;
            }
            break;
        }
        case alt::CEvent::Type::CLIENT_SCRIPT_EVENT:
        {
            auto clientScriptEvent = (alt::CClientScriptEvent*)ev;
            alt::MValueArgs serverArgs = clientScriptEvent->GetArgs();
            uint64_t size = serverArgs.GetSize();
            if(size == 0)
            {
                OnClientEventDelegate(clientScriptEvent->GetName().CStr(), nullptr, 0);
            }
            else
            {
                auto constArgs = new alt::MValueConst*[size];
                for(uint64_t i = 0; i < size; i++)
                {
                    constArgs[i] = &serverArgs[i];
                }
                OnClientEventDelegate(clientScriptEvent->GetName().CStr(), constArgs, size);
                delete[] constArgs;
            }
            break;
        }
        case alt::CEvent::Type::CONSOLE_COMMAND_EVENT:
        {
            auto consoleCommandEvent = (alt::CConsoleCommandEvent*)ev;
            auto args = consoleCommandEvent->GetArgs();
            uint64_t size = args.size();
            auto cArgs = new const char*[size];
            for(uint64_t i = 0; i < size; i++)
            {
                cArgs[i] = args[i].c_str();
            }
            auto name = consoleCommandEvent->GetName();
            OnConsoleCommandDelegate(name.c_str(), cArgs, (uint32_t) size);
            delete[] cArgs;
            break;
        }
#pragma region Player Events
		case alt::CEvent::Type::SPAWNED:
	    {
            OnPlayerSpawnDelegate();
            break;
	    }
        case alt::CEvent::Type::DISCONNECT_EVENT:
        {
            OnPlayerDisconnectDelegate();
            break;
        }
        case alt::CEvent::Type::PLAYER_ENTER_VEHICLE:
        {
            auto playerEnterVehicleEvent = (alt::CPlayerEnterVehicleEvent*)ev;
            OnPlayerEnterVehicleDelegate(playerEnterVehicleEvent->GetTarget().Get(), playerEnterVehicleEvent->GetSeat());
            break;
        }
#pragma endregion
#pragma region Misc
        case alt::CEvent::Type::RESOURCE_ERROR:
        {
            auto resourceErrorEvent = (alt::CResourceErrorEvent*)ev;
            OnResourceErrorDelegate(resourceErrorEvent->GetResource()->GetName().CStr());
            break;
        }
        case alt::CEvent::Type::RESOURCE_START:
        {
            auto resourceStartEvent = (alt::CResourceStartEvent*)ev;
            OnResourceStartDelegate(resourceStartEvent->GetResource()->GetName().CStr());
            break;
        }
        case alt::CEvent::Type::RESOURCE_STOP:
        {
            auto resourceStopEvent = (alt::CResourceStopEvent*)ev;
            OnResourceStopDelegate(resourceStopEvent->GetResource()->GetName().CStr());
            break;
        }
        case alt::CEvent::Type::KEYBOARD_EVENT:
        {
            auto keyboardEvent = (alt::CKeyboardEvent*)ev;
            if (keyboardEvent->GetKeyState() == alt::CKeyboardEvent::KeyState::UP)
                OnKeyUpDelegate(keyboardEvent->GetKeyCode());
            else
                OnKeyDownDelegate(keyboardEvent->GetKeyCode());
            break;
        }

#pragma endregion
    }

    return true;
}

void CSharpResourceImpl::OnTick()
{
    OnTickDelegate();
}

void CSharpResourceImpl::OnCreateBaseObject(alt::Ref<alt::IBaseObject> objectRef)
{
    auto object = objectRef.Get();
    if (object == nullptr) return;

    switch (object->GetType()) {
        case alt::IBaseObject::Type::VEHICLE:
        {
            auto vehicle = dynamic_cast<alt::IVehicle*>(object);
            OnCreateVehicleDelegate(vehicle, vehicle->GetID());
            break;
        }
        case alt::IBaseObject::Type::PLAYER:
        {
            auto player = dynamic_cast<alt::IPlayer*>(object);
            OnCreatePlayerDelegate(player, player->GetID());
            break;
        }
    }
}

void CSharpResourceImpl::OnRemoveBaseObject(alt::Ref<alt::IBaseObject> objectRef)
{
    auto object = objectRef.Get();
    if (object == nullptr) return;

    switch (object->GetType()) {
        case alt::IBaseObject::Type::VEHICLE:
        {
            OnRemoveVehicleDelegate(dynamic_cast<alt::IVehicle*>(object));
            break;
        }
        case alt::IBaseObject::Type::PLAYER:
        {
            OnRemovePlayerDelegate(dynamic_cast<alt::IPlayer*>(object));
            break;
        }
    }
}

alt::String CSharpResourceImpl::ReadFile(alt::String path)
{
    auto pkg = resource->GetPackage();
    if(!pkg->FileExists(path)) return {};
    alt::IPackage::File* pkgFile = pkg->OpenFile(path);
    alt::String src(pkg->GetFileSize(pkgFile));
    pkg->ReadFile(pkgFile, src.GetData(), src.GetSize());
    pkg->CloseFile(pkgFile);

    return src;
}


void CSharpResourceImpl::ResetDelegates() {
    OnTickDelegate = []() {};
    OnClientEventDelegate = [](auto var, auto var2, auto var3) {};
    OnServerEventDelegate = [](auto var, auto var2, auto var3) {};
    OnConsoleCommandDelegate = [](auto var, auto var2, auto var3) {};

    OnCreatePlayerDelegate = [](auto var, auto var2) {};
    OnRemovePlayerDelegate = [](auto var) {};

    OnCreateVehicleDelegate = [](auto var, auto var2) {};
    OnRemoveVehicleDelegate = [](auto var) {};

    OnPlayerSpawnDelegate = [](){};
    OnPlayerDisconnectDelegate = [](){};
    OnPlayerEnterVehicleDelegate = [](auto var, auto var2) {};

    OnResourceErrorDelegate = [](auto var) {};
    OnResourceStartDelegate = [](auto var) {};
    OnResourceStopDelegate = [](auto var) {};

    OnKeyUpDelegate = [](auto var) {};
    OnKeyDownDelegate = [](auto var) {};
}