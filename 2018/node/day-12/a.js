const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

let _rules = input.reduce((map, line) => {
    let splitted = line.split(" => ");
    map[splitted[0]] = splitted[1]
    return map
}, {})
let shifts = 0;


let total = 0;
// let initial_state = "#..#.#..##......###...###"
let initial_state = "..##.#######...##.###...#..#.#.#..#.##.#.##....####..........#..#.######..####.#.#..###.##..##..#..#"

let state = "..." + initial_state + "...";
for (let i = 0; i < 20; i++) {
    state = generate(_rules, state)
}

console.log(state); 
console.log(state.split('').reduce((acc, val, index) => acc + ((val === '#') ? index - (3+shifts) : 0), 0));
// 2542

function generate(rules, state) {
    let pots = state.split('')

    if (pots[2] === "#") {
        shifts++;
        pots.unshift(".")
    }
    if (pots[pots.length - 3] === "#") {
        pots.push(".")
    }

    let newPots = [];

    let indices = [-2, -1, 0, 1, 2];
    for (let i = 0; i < pots.length; i++) {
        let pattern = indices.reduce((acc, index) => acc + pots[i+index], "")
        newPots[i] = rules[pattern] ? rules[pattern] : ".";
    }
    return newPots.join("")
}