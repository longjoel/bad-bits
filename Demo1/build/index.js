"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
engine.setInit(function () {
    engine.loadSpriteSheet('font', 'asset\\font.png', 15, 15);
});
var drawChar = function (x, y, c) {
    var charTable = [' !"#$&\'()*+,-./',
        '0123456789:;<=>?',
        '@ABCDEFGHIJKLMNO',
        'PQRSTUVWXYZ[\\]^_',
        '`abcdefghijklmno',
        'pqrstuvwxyz{|}~'];
    var searchChar = function (cx) {
        for (var row_1 = 0; row_1 < charTable.length; row_1++) {
            for (var col_1 = 0; col_1 < charTable[row_1].length; col_1++) {
                if (charTable[row_1][col_1] == cx[0]) {
                    return [row_1, col_1];
                }
            }
        }
        return [-1, -1];
    };
    var _a = searchChar(c), row = _a[0], col = _a[1];
    if (row >= 0 && col >= 0) {
        engine.drawSprite('font', x, y, row, col);
    }
};
engine.setRender2d(function (dt) {
    drawChar(16, 16, 'a');
    drawChar(32, 16, 'b');
});
