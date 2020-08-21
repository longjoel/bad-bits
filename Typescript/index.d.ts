/**
 *
 *
 * @export
 * @interface IInputState
 */
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

/**
 *
 *
 * @export
 * @interface ITextureAttribs
 */
export interface ITextureAttribs {
    width: number;
    height: number;
}

/**
 *
 *
 * @export
 * @interface ISpriteCell
 */
export interface ISpriteCell {
    x: number,
    y: number,
    width: number,
    height: number
}

export interface IVertexPosition{
    x:number;
    y:number;
    z:number;
}

/**
 *
 *
 * @export
 * @interface ISpriteAttribs
 */
export interface ISpriteAttribs {

    [key: string]: ISpriteCell;
}

export interface IBadBits {

    /**
     * 
     *
     * @memberof IBadBits
     */
    createTexture: (name: string, width: number, height: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    drawSprite: (name: string, frame: string, x: number, y: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    drawTexture: (name: string, srcRect: number[], destRect: number[]) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    loadSpriteSheet: (name: string, path: string, spriteSheetPath: string) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    loadTexture: (name: string, path: string, spriteSheetPath: string) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setSpriteSheet: (name: string, rows: number, cols: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setPixelTransparent: (textureName: string, x: number, y: number, r: number, g: number, b: number, a: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setPixel: (textureName: string, x: number, y: number, r: number, g: number, b: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setInit: (initAction: () => void) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setProcess: (processAction: (dt: number) => void) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setRender2d: (render2dAction: (dt: number) => void) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    setRender3d: (render3dAction: (dt: number) => void) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    pollInput: () => IInputState;

    /**
     *
     *
     * @memberof IBadBits
     */
    makeTransparent: (name: string, r: number, int: number, b: number) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    getTextureAttribs: (name: string) => ITextureAttribs;

    /**
     *
     *
     * @memberof IBadBits
     */
    getSpriteAttribs: (name: string) => ISpriteAttribs;

    /**
     *
     *
     * @memberof IBadBits
     */
    logInfo: (text: string) => void;

    /**
     *
     *
     * @memberof IBadBits
     */
    drawFlatShadedTriangles:(r:number, g:number, b:number, verticies:IVertexPosition[])=> void;
}
