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
