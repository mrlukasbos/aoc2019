'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split(" ")

console.log("A: ", read(0).sum) // 43825
console.log("B: ", read(0).value) // 19276

function read(index) {
    let header = input.slice(index, index + 2);
    let childrenCount = parseInt(header[0]);
    let metaDataCount = parseInt(header[1]);
    
    let children = []
    let childIndex = index;
    for (let i = 0; i < childrenCount; i++) {
        let newChild = read(childIndex+2)
        childIndex += parseInt(newChild.length)
        children.push(newChild)
    }
    
    let length = children.reduce((acc, child) => acc + child.length, (2 + parseInt(metaDataCount))) 
    let metaData = input.slice(index + length - metaDataCount, index + length)
    let _metaDataSum = metaData.reduce((acc, val) => acc + parseInt(val), 0)
    let sum = children.reduce((acc, child) => acc + child.sum, _metaDataSum)

    let value = 0
    if (childrenCount === 0) {
        value = _metaDataSum
    } else {
        metaData.forEach(md => {
            if (md !== 0) {
                let child = children[md-1]
                if (child) {
                    value += child.value
                }
            }
        })
    }

    return {
        sum,
        length,
        value,
    }
}
