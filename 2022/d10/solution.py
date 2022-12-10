import re

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

c = 0
x = 1
s = 0

for l in lines:
    c += 1

    if c in (20, 60, 100, 140, 180, 220):
        print('c = ', c, 'x=', x, c * x)
        s += c * x
    if l == 'noop':
        pass
    else:
        c += 1
        if c in (20, 60, 100, 140, 180, 220):
            print('c = ', c, 'x=', x)
            s += c * x

        i, v = l.split(' ')
        x += int(v)

print('cycles = ', c)
print(x)
print('s', s)
