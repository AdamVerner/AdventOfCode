import re

import numpy as np

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)


screen = np.zeros((6, 40))
c = 0
x = 1
for l in lines:

    d = np.zeros((40,))
    d[x-1:x+2] = 1
    screen[c // 40, c % 40] = d[ c % 40 ]

    if l == 'noop':
        pass
    else:
        c += 1
        screen[c // 40, c % 40] = d[c % 40]
        i, v = l.split(' ')
        x += int(v)
    c += 1

print('cycles = ', c)
print(x)
print(*[''.join(map(lambda x: '# ' if x else '. ', x)) for x in screen], sep='\n')
