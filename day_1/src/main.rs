use std::{
    fs::File,
    io::{prelude::*, BufReader},
    path::Path,
};

fn lines_from_file(filename: impl AsRef<Path>) -> Vec<String> {
    let file = File::open(filename).expect("no such file");
    let buf = BufReader::new(file);
    buf.lines()
        .map(|l| l.expect("Could not parse line"))
        .collect()
}


fn first() {
    let input = lines_from_file("input.txt");

    let mut max_cal: i32 = 0;
    let mut curr_elf: i32 = 0;
    for input_str in input.iter(){
        if !input_str.is_empty(){
            curr_elf += input_str.parse::<i32>().unwrap();
        }
        else{
            if max_cal < curr_elf{
                max_cal = curr_elf;
            }
            curr_elf = 0;
        }
    }
    println!("Max is: {}", max_cal);
}

fn second(){
    let input = lines_from_file("input.txt");

    let mut max_cal = vec![0,0,0];
    let mut curr_elf: i32 = 0;
    for input_str in input.iter(){
        if !input_str.is_empty(){
            curr_elf += input_str.parse::<i32>().unwrap();
        }
        else {
            if max_cal[0] < curr_elf || max_cal[1] < curr_elf || max_cal[2] < curr_elf{
                max_cal.sort(); 
                max_cal[0] = curr_elf;
            }

            curr_elf = 0;
        }
    }
    
    let sum: i32 = max_cal.iter().sum();
    println!("Sum is: {}", sum);
}

fn main() {
    first();
    second();
}
