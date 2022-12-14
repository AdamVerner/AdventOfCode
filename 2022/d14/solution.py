import re
import numpy as np
import networkx as nx

a = open('input').read()
a=a[:-1]

v = [[[int(z) for z in y.split(',')] for y in x.split(' -> ')] for x in a.split('\n')]
blocked = set()

for l in v:
    for i in range(len(l)-1):
        [a, b] = l[i:i+2]

        if a[0] == b[0]:
            s, e = sorted([a[1],b[1]])
            for x in range(s, e + 1):
                blocked.add((a[0], x))

        if a[1] == b[1]:
            s, e = sorted([a[0],b[0]])
            for x in range(s, e+1):
                blocked.add((x, a[1]))

low_line = max(b for a, b in blocked) + 2

for x in range(0, 1000):
    blocked.add((x, low_line))

sand_count_p1 = None
sand_count = 1
while True:
    sand = (500, 0)

    while True:

        if sand[1] == low_line - 1 and sand_count_p1 is None:
            sand_count_p1 = sand_count - 1

        if (sand[0], sand[1]+1) in blocked:

            if (sand[0] - 1, sand[1] + 1) not in blocked:
                sand = (sand[0] - 1, sand[1] + 1)

            elif (sand[0] + 1, sand[1]+1) not in blocked:
                sand = (sand[0] + 1, sand[1] + 1)

            else:
                if sand[1] == 0:
                    print('Part 1', sand_count_p1)
                    print('Part 2', sand_count)
                    exit()
                blocked.add(sand)
                break
        else:
            sand = (sand[0], sand[1]+1)
    sand_count += 1
