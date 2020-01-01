use std::fs;
use std::cmp;

fn main() {
    let contents = fs::read_to_string("input.txt").expect("Something went wrong reading the file");
    let values: Vec<isize> = contents.lines().map(|s| -> isize { s.parse().unwrap() }).collect();

    println!("Part 1: {}", part_1(&values));
    println!("Part 2: {}", part_2(&values));
}

fn part_1(values: &Vec<isize>) -> isize {
    let mut total: isize = 0;
    for value in values {
        total += (value / 3) - 2;
    }
    total
}

fn part_2(values: &Vec<isize>) -> isize {
    let mut total: isize = 0;
    for value in values {
        let mut required_fuel = cmp::max(0, (value / 3) - 2);
        total += required_fuel;

        while required_fuel > 0 {
            required_fuel = cmp::max(0, (required_fuel / 3) - 2);
            total += required_fuel;
        }
    }
    total
}