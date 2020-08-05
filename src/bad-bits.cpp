// bad-bits.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#pragma comment(lib,"SDL2.lib")
#pragma comment(lib,"SDL2main.lib")
#pragma comment(lib, "opengl32.lib")

#include <iostream>

#ifdef WIN32
#include <Windows.h>
#endif // WIN32


#include <SDL.h>
#include <SDL_opengl.h>
#include <gl/GL.h>

#include "duk/duktape.h"

#include "app-context/app-context.h"

#define SCREEN_WIDTH 640
#define SCREEN_HEIGHT 480



int main(int argc, char* args[]) {

	BadBits::AppContext _context;

	_context.Init();

	SDL_Event event;

	while (true) {
	
		SDL_PollEvent(&event);

		if (event.type == SDL_QUIT) {
			break;
		}

		if (event.type == SDL_WINDOWEVENT) {
		
			if (event.window.event == SDL_WINDOWEVENT_RESIZED) {
				glViewport(0, 0,event.window.data1,event.window.data2);
			}
		}

		glClearColor(0.f, 0.f, 0.f, 0.f);
		

		glClear(GL_DEPTH_BUFFER_BIT | GL_COLOR_BUFFER_BIT);

		glBegin(GL_QUADS);

		glColor3f(1, 0, 0);
		glVertex3f(-.5f, .5f, 0.0f);

		glColor3f(1, 1, 0);
		glVertex3f(.5f, .5f, 0.0f);

		glColor3f(0, 1, 0);
		glVertex3f(.5f, -.5f, 0.0f);

		glColor3f(0, 1, 1);
		glVertex3f(-.5f, -.5f, 0.0f);

		glEnd();

		glFlush();
		

		SDL_GL_SwapWindow(_context.sdlWindow);

	}

	_context.Cleanup();
	
	return 0;
}