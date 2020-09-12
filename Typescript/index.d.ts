import { BooleanLiteral } from "typescript";

export interface IRgb {
    r: number;
    b: number;
    g: number;
}

export interface IRect {
    x: number;
    y: number;
    width: number;
    height: number;
}

export interface IVertexPosition {
    x:number;
    y:number;
    z:number;
}

export interface IVertexTexture {
    x:number;
    y:number;
    z:number;
    u:number;
    v:number;
}

export interface IRgba extends IRgb {
    a: number;
}

export interface IGamepadState {
    up:boolean;
    down:boolean;
    left:boolean;
    right:boolean;

    a:boolean;
    b:boolean;
    x:boolean;
    y:Boolean;

    l1:boolean;
    r1:boolean;
    l2:boolean;
    r2:boolean;

    start:boolean;
    select:boolean;

}

export interface I3dContext {
    drawColoredTriangles:(color:IRgba, verticies:IVertexPosition[])=>void;
    drawTexturedTriangles:(textureName:string, verticies:IVertexTexture[])=>void;
	setView:(xEye:number, yEye:number, zEye:number, xLook:number, yLook:number, zLook:number)=>void;
}

export interface I2dContext {
    drawTexture:(texture:string, srcRect:IRect, destRect:IRect)=>void;
    drawSprite:(spriteName:string, x:number, y:number, frameTime:number)=>void;
    drawLightText:(x:number, y:number, text:string)=>void;
    drawDarkText:(x:number, y:number, text:string)=>void;
}

export interface IInputContext {
    pollGamepadState:()=>IGamepadState;

}



export interface IBadBits {

    createTexture: (name: string, width: number, height: number) => void;
    loadTexture: (name: string, path: string) => void;
    makeTransparent: (name: string, color: IRgb) => void;
    setPixel: (name: string, x: number, y: number, color: IRgba) => void;

    setInit: (initCallback: () => void) => void;
    setClose: (closeCallback: () => void) => void;
    setProcess: (processCallback: (dt: number, context: IInputContext) => void) => void;
    setDraw3d: (renderCallback: (dt: number, context: I3dContext) => void) => void;
    setDrawBackground: (renderCallback: (dt: number, context: I2dContext) => void) => void;
    setDrawForeground: (renderCallback: (dt: number, context: I2dContext) => void) => void;

    getTextureAttributes: (name: string) => IRect;

    log:(value:string)=>void;
}