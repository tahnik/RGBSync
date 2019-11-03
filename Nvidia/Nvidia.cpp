#include "pch.h"

#include <iostream>
#include "Nvidia.h"

namespace SDKs {

	NvAPI_ShortString errorDescStr;
	NvPhysicalGpuHandle gpuHandleArray;

	char* GetNvAPIStatusString(NvAPI_Status nvapiErrorStatus)
	{
		NvAPI_GetErrorMessage(nvapiErrorStatus, errorDescStr);
		return errorDescStr;
	}

	NvAPI_Status GetGPUs(NvPhysicalGpuHandle* gpuHandleArray)
	{
		NvAPI_Status nvapiReturnStatus = NVAPI_ERROR;

		NvU32 gpuCount = 0;

		nvapiReturnStatus = NvAPI_EnumPhysicalGPUs(gpuHandleArray, &gpuCount);
		if (nvapiReturnStatus != NVAPI_OK)
		{
			printf("\nNvAPI_EnumPhysicalGPUs() failed.\nReturn Error : %s", GetNvAPIStatusString(nvapiReturnStatus));
			return nvapiReturnStatus;
		}

		return nvapiReturnStatus;
	}

	NvAPI_Status Initialize_NVAPI()
	{
		NvAPI_Status nvapiReturnStatus = NVAPI_ERROR;

		nvapiReturnStatus = NvAPI_Initialize();
		if (nvapiReturnStatus != NVAPI_OK)
		{
			printf("\nNvAPI_Initialize() failed.\nReturn Error : %s", GetNvAPIStatusString(nvapiReturnStatus));
		}
		else
		{
			printf("\nNVAPI Initialized successfully\n");
		}

		return nvapiReturnStatus;
	}
	Nvidia::Nvidia()
	{
		NvAPI_Status ret = Initialize_NVAPI();

		NvAPI_ShortString name;

		ret = GetGPUs(&gpuHandleArray);
		ret = NvAPI_GPU_GetFullName(gpuHandleArray, name);
	}

	int Nvidia::GetTemperature()
	{
		NV_GPU_THERMAL_SETTINGS thermal;

		thermal.version = NV_GPU_THERMAL_SETTINGS_VER;

		NvAPI_Status ret = NvAPI_GPU_GetThermalSettings(gpuHandleArray, 0, &thermal);

		return thermal.sensor[0].currentTemp;
	}
}

