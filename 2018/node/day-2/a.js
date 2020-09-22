'use strict'

const fs = require('fs');

let input = fs.readFileSync('input.txt','utf8').split("\n")

let twos = 0
let threes = 0
input.forEach((val) => { 

    let counts = val.split("").map(character => { 
        var regex = new RegExp(character, 'g');
        return val.match(regex).length
    })
    if (counts.indexOf(2) !== -1) twos++;
    if (counts.indexOf(3) !== -1) threes++;
    console.log(counts);
})

console.log(twos*threes);
