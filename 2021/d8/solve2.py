import numpy as np

d = open('t.input').readlines()
d = open('input').readlines()


def glam(row, lamb):
    f = list(filter(lamb, row))
    assert len(f) == 1
    return f[0]    


def glen(row, len_):
    return glam(row, lambda x: len(x) == len_)


def slen(row, len_):
    return set(filter(lambda x: len(x) == len_, row))


p2 = 0

for row in d:

    l, r = row.strip().split(' | ')
    l = {frozenset(x) for x in l.split(' ')}
    
    lookup = list(range(10))

    lookup[1] = glen(l, 2)
    lookup[4] = glen(l, 4)
    lookup[7] = glen(l, 3)
    lookup[8] = glen(l, 7)

    # all have lenght 6
    n069 = slen(l, 6)

    # six does not have common side with number 1
    lookup[6] = glam(n069, lambda x: not lookup[1].issubset(x))
        
    # four is subset of nine
    lookup[9] = glam(n069, lambda x: lookup[4].issubset(x))

    lookup[0] = glam(n069, lambda x: x not in lookup)

    # 5 is subset of of 6 without one segment
    lookup[5] = glam(l - set(lookup), lambda x: x.issubset(lookup[6]), )

    # 3 is subset of nine and 2 is not
    lookup[3] = glam(l - set(lookup), lambda x: x.issubset(lookup[9]), )
    lookup[2] = (l - set(lookup)).pop()

    # print(lookup)
    
    i = [str(lookup.index(set(w))) for w in r.split(' ')]
    p2 += int(''.join(i))


print('p2 = ', p2)

