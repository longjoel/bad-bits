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

        var cos = Math.cos(deg);
        var sine = Math.sin(deg);

        var v0 = this.matrix[0];
        var v4 = this.matrix[4];
        var v8 = this.matrix[8];

        this.matrix[0] = cos * this.matrix[0] + sine * this.matrix[2];
        this.matrix[4] = cos * this.matrix[4] + sine * this.matrix[6];
        this.matrix[8] = cos * this.matrix[8] + sine * this.matrix[10];

        this.matrix[2] = cos * this.matrix[2] - sine * v0;
        this.matrix[6] = cos * this.matrix[6] - sine * v4;
        this.matrix[10] = cos * this.matrix[10] - sine * v8;

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

    transform(src: IVertexPosition[]): IVertexPosition[] {
        return src.map(v => {
            return {
                x: v.x * this.matrix[0] + v.y * this.matrix[1] + v.z * this.matrix[2],
                y: v.x * this.matrix[4] + v.y * this.matrix[5] + v.z * this.matrix[6],
                z: v.z * this.matrix[8] + v.y * this.matrix[9] + v.z * this.matrix[10]
            }
        });
    };
}
