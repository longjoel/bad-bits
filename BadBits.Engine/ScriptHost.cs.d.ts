declare module server {
	interface scriptHost {
		render2dFunction: any;
		render3dFunction: any;
		processFunction: any;
		initFunction: any;
		textures: { [index: string]: any };
	}
}
