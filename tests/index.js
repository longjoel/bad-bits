"use strict";

require('./backup.js');

badBits = badBits || {};

let x = 0;

const clamp = function(x) {

    return x < 1000 ? x : 1000;
}

let dtx = 0;
badBits.setRender2d(function (dt) {
   
    let i = 0;
    let j = 0;
    dtx = dtx + dt;
    for (i = 0; i < 320; i++) {
        for (j = 0; j < 240; j++) {
          //  badBits.setPixel2d(i, j, i / 320, j / 200, ((i+j+(dtx*100))%200) / 200);
        }
    }

});