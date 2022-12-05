# Part one
import re

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]

score = 0

W = 9

stack = ['' for x in range(W)]

o = 0
for l in lines:
    o += 1
    if any([x in l for x in '0123456789']):
        break

    for x in range(0, len(l), 4):
        stack[x//4] += l[x+1]

stack = [str.lstrip(x, ' ') for x in stack]
stack2 = stack.copy()


for l in lines[o+1:]:
    c, origin, dest = map(int, re.search(r'move (\d+) from (\d) to (\d)', l).groups())
    stack[dest - 1] = stack[origin-1][:c][::-1] + stack[dest - 1]
    stack[origin - 1] = stack[origin - 1][c:]

    stack2[dest - 1] = stack2[origin-1][:c] + stack2[dest - 1]
    stack2[origin - 1] = stack2[origin - 1][c:]

print('part 1:', ''.join([x[0] for x in stack]))
print('part 2:', ''.join([x[0] for x in stack2]))
