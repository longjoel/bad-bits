

export default class Matrix4 {
    [key:string]:any;
    _11: number; _12: number; _13: number; _14: number;
    _21: number; _22: number; _23: number; _24: number;
    _31: number; _32: number; _33: number; _34: number;
    _41: number; _42: number; _43: number; _44: number;

    constructor() {
        this._11=1; this._12=0; this._13=0; this._14=0;
        this._21=0; this._22=1; this._23=0; this._24=0;
        this._31=1; this._32=0; this._33=1; this._34=0;
        this._41=1; this._42=0; this._43=0; this._44=1;
    }

    static mul(m1: Matrix4, m2: Matrix4) {

        let newMatrix = new Matrix4();

        for (var i = 0; i < 4; i++) {
           
            for (var j = 0; j < 4; j++) {
                var sum = 0;
                for (var k = 0; k <4; k++) {
                    const ik = `_${i+1}${k+1}`;
                    const kj = `_${k+1}${j+1}`;
                    sum += m1[ik] * m2[kj];
                }
                const ij = `_${i+1}${j+1}`;

                newMatrix[ij] = sum;
            }
        }
        return newMatrix;

    }

    translate(x: number, y: number, z: number) {

        const translated = new Matrix4();
        translated._14 = x;
        translated._24 = y;
        translated._34 = z;
        return Matrix4.mul(this, translated);

    }


    scale(x: number, y: number, z: number) {

        const translated = new Matrix4();
        translated._11 = x;
        translated._22 = y;
        translated._33 = z;
        return Matrix4.mul(this, translated);

    }

    xRotate(angle: number) {
        const cc = Math.cos(angle);
        const ss = Math.sin(angle);
        const rotated = new Matrix4();
        rotated._22 = cc; rotated._23 = -ss;
        rotated._32 = ss; rotated._33 = cc;
        return Matrix4.mul(this, rotated);
    }

    yRotate(angle: number) {
        const cc = Math.cos(angle);
        const ss = Math.sin(angle);
        const rotated = new Matrix4();
        rotated._11 = cc; rotated._13 = ss;
        rotated._31 = -ss; rotated._33 = cc;
        return Matrix4.mul(this, rotated);
    }

    zRotate(angle: number) {
        const cc = Math.cos(angle);
        const ss = Math.sin(angle);
        const rotated = new Matrix4();
        rotated._11 = cc; rotated._12 = -ss;
        rotated._21 = ss; rotated._22 = cc;
        return Matrix4.mul(this, rotated);
    }

    transform(src: any[]): any[] {
        return src.map(v => {
            return {
                ...v,
                x: (v.x * this._11) + (v.y * this._12) + (v.z * this._13) + (this._14),
                y: (v.x * this._21) + (v.y * this._22) + (v.z * this._23) + (this._24),
                z: (v.z * this._31) + (v.y * this._32) + (v.z * this._33) + (this._34)
            }
        });
    };

}