# Part one

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)

score = 0

for x in lines:
    o, m = x[:len(x)//2], x[len(x)//2:]
    a, *_ = set(o).intersection(set(m))
    if a.islower():
        score += ord(a) - ord('a') + 1
    else:
        score += ord(a) - ord('A') + 27
print(score)
