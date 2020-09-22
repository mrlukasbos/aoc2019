'use strict'

const fs = require('fs');

let results = [];
for (let n = 0; n < 26; n++) {
    let letter = String.fromCharCode(97 + n); 
    let input = fs.readFileSync('input.txt','utf8').split("").filter(elem => {
        return elem.toUpperCase() !== letter.toUpperCase()
    })

    let i = 0;
    while (i !== (input.length - 1)) {
        let curr = input[i]
        let next = input[i+1]
    
        if (reaction(curr, next)) {
            input.splice(i, 2); // remove two elements there
            i = Math.max(0, i-1);
        } else {
            i++;
        }
    }
    results.push(input.length);
}

let result = results.sort((a, b) => {
    return a - b;
})[0]
console.log("B: ", result)

// true when a is not b but when capitalized they are equal
function reaction(a, b) {
    return a !== b && a.toUpperCase() === b.toUpperCase() 
}