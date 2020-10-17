
/**
 * 
 */
export interface IRgb {
    r: number;
    b: number;
    g: number;
}

/**
 * 
 */
export interface IRgba extends IRgb {
    a: number;
}

/**
 * 
 */
export interface IRect {
    x: number;
    y: number;
    width: number;
    height: number;
}

/**
 * 
 */
export interface IVertexPosition {
    x: number;
    y: number;
    z: number;
}

/**
 * 
 */
export interface IVertexTexture extends IVertexPosition {
    u: number;
    v: number;
}

/**
 * 
 */
export interface IGamepadState {
    up: boolean;
    down: boolean;
    left: boolean;
    right: boolean;

    a: boolean;
    b: boolean;
    x: boolean;
    y: Boolean;

    l1: boolean;
    r1: boolean;
    l2: boolean;
    r2: boolean;

    start: boolean;
    select: boolean;

}

export interface ITransform {

    scaleX: number;
    scaleY: number;
    scaleZ: number;

    yaw: number;
    pitch: number;
    roll: number;

    translateX: number;
    translateY: number;
    translateZ: number;
}

/**
 * 
 */
export interface I3dContext {
    drawColoredTriangles: (color: IRgba, verticies: IVertexPosition[], transform?: ITransform) => void;
    drawTexturedTriangles: (textureName: string, verticies: IVertexTexture[], transform?: ITransform) => void;
    setView: (xEye: number, yEye: number, zEye: number, xLook: number, yLook: number, zLook: number) => void;
}

/**
 * 
 */
export interface I2dContext {
    drawTexture: (texture: string, srcRect: IRect, destRect: IRect) => void;
    drawSprite: (spriteName: string, destRect: IRect, ignoreAspectRatio: boolean, frameTime: number) => void;
    drawLightText: (x: number, y: number, text: string) => void;
    drawDarkText: (x: number, y: number, text: string) => void;
}

/**
 * 
 */
export interface IInputContext {
    pollGamepadState: () => IGamepadState;

}

export interface IAudioContext {

    playSound: (name: string, pan: number, volume: number) => void;

    startMusic: (name: string, onFinished: () => void) => void;
    startMusic: (name: string) => void;
    stopMusic: () => void;
    setMusicVolume: (volume: number) => void;
}

/**
 * 
 */
export interface IBadBits {

    createTexture: (name: string, width: number, height: number) => void;
    loadTexture: (name: string, path: string) => void;
    makeTransparent: (name: string, color: IRgb) => void;
    setPixel: (name: string, x: number, y: number, color: IRgba) => void;

    setInit: (initCallback: () => void) => void;
    setClose: (closeCallback: () => void) => void;
    setProcess: (processCallback: (dt: number, context: IInputContext, audioContext: IAudioContext) => void) => void;
    setDraw3d: (renderCallback: (dt: number, context: I3dContext) => void) => void;
    setDrawBackground: (renderCallback: (dt: number, context: I2dContext) => void) => void;
    setDrawForeground: (renderCallback: (dt: number, context: I2dContext) => void) => void;

    getTextureAttributes: (name: string) => IRect;

    loadSprite: (name: string, path: string) => void;

    loadAudio: (name: string, path: string) => void;

    log: (value: string) => void;
}
