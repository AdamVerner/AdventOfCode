# Part one

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('\n')]
print(lines)


score = 0

beats = {
  'X': 'C',
  'Y': 'A',
  'Z': 'B',
}

same = {
  'X': 'A',
  'Y': 'B',
  'Z': 'C',
}

ss = {
  'A': 1,
  'B': 2,
  'C': 3,
}

lose = {
    'A': 'C',
    'B': 'A',
    'C': 'B',
}

win = {
    'A': 'B',
    'B': 'C',
    'C': 'A',
}

for x in lines:
    o, m = x.split(' ')
    print(o, m)

    if m == 'X':
        score += 0
        score += ss[lose[o]]
    if m == 'Y':
        score += 3
        score += ss[o]
    if m == 'Z':
        score += ss[win[o]]
        score += 6

    print(score)

print(score)