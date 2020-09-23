'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

function manhattan(a, b) {
    return Math.abs(b.x - a.x) + Math.abs(b.y - a.y)
}

let coordinates  = input.map(line => {
    let split = line.split(",");
    return {
        x: parseInt(split[0].trim()),
        y: parseInt(split[1].trim()),
    }
})

// first determine borders
let sortedOnX = coordinates.slice().sort((a,b) => {
    return b.x - a.x;
})

let sortedOnY = coordinates.slice().sort((a,b) => {
    return b.y - a.y;
})

let maxX = sortedOnX[0].x;
let minX = sortedOnX[sortedOnX.length-1].x;
let maxY = sortedOnY[0].y;
let minY = sortedOnY[sortedOnY.length-1].y;

console.log(minX, maxX, minY, maxY);

// get a list of coordinates with their closes points
let detailedCoordinates = [];
for(let y = minY; y <= maxY; y++) {
    for(let x = minX; x <= maxX; x++) {
        let thisCoordinate = {
            x,
            y,
            closestCoordinate: null,
        }
        let sortedOnDistance = coordinates.sort((a,b) => {
            return manhattan(thisCoordinate, a) - manhattan(thisCoordinate, b);
        })

        if (manhattan(thisCoordinate, sortedOnDistance[0]) !== manhattan(thisCoordinate, sortedOnDistance[1])) {
            thisCoordinate.closestCoordinate = sortedOnDistance[0];
            detailedCoordinates.push(thisCoordinate);
        }
    }
}

// filter out all points that are on an edge
let borderCoordinates = detailedCoordinates.slice().filter((coord) => {
    return coord.x === minX || coord.x === maxX || coord.y === maxY || coord.y === minY;
})

let infinitePoints = borderCoordinates.map((coord) => {
    return coord.closestCoordinate;
})

let filteredCoordinates = detailedCoordinates.slice().filter((coord) => {
    return infinitePoints.indexOf(coord.closestCoordinate) === -1;
})

let results = filteredCoordinates
                .map(coord => { return coord.closestCoordinate })
                .reduce((map, coord) => {
                    let key = coord.x + "-" + coord.y;
                    map[key] = (map[key] || 0) + 1; 
                    return map
                }, {})

console.log(results);
let bestKey = Array.from(Object.keys(results)).sort((a,b) => results[b] - results[a])[0]

console.log("A: ", results[bestKey]);
//  4398


let region = [];
for(let y = minY; y <= maxY; y++) {
    for(let x = minX; x <= maxX; x++) {
        let thisCoordinate = {
            x,
            y,
        }

        let totalDistance = coordinates.reduce((acc, coord) => {
            return acc + manhattan(thisCoordinate, coord);
        }, 0)
        if (totalDistance < 10000) {
            region.push(thisCoordinate);
        }
    }
}

console.log("B: ", region.length);
// 39560