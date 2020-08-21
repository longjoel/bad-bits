import { IBadBits, IInputState, IVertexPosition } from '../../Typescript/index';

declare var engine: IBadBits;
declare var __dirname: string;


let scrollPos = 0;
let maxScrollPos = 1024 - 320;

let shipY = 120;

interface bullet {
    x: number,
    y: number,
    life: number
};

let bullets: bullet[] = [];

engine.setInit(() => {

    engine.loadSpriteSheet('ship', 'assets/tiny-ship.png', 'assets/tiny-ship.json');
    engine.loadSpriteSheet('bullets', 'assets/bullets.png', 'assets/bullets.json');
    engine.loadSpriteSheet('background', 'assets/background.png', 'assets/background.json');

    engine.logInfo('hello world');

});

let lastInputState: IInputState = {} as IInputState;

engine.setProcess((dt) => {

    const inputState = engine.pollInput();

    if (inputState.up) {
        shipY = shipY - (40 * dt)
    } else if (inputState.down) {
        shipY = shipY + (40 * dt);
    }

    if (shipY < 0) { shipY = 0; }
    if (shipY > 210) { shipY = 210; }

    if (!lastInputState.a && inputState.a) {
        bullets.push({
            x: 64,
            y: shipY + 8,
            life: 16
        });
    }

    lastInputState = inputState;

    for (let b = 0; b < bullets.length; b++) {
        bullets[b].life = bullets[b].life - dt;
        bullets[b].x = bullets[b].x + (dt * 60 * (bullets[b].life / 24));
    }

    bullets = bullets.filter(b => b.life > 0);

});

engine.setRender3d((dt)=>{

engine.drawFlatShadedTriangles(128,128,128, [
    {x:-512.0,y:-512.0,z:0.0} as IVertexPosition,
    {x:-0.0,y:-512.0,z:0.0}as IVertexPosition,
    {x:-0.0,y:-0.0,z:0.0}as IVertexPosition,
]);

});

engine.setRender2d((dt) => {
    scrollPos = scrollPos + dt * 25;
    scrollPos = scrollPos % maxScrollPos;
    //engine.drawSprite('background', '_', -scrollPos, 0);
    engine.drawSprite('ship', '_', 32, shipY);


    bullets.forEach(b => {

        engine.drawSprite('bullets',
            'rightBullet',
            b.x, b.y);

    });

})