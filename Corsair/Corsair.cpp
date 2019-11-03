#include "pch.h"

#include "Corsair.h"

namespace SDKs {
	std::vector<CorsairLedColor> colorsVector;
	const auto TIMEPERFRAME = 25;

	const char* Corsair::GetErrorString(CorsairError error)
	{
		switch (error) {
		case CE_Success:
			return "CE_Success";
		case CE_ServerNotFound:
			return "CE_ServerNotFound";
		case CE_NoControl:
			return "CE_NoControl";
		case CE_ProtocolHandshakeMissing:
			return "CE_ProtocolHandshakeMissing";
		case CE_IncompatibleProtocol:
			return "CE_IncompatibleProtocol";
		case CE_InvalidArguments:
			return "CE_InvalidArguments";
		default:
			return "unknown error";
		}
	}

	std::vector<CorsairLedColor> Corsair::GetAvailableKeys()
	{
		auto colorsSet = std::unordered_set<int>();
		for (int deviceIndex = 0, size = CorsairGetDeviceCount(); deviceIndex < size; deviceIndex++) {
			if (const auto ledPositions = CorsairGetLedPositionsByDeviceIndex(deviceIndex)) {
				for (auto i = 0; i < ledPositions->numberOfLed; i++) {
					const auto ledId = ledPositions->pLedPosition[i].ledId;
					colorsSet.insert(ledId);
				}
			}
		}

		std::vector<CorsairLedColor> colorsVector;
		colorsVector.reserve(colorsSet.size());
		for (const auto& ledId : colorsSet) {
			colorsVector.push_back({ static_cast<CorsairLedId>(ledId), 0, 0, 0 });
		}
		return colorsVector;
	}

	int Corsair::Init()
	{
		CorsairPerformProtocolHandshake();

		if (const auto error = CorsairGetLastError()) {
			std::cout << "Handshake failed: " << GetErrorString(error) << "\nPress any key tro quit." << std::endl;
			return 1;
		}

		colorsVector = GetAvailableKeys();

		if (colorsVector.empty()) {
			return 2;
		}
	}

	int Corsair::ToRed(bool immediate)
	{
		if (immediate)
		{
			for (auto& ledColor : colorsVector)
			{
				ledColor.r = 255;
				ledColor.g = 0;
				ledColor.b = 0;
			}

			CorsairSetLedsColorsAsync(
				static_cast<int>(colorsVector.size()),
				colorsVector.data(),
				nullptr,
				nullptr
			);

			return 0;
		}

		for (auto x = 0.0; x < 1; x += static_cast<double>(TIMEPERFRAME) / duration_) {
			auto val = static_cast<int>((1 - pow(x - 1, 2)) * 255);

			for (auto& ledColor : colorsVector)
			{
				ledColor.r = val;
				ledColor.g = 255 - val;
				ledColor.b = 255 - val;
			}

			CorsairSetLedsColorsAsync(
				static_cast<int>(colorsVector.size()),
				colorsVector.data(),
				nullptr,
				nullptr
			);

			std::this_thread::sleep_for(std::chrono::milliseconds(TIMEPERFRAME));
		}

		return 0;
	}

	int Corsair::ToCyan(bool immediate)
	{
		if (immediate)
		{

			for (auto& ledColor : colorsVector)
			{
				ledColor.r = 0;
				ledColor.g = 255;
				ledColor.b = 255;
			}

			CorsairSetLedsColorsAsync(
				static_cast<int>(colorsVector.size()),
				colorsVector.data(),
				nullptr,
				nullptr
			);
			return 0;
		}
		for (auto x = 0.0; x < 1; x += static_cast<double>(TIMEPERFRAME) / duration_) {
			auto val = static_cast<int>((1 - pow(x - 1, 2)) * 255);

			for (auto& ledColor : colorsVector)
			{
				ledColor.r = 255 - val;
				ledColor.g = val;
				ledColor.b = val;
			}

			CorsairSetLedsColorsAsync(
				static_cast<int>(colorsVector.size()),
				colorsVector.data(),
				nullptr,
				nullptr
			);

			std::this_thread::sleep_for(std::chrono::milliseconds(TIMEPERFRAME));
		}

		return 0;
	}

}
