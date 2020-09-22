const fs = require('fs');

let input = fs.readFileSync('input.txt','utf8').split("\n")
let result = input.reduce((accumulator, currentValue) => {
    return parseInt(accumulator) + parseInt(currentValue)
});

console.log(result);

