# Part one
import re

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

score = 0

for l in lines:
    f1, f2, s1, s2 = map(int, re.split(r'\W+', l))

    if (f1 <= s1 and f2 >= s2) or f1 >= s1 and f2 <= s2:
        score += 1


print(score)
