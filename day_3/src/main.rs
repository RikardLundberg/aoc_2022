fn main() {
    first();
    second();
}

fn contains(line: &str, ch: char) -> bool {
    for ch2 in line.chars() {
        if (ch == ch2) {
            return true;
        }
    }
    return false;
}

fn first() {
    let input = include_str!("..\\input.txt");
    let mut totalscore = 0;

    for line in input.lines() {
        let leng = line.len();
        let (comp1, comp2) = line.split_at(leng / 2);

        for ch in comp1.chars() {
            if contains(comp2, ch) {
                let mut score = ch as i32;
                if (score > 96) {
                    score -= 96;
                } else {
                    score -= 38;
                }
                totalscore += score;
                break;
            }
        }
    }
    println!("Totalscore for first is {}!", totalscore);
}

fn second() {
    let input = include_str!("..\\input.txt");
    let mut totalscore = 0;
    let mut groupCount = 0;
    let mut elf_group: Vec<&str> = vec![];

    for line in input.lines() {
        elf_group.push(line);
        if groupCount < 2 {
            groupCount += 1;
            continue;
        }
        groupCount = 0;

        for ch in elf_group[0].chars() {
            if contains(elf_group[1], ch) {
                if contains(elf_group[2], ch) {
                    let mut score = ch as i32;
                    if score > 96 {
                        score -= 96;
                    } else {
                        score -= 38;
                    }
                    totalscore += score;
                    break;
                }
            }
        }
        elf_group.clear();
    }
    
    println!("Totalscore for second is {}!", totalscore);
}
