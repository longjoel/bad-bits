import { IBadBits } from '../../Typescript/bad-bits';

declare var engine: IBadBits;
declare var __dirname: string;

engine.setInit(()=>{

    engine.logInfo('hello world');

});
