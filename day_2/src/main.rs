fn main() {
    let input = include_str!("..\\input.txt");

    let mut totalscore_first = 0;
    let mut totalscore_second = 0;
    for line in input.lines() {
        let split: Vec<&str> = line.split(' ').collect();
        let enemy_val = split[0].chars().next().unwrap() as i32 - 65;
        let my_val = split[1].chars().next().unwrap() as i32 - 88;

        first(enemy_val, my_val, &mut totalscore_first);
        second(enemy_val, my_val, &mut totalscore_second);
    }
    println!("Totalscore for first is {}!", totalscore_first);
    println!("Totalscore for first is {}!", totalscore_second);
}

fn first(enemy_val: i32, my_val: i32, totalscore: &mut i32) {
    *totalscore += my_val + 1;
    if my_val == enemy_val + 1 || my_val == 0 && enemy_val == 2 {
        *totalscore += 6;
    } else if my_val == enemy_val {
        *totalscore += 3;
    }
}

fn second(enemy_val: i32, my_val: i32, totalscore: &mut i32) {
    if my_val == 0{
        let tmp_val = enemy_val - 1;
        *totalscore += if tmp_val == -1 {3} else {tmp_val + 1};
    } else if my_val == 1{
        *totalscore += 3 + enemy_val + 1;
    } else {
        *totalscore += 6 + ((enemy_val + 1) % 3 + 1);
    }
}
