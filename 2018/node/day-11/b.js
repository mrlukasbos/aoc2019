
let GRID_SERIAL_NUMBER = 2187;

let coordinates = [];
for (let x = 1; x <= 300; x++) {
    coordinates[x] = [];
    for (let y = 1; y <= 300; y++) {
        coordinates[x][y] = {
            x,
            y,
            power: getPowerLevel(x,y),
            totalPower: -Infinity,
            bestsize: 0,
        }
    }
}

for (let s = 0; s <= 300; s++) {
    for (let x = 1; x <= 300-s; x++) {
        for (let y = 1; y <= 300-s; y++) {

            let accPower = 0
            for (let i = 0; i < s; i++) {
                for (let j = 0; j < s; j++) {
                  accPower += coordinates[x+i][y+j].power;
                }
            }
            if (accPower > coordinates[x][y].totalPower) {
                coordinates[x][y].totalPower = accPower;
                coordinates[x][y].bestsize = s;
            }
        }
    }
    console.log(s);
}
let bestCoordinate = coordinates.flat().sort((a,b) => b.totalPower - a.totalPower)[0];
console.log("B:", bestCoordinate.x + "," + bestCoordinate.y + "," + bestCoordinate.bestsize);

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
