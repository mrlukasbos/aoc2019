const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

let _data = input.map(line => {
    let splitted = line.split('<')
    let posStr = splitted[1].split(">")[0].split(",")
    let velStr = splitted[2].split(">")[0].split(",")


    let pos = {
        x: parseInt(posStr[0]),
        y: parseInt(posStr[1]),
    }
    let vel = {
        x: parseInt(velStr[0]),
        y: parseInt(velStr[1]),
    }

    return {
        pos,
        vel
    }
})

// set the initial data
prev = {
    data: _data,
    width: Infinity,
    height: Infinity
}

let seconds = 0;
while (true) {
    let dimensions = doTimeStep(prev.data, false);

    if (dimensions.width > prev.width) {
        let smallest = doTimeStep(prev.data, true);

        console.log("A: ")
        draw(smallest.data);

        console.log("B: ", seconds)
        break;
    } 
    prev = dimensions;
    seconds++;
}

// AHFGRKEE


function draw(d) {
    let smallestX = d.reduce((acc, val) => Math.min(acc, val.pos.x), Infinity);
    let largestX = d.reduce((acc, val) => Math.max(acc, val.pos.x), -Infinity);
    let smallestY = d.reduce((acc, val) => Math.min(acc, val.pos.y), Infinity);
    let largestY = d.reduce((acc, val) => Math.max(acc, val.pos.y), -Infinity);


    for (let y = smallestY; y <= largestY; y++) {
        let str = "";
        for (let x = smallestX; x <= largestX; x++) {
            if (d.some(item => item.pos.x === x && item.pos.y === y)) {
                str += "#";
            } else {
                str += "."
            }
        }
        console.log(str);
    }
}

function doTimeStep(d, inversed) {

    let factor = 1;
    if (inversed) {
        factor = -1;
    }

    d.slice().map(item => {
        item.pos.x += item.vel.x * factor;
        item.pos.y += item.vel.y * factor;
    })
    let smallestX = d.reduce((acc, val) => Math.min(acc, val.pos.x), Infinity);
    let largestX = d.reduce((acc, val) => Math.max(acc, val.pos.x), -Infinity);
    let smallestY = d.reduce((acc, val) => Math.min(acc, val.pos.y), Infinity);
    let largestY = d.reduce((acc, val) => Math.max(acc, val.pos.y), -Infinity);
    return {
        data: d,
        width: Math.abs(largestX - smallestX),
        height: Math.abs(largestY - smallestY),
    }
}






