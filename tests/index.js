"use strict";

const backup = require('./backup.js');

badBits.setRender2d(function (dt, g) {

    g.clear();

    g.setRect(0, 0, 16, 16, 255, 255, 255, 0xFF);
    g.setRect(0, 240 - 16, 16, 16, 128, 128, 128, 0xFF);
    g.setRect(320-16, 240 - 16, 16, 16, 128, 128, 0, 0xFF);
    
    g.render();

});