#pragma once

namespace BadBits {

	class AppContext {

	private:
		void _dukInit(); 

		void _dukCleanup();

		void _sdlInit();
		void _sdlCleanup();


	public:
		duk_context *dukContext = NULL;
		SDL_Window *sdlWindow = NULL;
		void *glContext = NULL;

		void Init() {
			this->_dukInit();
			this->_sdlInit();
		}

		void Cleanup() {
			this->_sdlCleanup();
			this->_dukCleanup();
		}
	};


}