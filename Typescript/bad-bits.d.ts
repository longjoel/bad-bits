export interface IInputState {
    up: boolean;
    down: boolean;
    left: boolean;
    right: boolean;
    a: boolean;
    b: boolean;
    x: boolean;
    y: boolean;
    l1: boolean;
    l2: boolean;
    r1: boolean;
    r2: boolean;
    start: boolean;
    select: boolean;
}

export interface ITextureAttribs {
    width: number;
    height: number;
}

export interface ISpriteCell{
    x:number, 
    y:number, 
    width:number, 
    height:number}

export interface ISpriteAttribs {
    [key: string]: ISpriteCell;
}

export interface IBadBits {
    createTexture: (name: string, width: number, height: number) => void;
    drawSprite: (name: string, frame:string, x:number, y:number) => void;
    drawTexture: (name: string, srcRect: number[], destRect: number[]) => void;
    loadSpriteSheet: (name: string, path: string, spriteSheetPath:string) => void;
    loadTexture: (name: string, path: string, spriteSheetPath:string) => void;
    setSpriteSheet: (name: string, rows: number, cols: number) => void;
    setPixelTransparent: (textureName: string, x: number, y: number, r: number, g: number, b: number, a: number) => void;
    setPixel: (textureName: string, x: number, y: number, r: number, g: number, b: number) => void;
    setInit: (initAction: () => void) => void;
    setProcess: (processAction: (dt: number) => void) => void;
    setRender2d: (render2dAction: (dt: number) => void) => void;
    setRender3d: (render3dAction: (dt: number) => void) => void;
    pollInput: () => IInputState;
    makeTransparent: (name: string, r: number, int: number, b: number) => void;
    getTextureAttribs: (name: string) => ITextureAttribs;
    getSpriteAttribs: (name: string) => ISpriteAttribs;
    logInfo: (text: string) => void;
}
