# Part one
import re

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

score = 0

for l in lines:
    f1, f2, s1, s2 = map(int, re.split(r'\W+', l))
    if f1 <= s2 and s1 <= f2:
        score += 1

print(score)
