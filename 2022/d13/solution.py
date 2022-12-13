import re
import numpy as np
import networkx as nx

a = open('input').read()
a=a[:-1]

RightOrder = type('RightOrder', (Exception, ), {})
WrongOrder = type('WrongOrder', (Exception, ), {})


def compare(a, b):
    # print(f'compare {a} vs {b}')
    if isinstance(a, (list, tuple)) and isinstance(b, (list, tuple)):
        for l, r in zip(a, b):
            compare(l, r)

        if len(a) < len(b):
            raise RightOrder()

        if len(a) > len(b):
            raise WrongOrder()

    if isinstance(a, int) and isinstance(b, (list, tuple)):
        compare([a], b)

    if isinstance(a, (list, tuple)) and isinstance(b, int):
        compare(a, [b])

    if isinstance(a, int) and isinstance(b, int):
        if a < b:
            raise RightOrder()
        if a > b:
            raise WrongOrder()
        return None


def compare_check(a, b):
    try:
        compare(a, b)
    except RightOrder:
        return True
    except WrongOrder:
        return False


score = 0
packets = []

for i, p in enumerate(a.split('\n\n')):
    a, b = map(eval, p.split('\n'))
    packets.append(a)
    packets.append(b)

    if compare_check(a, b):
        # print('right order')
        score += i+1


print('part 1', score)


def compare_check(a, b):
    try:
        compare(a, b)
    except RightOrder:
        return -1
    except WrongOrder:
        return 1
    return 0

packets.append([[2]])
packets.append([[6]])

from functools import cmp_to_key

packets.sort(key=cmp_to_key(compare_check))

print('part 2', (packets.index([[2]]) + 1) * (packets.index([[6]]) + 1))
