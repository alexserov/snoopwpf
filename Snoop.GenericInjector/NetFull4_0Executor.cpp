#include "pch.h"
#include "NetFull4_0Executor.h"

#include <metahost.h>
#include "mscoree.h"
#pragma comment(lib, "mscoree.lib")

ICLRRuntimeHost* NetFull4_0Executor::GetNETFullCLRRuntimeHost()
{
	ICLRMetaHost* metaHost = nullptr;
	ICLRRuntimeInfo* runtimeInfo = nullptr;
	ICLRRuntimeHost* runtimeHost = nullptr;

	this->Log(L"Trying to get runtime meta host...");

	if (CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, reinterpret_cast<LPVOID*>(&metaHost)) == S_OK)
	{
		this->Log(L"Got runtime meta host.");

		this->Log(L"Trying to get runtime info...");

		if (metaHost->GetRuntime(L"v4.0.30319", IID_ICLRRuntimeInfo, reinterpret_cast<LPVOID*>(&runtimeInfo)) == S_OK)
		{
			this->Log(L"Got runtime info.");

			this->Log(L"Trying to get runtime host...");

			runtimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, reinterpret_cast<LPVOID*>(&runtimeHost));

			if (runtimeHost)
			{
				this->Log(L"Got runtime host.");
			}
			else
			{
				this->Log(L"Could not get runtime host.");
			}
		}
		else
		{
			this->Log(L"Could not get runtime info.");
		}
		

		runtimeInfo->Release();
		metaHost->Release();
	}
	else
	{
		this->Log(L"Could not get runtime meta host.");
	}

	return runtimeHost;
}

int NetFull4_0Executor::Execute(LPCWSTR pwzAssemblyPath, LPCWSTR pwzTypeName, LPCWSTR pwzMethodName, LPCWSTR pwzArgument, DWORD* pReturnValue)
{
	auto host = GetNETFullCLRRuntimeHost();

	if (!host)
	{
		return E_FAIL;
	}

	this->Log(L"Trying to ExecuteInDefaultAppDomain...");

	const auto hr = host->ExecuteInDefaultAppDomain(pwzAssemblyPath, pwzTypeName, pwzMethodName, pwzArgument, pReturnValue);

	this->Log(L"ExecuteInDefaultAppDomain finished.");

	host->Release();

	return hr;
}


ICLRRuntimeHost* NetFull4_0Launcher::GetNETFullCLRRuntimeHost()
{
	 ICLRMetaHost              *pMetaHost = nullptr;
        ICLRRuntimeInfo         *pCLRRuntimeInfo = nullptr;
        ICLRRuntimeHost         *pCLRRuntimeHost = nullptr;                
            HRESULT hr;
            // Получаем среду выполнения
            hr = CLRCreateInstance( CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost );
            if ( FAILED(hr) ) return nullptr;
    
            hr = pMetaHost->GetRuntime( L"v4.0.30319", IID_ICLRRuntimeInfo, (LPVOID*)&pCLRRuntimeInfo );
    
            BOOL bCLRIsLoadable;
            hr = pCLRRuntimeInfo->IsLoadable( &bCLRIsLoadable );
            if ( FAILED( hr ) ) return nullptr;
            if ( ! bCLRIsLoadable ) return nullptr;
            
            hr = pCLRRuntimeInfo->GetInterface( CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pCLRRuntimeHost );
            if ( FAILED(hr) ) return nullptr; 
            
            // Запускаем CLR
            hr = pCLRRuntimeHost->Start();
            if ( FAILED(hr) ) return nullptr;                 

	return pCLRRuntimeHost;
}

int NetFull4_0Launcher::Execute(LPCWSTR pwzAssemblyPath, LPCWSTR pwzTypeName, LPCWSTR pwzMethodName, LPCWSTR pwzArgument, DWORD* pReturnValue)
{
	auto host = GetNETFullCLRRuntimeHost();

	if (!host)
	{
		return E_FAIL;
	}

	this->Log(L"Trying to ExecuteInDefaultAppDomain...");

	const auto hr = host->ExecuteInDefaultAppDomain(pwzAssemblyPath, pwzTypeName, pwzMethodName, pwzArgument, pReturnValue);

	this->Log(L"ExecuteInDefaultAppDomain finished.");

	host->Release();

	return hr;
}