'use strict'

const fs = require('fs');
const Cart = require('./cart');
const direction = require('./direction');
const { fromSymbol, toSymbol, getAbsoluteDirection, Direction, RelativeDirection } = require('./direction');
let input = fs.readFileSync('input.txt','utf8').split("\n")
let cartSymbols = "<>^v".split("")

let coords = []
let carts = [];
input.forEach((line, y) => {
    let split = line.split("");
    coords[y] = [];
    split.forEach((symbol, x) => {
        coords[y][x] = symbol
        if (cartSymbols.includes(symbol)) {
            carts.push(new Cart(x, y, fromSymbol(symbol)));
        }
    })
})


function tick() {
    carts.sort((a, b) => {
        return (a.y*100000 + a.x) - (b.y*100000 + b.x);
    })

    carts.forEach(cart => {
        let isVertical = (cart.direction === Direction.NORTH || cart.direction === Direction.SOUTH)
        let newRelativeDirection = RelativeDirection.STRAIGHT;
       
        let nextLocation = getNextLocation(cart);

        if (!coords[nextLocation.y]) {
            console.log(cart)
            draw(coords)
        }

        switch (coords[nextLocation.y][nextLocation.x]) {
            case '+': {
                newRelativeDirection = cart.turnForIntersect
                break;
            }
            case '\\': {
                newRelativeDirection = isVertical ? RelativeDirection.LEFT : RelativeDirection.RIGHT
                break;
            }
            case '/': {
                newRelativeDirection = isVertical ? RelativeDirection.RIGHT : RelativeDirection.LEFT
                break;
            }
            case '<':
            case '>':
            case '^':
            case 'v': { 
                console.log("Collision! Location: ", nextLocation.x, nextLocation.y)
                console.log("A: " + nextLocation.x + "," + nextLocation.y)
                draw(coords)
                process.exit();
                break;
            }
        }

        let newDirection = getAbsoluteDirection(cart.direction, newRelativeDirection);

        if (toSymbol(newDirection) === undefined) {
            console.log(coords[nextLocation.y][nextLocation.x]);
            console.log(cart.direction, newRelativeDirection)
            console.log(newDirection);
            console.log("no")
            console.log(cart)
            process.exit();
        } 

        let prevSymbol = cart.prevSymbol;
        cart.prevSymbol = coords[nextLocation.y][nextLocation.x]
        coords[cart.y][cart.x] = prevSymbol
        cart.tick(newDirection)
        coords[nextLocation.y][nextLocation.x] = toSymbol(newDirection)
    })
}


// PART 1: 65,73

while (!collision(carts)) {
    tick();
}


function draw(coords) {
    for (let y = 0; y < coords.length; y++) {
        let str = ""
        for (let x = 0; x < coords[y].length; x++) {
            str+= coords[y][x]
        }
        console.log(str);
    }
}


function collision(carts) {
    let locations = carts.map(cart => { 
        return { x: cart.x, y: cart.y }
    });
    locations.sort((a,b) => {
        return (a.y*100000 + a.x) - (b.y*100000 + b.x);
    })
    for (let i = 0; i < locations.length-1; i++) {
        if (locations[i].x === locations[i+1].x 
            && locations[i].y === locations[i+1].y) {
                return locations[i];
            }
    }
    return null;
}


function getNextLocation(cart) {
    let x = cart.x;
    let y = cart.y;
    switch(cart.direction) {
        case Direction.NORTH: {
            y--;
            break;
        }; 
        case Direction.SOUTH: {
            y++;
            break;
        }
        case Direction.WEST: {
            x--;
            break;
        }
        case Direction.EAST: {
            x++;
            break;
        }
    }
    return  {
        x, 
        y
    }
}