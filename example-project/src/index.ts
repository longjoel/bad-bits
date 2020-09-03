import { IBadBits } from '../../Typescript/index';

declare var engine: IBadBits;
declare var __dirname: string;


engine.setInit(() => {

    engine.loadTexture("ship", "assets/tiny-ship.png");

});

engine.setDrawBackground((dt, context) => {

    const srcRect = engine.getTextureAttributes("ship");
    
    context.drawTexture("ship", { x: 0, y: 0, width: 32, height: 32 }, { x: 0, y: 0, width: 32, height: 32 });
});

 engine.setDraw3d((dt, context)=>{


    context.drawColoredTriangles({r:128,g:128,b:128,a:255},[
        {x:0,y:0,z:0},
        {x:1,y:0,z:0},
        {x:1,y:1,z:0}
    ]);

    context.drawTexturedTriangles("ship", [
        {x:.15,y:.15,z:.25,u:0,v:0},
        {x:.45,y:.15,z:.25,u:1,v:0},
        {x:.45,y:.45,z:.25,u:1,v:1},

        {x:.15,y:.15,z:.25,u:0,v:0},
        {x:.15,y:.45,z:.25,u:0,v:1},
        {x:.45,y:.45,z:.25,u:1,v:1}
    ]);

 });