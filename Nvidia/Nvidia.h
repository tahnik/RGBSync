#pragma once

#include "nvapi.h"

using namespace System;

namespace SDKs {
	public ref class Nvidia
	{
	public:
		Nvidia();
		int GetTemperature();
	private:

	};
}
