"use strict";

const backup = require('./backup.js');

badBits = badBits || {};

badBits.setRender2d(function (dt, g) {

    g.clear();

    g.setPixel(32, 32, 0xFF, 0xFF, 0xFF, 0xFF);

    g.render();

});