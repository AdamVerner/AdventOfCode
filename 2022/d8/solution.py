import re
import numpy as np

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
t = np.array([np.array([int(y) for y in x]) for x in a.split('\n')])


visible = sum(t.shape * 2) - 4
print(visible)

score = []

for x in range(1, t.shape[0]-1):
    for y in range(1, t.shape[1]-1):
        c = t[x, y]
        visible += any([np.all(t[:x, y] < c), np.all(t[x+1:, y] < c), np.all(t[x, :y] < c), np.all(t[x, y+1:] < c)])
        score.append(
            min(np.argmax(np.append(t[:x, y][::-1] >= c, [True]))+1, x) *
            min(np.argmax(np.append(t[x+1:, y]     >= c, [True]))+1, t.shape[0] - x - 1) *
            min(np.argmax(np.append(t[x, :y][::-1] >= c, [True]))+1, y) *
            min(np.argmax(np.append(t[x, y+1:]     >= c, [True]))+1, t.shape[1] - y - 1)
        )

print('Part 1 ', visible)
print('Part 2 ', max(score))

