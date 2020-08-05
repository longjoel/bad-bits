#include "../duk/duktape.h"

#pragma once

namespace BadBits {
	class EngineHooks
	{
	public:
		EngineHooks(duk_context *dukEngineContext);
		void Register();
	};

}
