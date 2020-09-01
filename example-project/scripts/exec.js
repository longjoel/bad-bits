const exec = require('child_process').execSync;
const fs = require('fs');
const path = require('path');

const root = path.join(__dirname, '..');
const execPath = path.join(__dirname,'..','..','BadBits.Engine.Next','bin','Debug','BadBits.Engine.Next.exe');
const startPath = path.join(__dirname,'..','build','index.js');

exec(`${execPath} ${startPath}`, {cwd:`${root}`,stdio:'inherit'});