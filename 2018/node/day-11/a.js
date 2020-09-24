
let GRID_SERIAL_NUMBER = 2187;

let coordinates = [];
for (let x = 1; x <= 300; x++) {
    coordinates[x] = [];
    for (let y = 1; y <= 300; y++) {
        coordinates[x][y] = {
            x,
            y,
            power: getPowerLevel(x,y),
            totalPower: 0
        }
    }
}

for (let x = 1; x <= 297; x++) {
    for (let y = 1; y <= 297; y++) {
        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                coordinates[x][y].totalPower += coordinates[x+i][y+j].power;
            }
        }
    }
}

let bestCoordinate = coordinates.flat().sort((a,b) => b.totalPower - a.totalPower)[0];
console.log("A:", bestCoordinate.x + "," + bestCoordinate.y);


function getPowerLevel(x, y) {
    let rack_id = x + 10;
    let power_level_start = rack_id * y;
    let power_level_increased = power_level_start + GRID_SERIAL_NUMBER;
    let power_level_multiplied = power_level_increased * rack_id;
    let plm_str = power_level_multiplied.toString();
    let hundreds_digit = parseInt(plm_str.charAt(plm_str.length-3));
    let result = hundreds_digit - 5;
    return result;
}