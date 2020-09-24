#pragma once

#include "pch.h"

#include <metahost.h>

#include "FrameworkExecutor.h"

class NetFull4_0Executor : public FrameworkExecutor
{
public:
    NetFull4_0Executor(const std::wstring& executorName) : FrameworkExecutor(executorName)
	{}
	NetFull4_0Executor() : FrameworkExecutor(L"NetFull4_0Executor")
	{}
	
	int Execute(LPCWSTR pwzAssemblyPath, LPCWSTR pwzTypeName, LPCWSTR pwzMethodName, LPCWSTR pwzArgument, DWORD* pReturnValue) override;

private:
	ICLRRuntimeHost* GetNETFullCLRRuntimeHost();
};

class NetFull4_0Launcher : public FrameworkExecutor
{
public:
	NetFull4_0Launcher() : FrameworkExecutor(L"NetFull4_0Launcher")
	{}
    int Execute(LPCWSTR pwzAssemblyPath, LPCWSTR pwzTypeName, LPCWSTR pwzMethodName, LPCWSTR pwzArgument, DWORD* pReturnValue) override;
private:
	ICLRRuntimeHost* GetNETFullCLRRuntimeHost();
};
