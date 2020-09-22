use std::fs;

fn main() {
    println!("Part 1: {}", part_1());
    println!("Part 2: {}", part_2());
}

fn part_1() -> usize {
    let contents = fs::read_to_string("input.txt").expect("Something went wrong reading the file");
    let mut values: Vec<usize> = contents.split(",").map(|s| -> usize { s.parse().unwrap() }).collect();

    values[1] = 12;
    values[2] = 2;
    let results = compute(&mut values);
    results[0]
}

fn part_2() -> usize {
   
    let contents = fs::read_to_string("input.txt").expect("Something went wrong reading the file");
    let values: Vec<usize> = contents.split(",").map(|s| -> usize { s.parse().unwrap() }).collect();

    for noun in 0..99 {
        for verb in 0..99 {
            let mut clone = values.clone();
            clone[1] = noun;
            clone[2] = verb;
            let results = compute(&mut clone);

            if results[0] == 19690720 {
                return noun * 100 + verb
            }
        }
    }
    0
}

fn compute(program: &mut Vec<usize>) -> &mut Vec<usize> {
    let mut pc = 0;

    loop {
        let ins = program.get(pc).unwrap();
        let arg_1 = program[pc + 1];
        let arg_2 = program[pc + 2];
        let arg_3 = program[pc + 3];

        match ins {
            1 => { 
                program[arg_3] = program[arg_1] + program[arg_2]; 
                pc += 4;
            },
            2 => { 
                program[arg_3] = program[arg_1] * program[arg_2]; 
                pc += 4;
            },
            99 => {
                break;
            }
            _ => panic!("unknown instruction!")
        }
    }
    program
}
