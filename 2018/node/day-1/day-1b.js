'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

let totals = [];
let index = 0;
let total = 0;

while(totals.indexOf(total) === -1) {
    totals.push(total)
    total += parseInt(input[index])
    index = (index >= input.length - 1) ? 0 : (index + 1)
}
console.log("total is: ", total);
