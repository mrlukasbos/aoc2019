use std::fs;

fn main() {

    let contents = fs::read_to_string("input.txt").expect("Something went wrong reading the file");
    let values: Vec<isize> = contents.split(",").map(|s| -> isize { 
        s.parse().unwrap()
    }).collect();

    let mut initial_program_state = ProgramState {
        program: values,
        pc: 0,
        inputs: vec!(1),
        outputs: Vec::new()
    };

    let mut initial_program_state_2 = ProgramState {
        inputs: vec!(5),
        ..initial_program_state.clone()
    };

    let outputs_part_1 = compute(&mut initial_program_state);
    println!("Part 1: {}", outputs_part_1.outputs.last().unwrap());

    let outputs_part_2 = compute(&mut initial_program_state_2);
    println!("Part 2: {}", outputs_part_2.outputs.last().unwrap());
}

#[derive(Clone)]
struct ProgramState {
    program: Vec<isize>,
    pc: usize,
    inputs: Vec<isize>,
    outputs: Vec<isize>
}

fn compute(program_state: &mut ProgramState) -> &ProgramState {
    let program = &mut program_state.program;
    let mut pc = program_state.pc; // not sure if this mutates program_state
    let inputs = &mut program_state.inputs;
    let outputs = &mut program_state.outputs;

    loop {
        let opcode = program.get(pc).unwrap();
        let digits: Vec<u32> = get_digits((opcode + 100000) as u32).drain(1..).collect();

        let ins = digits[3] * 10 + digits[4];
        let arg_1 = get_param(digits[2], pc + 1, program);
        let arg_2 = get_param(digits[1], pc + 2, program);
        let arg_3 = get_param(digits[0], pc + 3, program);

        match ins {
            1 => { 
                program[arg_3 as usize] = program[arg_1 as usize] + program[arg_2 as usize]; 
                pc += 4;
            },
            2 => { 
                program[arg_3 as usize] = program[arg_1 as usize] * program[arg_2 as usize]; 
                pc += 4;
            },
            3 => {
                // take input
                if let Some(&input) = inputs.get(0) {
                    program[arg_1 as usize] = input;
                    pc += 2;
                    inputs.drain(1..); // remove the first element
                } else {
                    panic!("I don't have an input!");
                }
            },
            4 => { 
                outputs.push(program[arg_1 as usize]);
                pc += 2;
            }, 
            5 => {
                // jump if true
                if program[arg_1] != 0 {
                    pc = program[arg_2] as usize;
                } else {
                    pc += 3;
                }
            },
            6 => {
                // jump if false
                if program[arg_1] == 0 {
                    pc = program[arg_2] as usize;
                } else {
                    pc += 3;
                }
            },
            7 => {
                // less than
                if program[arg_1] < program[arg_2] {
                    program[arg_3] = 1;
                } else {
                    program[arg_3] = 0;
                }
                pc += 4
            },
            8 => {
                // equals
                if program[arg_1] == program[arg_2] {
                    program[arg_3] = 1;
                } else {
                    program[arg_3] = 0;
                }
                pc += 4
            },
            99 => {
                break;
            }
            _ => panic!("unknown instruction!")
        }
    }
    program_state
}

fn get_param(mode: u32, memory_location: usize, program: &Vec<isize>) -> usize {
    match mode {
        1 => memory_location as usize, // immediate
        _ => *program.get(memory_location).unwrap_or_else(|| &0) as usize // positional
    }
}

fn get_digits(num: u32) -> Vec<u32> {
    num.to_string().chars().map(|d| d.to_digit(10).unwrap()).collect()
}