'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

input.forEach((val) => { 
    input.forEach((other) => { 
        let splitted_a = val.split("");
        let splitted_b = other.split("");
    
        let total = 0;
        splitted_a.forEach((char, index) => {
            if (char !== splitted_b[index]) total++
        })
    
        if (total === 1) {
            let result = splitted_a.reduce((acc, char, index) => {
                return acc + ((char === splitted_b[index]) ? char : "");
            })
            console.log(result)
            process.exit();
        }
        return total;
    })
})