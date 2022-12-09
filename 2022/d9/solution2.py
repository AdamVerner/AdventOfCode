import re

import numpy as np

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]

v = np.zeros((1000,1000), dtype=int)
h = np.array([500, 500], dtype=int)
t = np.array([[500, 500] for x in range(9)], dtype=int)
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

for d, _, *steps in lines:
    steps = ''.join(steps)
    for step in range(int(steps)):
        h[1] += 1 if d == 'U' else 0
        h[1] += -1 if d == 'D' else 0
        h[0] += 1 if d == 'R' else 0
        h[0] += -1 if d == 'L' else 0

        b = h

        for x in range(9):
            t[x] += move_map[tuple((t[x] - b))]
            b = t[x]

        v[b[1], b[0]] += 1

print(np.where(v >= 1)[0].shape[0])
