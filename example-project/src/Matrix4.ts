

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
        this._41=1; this._42=0; this._43=0; this._44=0;
    }

    static mul(src1: Matrix4, src2: Matrix4) {

        let dest = new Matrix4();

        dest._11 = src1._11 * src2._11 + src1._12 * src2._21 + src1._13 * src2._31 + src1._14 * src2._41; 
        dest._12 = src1._11 * src2._12 + src1._12 * src2._22 + src1._13 * src2._32 + src1._14 * src2._42; 
        dest._13 = src1._11 * src2._13 + src1._12 * src2._23 + src1._13 * src2._33 + src1._14 * src2._43; 
        dest._14 = src1._11 * src2._14 + src1._12 * src2._24 + src1._13 * src2._34 + src1._14 * src2._44; 
        dest._21 = src1._21 * src2._11 + src1._22 * src2._21 + src1._23 * src2._31 + src1._24 * src2._41; 
        dest._22 = src1._21 * src2._12 + src1._22 * src2._22 + src1._23 * src2._32 + src1._24 * src2._42; 
        dest._23 = src1._21 * src2._13 + src1._22 * src2._23 + src1._23 * src2._33 + src1._24 * src2._43; 
        dest._24 = src1._21 * src2._14 + src1._22 * src2._24 + src1._23 * src2._34 + src1._24 * src2._44; 
        dest._31 = src1._31 * src2._11 + src1._32 * src2._21 + src1._33 * src2._31 + src1._34 * src2._41; 
        dest._32 = src1._31 * src2._12 + src1._32 * src2._22 + src1._33 * src2._32 + src1._34 * src2._42; 
        dest._33 = src1._31 * src2._13 + src1._32 * src2._23 + src1._33 * src2._33 + src1._34 * src2._43; 
        dest._34 = src1._31 * src2._14 + src1._32 * src2._24 + src1._33 * src2._34 + src1._34 * src2._44; 
        dest._41 = src1._41 * src2._11 + src1._42 * src2._21 + src1._43 * src2._31 + src1._44 * src2._41; 
        dest._42 = src1._41 * src2._12 + src1._42 * src2._22 + src1._43 * src2._32 + src1._44 * src2._42; 
        dest._43 = src1._41 * src2._13 + src1._42 * src2._23 + src1._43 * src2._33 + src1._44 * src2._43; 
        dest._44 = src1._41 * src2._14 + src1._42 * src2._24 + src1._43 * src2._34 + src1._44 * src2._44; 
        
        
        return dest;

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
rotate(a:number,x:number,y:number,z:number){

    const m = new Matrix4();

    var d = Math.sqrt(x*x + y*y + z*z);
     x /= d; y /= d; z /= d;
    var c = Math.cos(a), s = Math.sin(a), t = 1 - c;
  
    m._11 = x * x * t + c;
    m._12 = x * y * t - z * s;
    m._13 = x * z * t + y * s;
    m._14 = 0;
  
    m._21 = y * x * t + z * s;
    m._22 = y * y * t + c;
    m._23 = y * z * t - x * s;
    m._24 = 0;
  
    m._31 = z * x * t - y * s;
    m._32= z * y * t + x * s;
    m._33 = z * z * t + c;
    m._34 = 0;
  
    m._41 = 0;
    m._42 = 0;
    m._43 = 0;
    m._44 = 1;
  
    return Matrix4.mul(this,m);

}

    transform(src: any[]): any[] {
        return src.map(v => {
            return {
                ...v,
                x: (v.x * this._11) + (v.y * this._12) + (v.z * this._13),
                y: (v.x * this._21) + (v.y * this._22) + (v.z * this._23),
                z: (v.z * this._31) + (v.y * this._32) + (v.z * this._33) 
            }
        });
    };

}