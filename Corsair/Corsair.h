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
	enum class Mode {
		INITIAL,
		GAME,
		WORK,
	};

	public ref class Corsair
	{
	public:
		void Init();
		void ToGameMode();
		void ToWorkMode();
	private:
		int duration_ = 30000;
		bool initialized_ = false;
		Mode currentMode_ = Mode::INITIAL;

		std::vector<CorsairLedColor> GetAvailableKeys();
		const char* GetErrorString(CorsairError error);
	};
}
