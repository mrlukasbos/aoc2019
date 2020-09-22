'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("")

let i = 0;
while (i !== (input.length - 1)) {
    let curr = input[i]
    let next = input[i+1]

    if (reaction(curr, next)) {
     //   console.log("reaction: ", curr, next)
        input.splice(i, 2); // remove two elements there
        i = Math.max(0, i-1);
    } else {
        i++;
    }
}

console.log("A: ", input.length);

// true when a is not b but when capitalized they are equal
function reaction(a, b) {
    return a !== b && a.toUpperCase() === b.toUpperCase() 
}
