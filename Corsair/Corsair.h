#pragma once

#define CORSAIR_LIGHTING_SDK_DISABLE_DEPRECATION_WARNINGS

#ifdef __APPLE__
#include <CUESDK/CUESDK.h>
#else
#include <CUESDK.h>
#endif

#include <iostream>
#include <atomic>
#include <thread>
#include <vector>
#include <unordered_set>
#include <cmath>

using namespace System;

namespace SDKs {
	enum class Color {
		RED,
		CYAN
	};

	public ref class Corsair
	{
	public:
		int Init();
		int ToRed(bool immediate);
		int ToCyan(bool immediate);
	private:
		int duration_ = 10000;

		std::vector<CorsairLedColor> GetAvailableKeys();
		const char* GetErrorString(CorsairError error);
	};
}
