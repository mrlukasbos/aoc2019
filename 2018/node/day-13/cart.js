const { RelativeDirection, Direction, isVertical } = require('./direction');
const TURNS = [RelativeDirection.LEFT, RelativeDirection.STRAIGHT, RelativeDirection.RIGHT]

module.exports = class Cart {
    constructor(x, y, direction) {
        this._x = x;
        this._y = y;
        this._direction = direction;
        this._turnIndex = 0;
        this._prevsymbol = isVertical(direction) ? '|' : '-';
        this._isalive = true;
    }   

    get x() { return this._x }
    get y() { return this._y }
    isAlive() { return this._isalive }
    get direction() { return this._direction }
    set direction(dir) { this._direction = dir } // absolute direction
    set prevSymbol(symbol) { this._prevsymbol = symbol }
    get prevSymbol() { return this._prevsymbol }
    get turnForIntersect() { 
        let turn = TURNS[this._turnIndex];
        this._turnIndex = (this._turnIndex + 1) % TURNS.length;
        return turn;
    }
    die() {
        this._isalive = false;
    }

    moveToNextLocation(location) {
        let newLocation = location;
        this._x = newLocation.x;
        this._y = newLocation.y;
    }

    getNextLocation() {
        let x = this._x;
        let y = this._y;
        switch(this._direction) {
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
}