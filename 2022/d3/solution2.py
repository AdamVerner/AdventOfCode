# Part one

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

score = 0

for x in range(0, len(lines), 3):
    x1,x2,x3 = map(set, lines[x:x+3])
    a, *_ = x1.intersection(x2).intersection(x3)
    if a.islower():
        score += ord(a) - ord('a') + 1
    else:
        score += ord(a) - ord('A') + 27
print(score)
