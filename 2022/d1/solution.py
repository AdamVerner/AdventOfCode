# Part one

with open('input') as data:
    d = data.read()[:-1]
    t = [sum([int(y) for y in x.split('\n')]) for x in d.split('\n\n')]
    # for x in d.split('\n\n'):
    print('Part one =', max(t))

    s = sorted([sum([int(y) for y in x.split('\n')]) for x in d.split('\n\n')], reverse=True)
    print('Part one =', sum(s[0:3]))
