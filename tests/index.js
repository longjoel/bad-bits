"use strict";

const backup = require('./backup.js');

badBits = badBits || {};

badBits.setRender2d(function (dt, g) {

   // g.clear();

    g.setPixel(0, 0, 0xFF, 0xFF, 0x00, 0xFF);

    g.render();

});