import re

import numpy as np

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

v = np.zeros((1000,1000), dtype=int)

h = np.array([500, 500], dtype=int)
t = np.array([500, 500], dtype=int)
move_map = {
    (-2,-2):  (1,1),
    (-2, -1): (1, 1),
    (-2, 0): (1, 0),
    (-2, 1): (1, -1),
    (-2, 2): (1, -1),
    (2, -2): (-1, 1),
    (2, -1): (-1, 1),
    (2, 0):  (-1, 0),
    (2, 1):  (-1, -1),
    (2, 2):  (-1, -1),

    (-1, -2): (1, 1),
    (-1, -1): (0, 0),
    (-1, 0): (0, 0),
    (-1, 1): (0, 0),
    (-1, 2): (1, -1),

    (1, -2): (-1, 1),
    (1, -1): (0, 0),
    (1, 0): (0, 0),
    (1, 1): (0, 0),
    (1, 2): (-1, -1),

    (0, -2): (0, 1),
    (0, -1): (0, 0),
    (0, 0): (0, 0),
    (0, 1): (0, 0),
    (0, 2): (0, -1),
}
print(move_map)

for d, _, *steps in lines:
    steps = ''.join(steps)
    print(d, steps, h)
    for step in range(int(steps)):
        h[1] += 1 if d == 'U' else 0
        h[1] += -1 if d == 'D' else 0
        h[0] += 1 if d == 'R' else 0
        h[0] += -1 if d == 'L' else 0

        print(tuple((t-h)), move_map[tuple((t-h))])

        # s = np.zeros((10, 10), dtype=int)
        # s[h[1], h[0]] = 1
        # s[t[1], t[0]] = 2

        #  print('\n'.join([''.join(map(str, x))  for x in s]))
        # print('\n'.join([''.join(map(str, x))  for x in s][::-1]))

        t += move_map[tuple((t-h))]
        v[t[1], t[0]] += 1
        print('move', move_map[tuple((t-h))])

        print('>>> ', h, t)

print('\n'.join([''.join(map(lambda x: '#' if x else '.', x))  for x in v][::-1]))


print(np.where(v >= 1)[0].shape[0])