#include <stdio.h>
#include <SDL.h>
#include <SDL_opengl.h>
#include <gl/GL.h>

#include "../duk/duktape.h"

#include "app-context.h"

void BadBits::AppContext::_dukInit() {
	this->dukContext = duk_create_heap_default();
	if (this->dukContext == NULL) {
		fprintf(stderr, "could not create duk context.\n");
		exit(1);
	}

	duk_eval_string(this->dukContext, "1+2");

	if (duk_get_int(this->dukContext, -1) != 3) {
		fprintf(stderr, "Duk logic error.\n");
		duk_destroy_heap(this->dukContext);
		exit(1);
	}
}

void BadBits::AppContext::_dukCleanup() {
	if (this->dukContext != NULL) {
		duk_destroy_heap(this->dukContext);
	}
}

void BadBits::AppContext::_sdlInit() {
	if (SDL_Init(SDL_INIT_EVERYTHING) < 0) {
		fprintf(stderr, "could not initialize sdl2: %s\n", SDL_GetError());
		exit(1);
	}

	this->sdlWindow = SDL_CreateWindow(
		"Bad Bits Game Engine",
		SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED,
		640, 480,
		SDL_WINDOW_OPENGL | SDL_WINDOW_RESIZABLE
	);

	if (this->sdlWindow == NULL) {
		fprintf(stderr, "could not create window: %s\n", SDL_GetError());
		exit(1);
	}

	this->glContext = SDL_GL_CreateContext(this->sdlWindow);

	if (this->glContext == NULL) {
		fprintf(stderr, "could not create window: %s\n", SDL_GetError());
		exit(1);
	}

}

void BadBits::AppContext::_sdlCleanup() {

	if (this->glContext != NULL) {
		SDL_GL_DeleteContext(this->glContext);
	}

	if (this->sdlWindow != NULL) {
		SDL_DestroyWindow(this->sdlWindow);
	}

	SDL_Quit();

}