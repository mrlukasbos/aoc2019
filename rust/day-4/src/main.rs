fn main() {
    println!("part 1: {}", part_1());
    println!("part 2: {}", part_2());
}

fn part_1() -> usize{
    let mut total : usize = 0;
    for code in 235741..706948 {
        if is_not_decreasing(code) && has_adjacent_digits(code) {
            total += 1;
        }
    }
    total
}

fn part_2() -> usize {
    let mut total : usize = 0;
    for code in 235741..706948 {
        if is_not_decreasing(code) && has_two_adjacent_digits(code) {
            total += 1;
        }
    }
    total
}

fn is_not_decreasing(code: usize) -> bool {
    let digits = get_digits(code as u32);
    for i in 1..6 {
        if digits[i] < digits[i-1] {
            return false
        }
    }
    true
}

fn has_adjacent_digits(code: usize) -> bool {
    let digits = get_digits(code as u32);
    for i in 1..6 {
        if digits[i] == digits[i-1] {
            return true
        }
    }
    false
}

fn has_two_adjacent_digits(code: usize) -> bool {
    let digits = get_digits(code as u32);

    let mut previous_digit: u32 = 0;
    let mut streaks: Vec<u32> = Vec::new();
    let mut current_streak: u32 = 0;

    for i in 0..6 {
        if digits[i] == previous_digit {
            current_streak += 1;
        } else {
            streaks.push(current_streak);
            current_streak = 1;
        }
        previous_digit = digits[i];
    }

    streaks.push(current_streak);
    streaks.contains(&2)
}

fn get_digits(num: u32) -> Vec<u32> {
    num.to_string().chars().map(|d| d.to_digit(10).unwrap()).collect()
}