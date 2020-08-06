"use strict";

badBits = badBits || {};

let x = 0;

const clamp = function (x) {

    return x < 1000 ? x : 1000;
}

badBits.SetRender(function (dt) {
    x = x + dt;
    badBits.Info(clamp(x));
});