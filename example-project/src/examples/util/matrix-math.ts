// code borrowed from
// https://github.com/Wolfos/Matrix.ts/blob/master/Matrix.ts

import { IVertexPosition } from '../../../../Typescript/index';

export class Matrix4 {

    matrix: number[] = [];

    constructor() {
        this.matrix = [1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1];
    }

    rotateX(deg: number): Matrix4 {
        var cos = Math.cos(deg);
        var sine = Math.sin(deg);

        var v1 = this.matrix[1];
        var v5 = this.matrix[5];
        var v9 = this.matrix[9];

        this.matrix[1] = this.matrix[1] * cos - this.matrix[2] * sine;
        this.matrix[5] = this.matrix[5] * cos - this.matrix[6] * sine;
        this.matrix[9] = this.matrix[9] * cos - this.matrix[10] * sine;

        this.matrix[2] = this.matrix[2] * cos + v1 * sine;
        this.matrix[6] = this.matrix[6] * cos + v5 * sine;
        this.matrix[10] = this.matrix[10] * cos + v9 * sine;

        return this;
    };

    rotateY(deg: number): Matrix4 {

        var s = Math.sin(deg);
        var c = Math.cos(deg);
        var a00 =  this.matrix[0];
        var a01 =  this.matrix[1];
        var a02 =  this.matrix[2];
        var a03 =  this.matrix[3];
        var a20 =  this.matrix[8];
        var a21 =  this.matrix[9];
        var a22 =  this.matrix[10];
        var a23 =  this.matrix[11];
    
      
        this.matrix[0] = a00 * c - a20 * s;
        this.matrix[1] = a01 * c - a21 * s;
        this.matrix[2] = a02 * c - a22 * s;
        this.matrix[3] = a03 * c - a23 * s;
        this.matrix[8] = a00 * s + a20 * c;
        this.matrix[9] = a01 * s + a21 * c;
        this.matrix[10] = a02 * s + a22 * c;
        this.matrix[11] = a03 * s + a23 * c;
        return this;

    };

    rotateZ(deg: number): Matrix4 {
        var cos = Math.cos(deg);
        var sine = Math.sin(deg);

        var v0 = this.matrix[0];
        var v4 = this.matrix[4];
        var v8 = this.matrix[8];

        this.matrix[0] = cos * this.matrix[0] - sine * this.matrix[1];
        this.matrix[4] = cos * this.matrix[4] - sine * this.matrix[5];
        this.matrix[8] = cos * this.matrix[8] - sine * this.matrix[9];

        this.matrix[1] = cos * this.matrix[1] + sine * v0;
        this.matrix[5] = cos * this.matrix[5] + sine * v4;
        this.matrix[9] = cos * this.matrix[9] + sine * v8;

        return this;
    };

    scale(x: number, y: number, z: number): Matrix4 {

        this.matrix[0] = this.matrix[0] * x;
        this.matrix[5] = this.matrix[5] * y;
        this.matrix[10] = this.matrix[10] * z;

        return this;

    };
    translate(x: number, y: number, z: number): Matrix4 {

        this.matrix[12] += x;
        this.matrix[13] += y;
        this.matrix[14] += z;

        return this;
    };

    transform(src: any[]): any[] {
        return src.map(v => {
            return {...v,
                x: v.x * this.matrix[0] + v.y * this.matrix[1] + v.z * this.matrix[2],
                y: v.x * this.matrix[4] + v.y * this.matrix[5] + v.z * this.matrix[6],
                z: v.z * this.matrix[8] + v.y * this.matrix[9] + v.z * this.matrix[10]
            }
        });
    };
}
