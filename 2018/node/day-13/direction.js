const Direction = {
    NORTH: 0,
    EAST: 1,
    SOUTH: 2,
    WEST: 3,
}

const RelativeDirection = {
    LEFT: -1,
    STRAIGHT: 0,
    RIGHT: 1,
}

function getAbsoluteDirection(currentDirection, relativeDirection) {
    return (4 + currentDirection + relativeDirection) % 4;
}

function fromSymbol(symbol) {
    switch(symbol) {
        case '^': return Direction.NORTH;
        case '<': return Direction.WEST;
        case '>': return Direction.EAST;
        case 'v': return Direction.SOUTH;
    }
}

function toSymbol(direction) {
    switch(direction) {
        case Direction.NORTH: return '^'
        case Direction.WEST: return '<'
        case Direction.EAST: return '>'
        case Direction.SOUTH: return 'v'
    }
}

function isVertical(direction) {
   return (direction === Direction.NORTH || direction === Direction.SOUTH)
}

module.exports = { 
    Direction,
    RelativeDirection,
    getAbsoluteDirection,
    fromSymbol,
    toSymbol,
    isVertical,
}