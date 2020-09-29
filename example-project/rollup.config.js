import resolve from '@rollup/plugin-node-resolve';
//import typescript from '@rollup/plugin-typescript';
import commonjs from '@rollup/plugin-commonjs';
import {terser} from 'rollup-plugin-terser';
export default {
  input: 'build/index.js',
  output: {
    file: 'bundle/index.js',
    format: 'cjs',
    exports:"default"
  },
  plugins: [commonjs(), resolve(),terser()]
};