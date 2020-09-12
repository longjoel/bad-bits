import { IBadBits } from '../../Typescript/index';

import { examples } from './examples';
declare var engine: IBadBits;
declare var __dirname: string;

let exampleIndex: number = 0;

engine.setInit(() => {

    examples.forEach(e => e.init(engine));

});

engine.setDrawBackground((dt, context) => {

    examples[exampleIndex].drawBackground(dt, context);

});

engine.setDrawForeground((dt, context) => {

    examples[exampleIndex].drawForeground(dt, context);


    context.drawLightText(0, 0, "This is the bad-bits engine.\n"
        + "Joel Longanecker (c)2020\n"
        + "Press Up/Dn to change example.\n"
        + examples[exampleIndex].name);

});
