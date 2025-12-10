import scipy
import sys

def parse2(raw: str):
    for line in raw.splitlines():
        _, *wirings_str_list, joltage_str = line.split(' ')
        wirings = [[int(x) for x in wirings_str[1:-1].split(",")] for wirings_str in wirings_str_list]
        joltages = [int(x) for x in joltage_str[1:-1].split(',')]
        yield wirings, joltages

def calc_cost2(joltages: tuple[int, ...], wiring_indexes: list[tuple[int, ...]]):
    wirings = [[int(i in b) for i in range(len(joltages))] for b in wiring_indexes]
    c = [1] * len(wirings)
    A_eq = list(map(list, zip(*wirings)))
    b_eq = joltages

    result = scipy.optimize.linprog(c=c, A_eq=A_eq, b_eq=b_eq, integrality=1)
    return int(result.fun)


def part2(raw: str):
    return

if __name__ == '__main__':
    inp = parse2(open(sys.argv[1]).read())
    total = sum(calc_cost2(joltages, wirings) for wirings, joltages in inp)
    print(total)
