'use strict'

const fs = require('fs');
const Cart = require('./cart');
const {isVertical, fromSymbol, toSymbol, getAbsoluteDirection, Direction, RelativeDirection } = require('./direction');
let input = fs.readFileSync('input.txt','utf8').split("\n")
let cartSymbols = "<>^v".split("")

let coords = []
let carts = [];
let collisions = [];
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
        if (cart.isAlive()) {
            let newRelativeDirection = RelativeDirection.STRAIGHT;
            let nextLocation = cart.getNextLocation();
            let collision = false;
            switch (coords[nextLocation.y][nextLocation.x]) {
                case '+': {
                    newRelativeDirection = cart.turnForIntersect
                    break;
                }
                case '\\': {
                    newRelativeDirection = isVertical(cart.direction) ? RelativeDirection.LEFT : RelativeDirection.RIGHT
                    break;
                }
                case '/': {
                    newRelativeDirection = isVertical(cart.direction) ? RelativeDirection.RIGHT : RelativeDirection.LEFT
                    break;
                }
                case '<':
                case '>':
                case '^':
                case 'v': { 
                    collisions.push({ 
                        x: nextLocation.x,
                        y: nextLocation.y,
                    })
                    collision = true;
                    break;
                }
            }
            if (collision) {
                let prevSymbol = cart.prevSymbol;
                coords[cart.y][cart.x] = prevSymbol;

                cart.die();
                carts.find(otherCart => {
                    if (otherCart.x === nextLocation.x  && otherCart.y === nextLocation.y) {
                        otherCart.die();
                        coords[otherCart.y][otherCart.x] = otherCart.prevSymbol
                    }
                })
            } else {
                let newDirection = getAbsoluteDirection(cart.direction, newRelativeDirection);
                let prevSymbol = cart.prevSymbol;
                cart.prevSymbol = coords[nextLocation.y][nextLocation.x]
                coords[cart.y][cart.x] = prevSymbol

                cart.moveToNextLocation(nextLocation)
                cart.direction = newDirection;
                coords[nextLocation.y][nextLocation.x] = toSymbol(newDirection)
            }
        }
        
    })
}

let aliveCarts;
while (true) {
    tick();
    aliveCarts = carts.filter(cart => cart.isAlive());
    if (aliveCarts.length === 1) {
        break;
    }
}

console.log("A: ",  collisions[0].x + "," +  collisions[0].y) // 65,73
console.log("B: ",  aliveCarts[0].x + "," +  aliveCarts[0].y) // 54,66

function draw(coords) {
    for (let y = 0; y < coords.length; y++) {
        let str = ""
        for (let x = 0; x < coords[y].length; x++) {
            str+= coords[y][x]
        }
        console.log(str);
    }
}