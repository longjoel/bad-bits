"use strict";

require('./backup.js');

badBits = badBits || {};

let x = 0;

const clamp = function(x) {

    return x < 1000 ? x : 1000;
}

badBits.setRender(function (dt) {
    x = x + dt;
    badBits.info(clamp(x));
});