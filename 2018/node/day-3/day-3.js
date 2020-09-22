'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

// parse input
let formattedInput = input.map((line, index) => {
    let split1 = line.split('@');
    let split2 = split1[1].split(':');

    let coordinates = split2[0].trim();
    let coordSplit = coordinates.split(',');

    let dimensions = split2[1].trim();
    let dimensionSplit = dimensions.split('x')

    return {
        id: index+1,
        x: parseInt(coordSplit[0]),
        y: parseInt(coordSplit[1]),
        width: parseInt(dimensionSplit[0]),
        height: parseInt(dimensionSplit[1]),
    }
})

let claims = []
for (let instruction of formattedInput) {
    for (let i = instruction.x; i < (instruction.x + instruction.width); i++) {
        if (!claims[i]) claims[i] = []
        for (let j = instruction.y; j < (instruction.y + instruction.height); j++) {
            if (!claims[i][j]) {
                claims[i][j] = {
                    count: 0,
                    id: instruction.id,
                }
            }
            claims[i][j].count++;
            claims[i][j].id = instruction.id;
        }
    }
}

console.log("A:", [].concat(...claims).filter(claim => claim.count > 1).length)

// just iterate again for part two
for (let instruction of formattedInput) {
    let overlaps = false;
    for (let i = instruction.x; i < (instruction.x + instruction.width); i++) {
        for (let j = instruction.y; j < (instruction.y + instruction.height); j++) {
            if (claims[i][j].count !== 1) overlaps = true;
        }
    }
    if (!overlaps) {
        console.log("B:", instruction.id)
    }
}
