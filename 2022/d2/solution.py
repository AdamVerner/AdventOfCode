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
  'X': 1,
  'Y': 2,
  'Z': 3,
}

for x in lines:
    o, m = x.split(' ')
    # print(o, m)

    score += ss[m]

    if o == same[m]:
        score += 3

    if beats[m] == o:
        score += 6

    print(score)

print(score)