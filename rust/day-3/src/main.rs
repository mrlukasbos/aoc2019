use std::fs;
use std::cmp;

#[derive(Debug)]
struct Point {
    x: isize, 
    y: isize, 
    steps: isize
}

fn main() {
    let contents = fs::read_to_string("input.txt").expect("Something went wrong reading the file");
    let lines: Vec<&str> = contents.lines().collect();
    let wire_1_str: Vec<&str> = lines[0].split(",").collect();
    let wire_2_str: Vec<&str> = lines[1].split(",").collect();
    let wire_1 = line_to_coordinate(wire_1_str);
    let wire_2 = line_to_coordinate(wire_2_str);

    println!("part 1:{}", part_1(&wire_1, &wire_2));
    println!("part 2:{}", part_2(&wire_1, &wire_2));
}

fn part_1(wire_1: &Vec<Point>, wire_2: &Vec<Point>) -> isize {
    let mut intersections = get_intersections(wire_1, wire_2);

    intersections.sort_by(|a, b| {
        let size_a = (a.x).abs() + (a.y).abs();
        let size_b = (b.x).abs() + (b.y).abs();
        size_a.cmp(&size_b)
    });

    let intersection_values: Vec<isize> = intersections.iter().map(|i| { 
        (i.x).abs() + (i.y).abs()
    }).collect();

    intersection_values[1] // take the second index because the first intersection is (0, 0)
}

fn part_2(wire_1: &Vec<Point>, wire_2: &Vec<Point>) -> isize {
    let intersections = get_intersections(wire_1, wire_2);
    intersections[1].steps
}

fn get_intersections(wire_1: &Vec<Point>, wire_2: &Vec<Point>) -> Vec<Point> {
    let mut intersections: Vec<Point> = Vec::new();

    for i in 1..wire_1.len() {
        let line_start = &wire_1[i-1];
        let line_end = &wire_1[i];

        for j in 1..wire_2.len() {
            let line_2_start = &wire_2[j-1];
            let line_2_end = &wire_2[j];

            if has_intersection(&line_start, &line_end, &line_2_start, &line_2_end) {
                let x: isize;
                let y: isize; 
                if line_start.x == line_end.x {  // vertical
                    x = line_start.x;
                } else { // horizontal
                    x = line_2_start.x;
                }

                if line_start.y== line_end.y { 
                    y = line_start.y;
                } else {
                    y = line_2_start.y;
                }

                let intersection_dist_1 = (x + y) - (line_start.x + line_start.y);
                let intersection_dist_2 = (x + y) - (line_2_start.x + line_2_start.y);
                let steps = line_start.steps + line_2_start.steps + intersection_dist_1.abs() + intersection_dist_2.abs();
                intersections.push(Point {x, y, steps });
            } 
        }
    }
    intersections
}


fn line_to_coordinate(strs: Vec<&str>) -> Vec<Point> { 
    let mut coordinates: Vec<Point> = Vec::new();
    let initial_point = Point { x: 0, y: 0, steps: 0};
    coordinates.push(initial_point);

    for cmd in strs {
        let char_vec: Vec<char> = cmd.chars().collect();
        let lst = coordinates.last().unwrap().clone();

        let value = cmd[1..].parse::<isize>().unwrap();
        let p = match char_vec[0] {
            'U' => Point {x: lst.x, y: lst.y + value, steps: lst.steps + value.abs()},
            'D' => Point {x: lst.x, y: lst.y - value, steps: lst.steps + value.abs()},
            'R' => Point {x: lst.x + value, y: lst.y, steps: lst.steps + value.abs()},
            'L' => Point {x: lst.x - value, y: lst.y, steps: lst.steps + value.abs()},
            _ => panic!("unkown input")
        };
        coordinates.push(p);
    }
    // println!("{:?}", coordinates);
    coordinates
}


fn on_segment(p: &Point, q: &Point, r: &Point) -> bool { 
    (q.x <= cmp::max(p.x, r.x) && q.x >= cmp::min(p.x, r.x) && q.y <= cmp::max(p.y, r.y) && q.y >= cmp::min(p.y, r.y))
} 

fn orientation(p: &Point, q: &Point, r: &Point) -> usize { 
    let val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y); 
  
    if val == 0 {   // colinear 
        0
    } else if val > 0 {
        1
    } else {
        2
    }
} 

fn has_intersection(p1: &Point, q1: &Point, p2: &Point, q2: &Point) -> bool { 
    // Find the four orientations needed for general and 
    // special cases 
    let o1 = orientation(p1, q1, p2); 
    let o2 = orientation(p1, q1, q2); 
    let o3 = orientation(p2, q2, p1); 
    let o4 = orientation(p2, q2, q1); 
  
    (o1 != o2 && o3 != o4) ||
    (o1 == 0 && on_segment(p1, p2, q1)) ||
    (o2 == 0 && on_segment(p1, q2, q1)) ||
    (o3 == 0 && on_segment(p2, p1, q2)) ||
    (o4 == 0 && on_segment(p2, q1, q2))
  } 