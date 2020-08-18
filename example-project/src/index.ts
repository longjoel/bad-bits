import { IBadBits } from '../../Typescript/bad-bits';

declare var engine: IBadBits;
declare var __dirname: string;

const spriteInfo = [
    {
        name:'ship',
        path:"assets/tiny-ship.png",
        srcRect:[0,0,32,32]
    },
    {
        name:'background',
        path:"assets/background.png",
        srcRect:[0,0,1024,240]
    },
    {
        name:'laser-left',
        path:'assets/bullets.png',
        srcRect:[127,120,8,10]
    },
    {
        name:'laser-middle',
        path:'assets/bullets.png',
        srcRect:[138,120,8,10],
    }, 
    {
        name:'laser-right',
        path:'assets/bullets.png',
        srcRect:[155,120,8,10],
    }

]

let scrollPos = 0;
let maxScrollPos = spriteInfo.filter(x=> x.name==='background')[0].srcRect[2]-320;

let shipY = 120;

engine.setInit(()=>{

    spriteInfo.forEach(x=>{
        engine.loadTexture(x.name,x.path);
    });

    engine.logInfo('hello world');

});

engine.setProcess((dt)=>{

    const inputState = engine.pollInput();

    if(inputState.up){
        shipY = shipY - (40* dt)
    }else if(inputState.down){
        shipY = shipY + (40* dt);
    }

    if(shipY <0){shipY = 0;}
    if(shipY >210){shipY = 210;}

});

engine.setRender2d((dt)=>{
    scrollPos = scrollPos + dt*25;
    scrollPos = scrollPos % maxScrollPos;
    engine.drawTexture('background',[scrollPos,0,320,240],[0,0,320,240]);

    const ship = spriteInfo.filter(s=>s.name === 'ship')[0];
    engine.drawTexture('ship',ship.srcRect, [32,shipY,ship.srcRect[2], ship.srcRect[3]]);
})