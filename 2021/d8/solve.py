import numpy as np

# d = open('t.input').readlines()
# d = open('input').readlines()

def glen(row, len_):
    return next(filter(lambda x: len(x) == len_, row))

def slen(row, len_):
    return list(filter(lambda x: len(x) == len_, row))



p1, p2 = 0, 0


row = 'acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf'

l, r = row.strip().split(' | ')
l = [set(x) for x in l.split(' ')]


mapping = {}

n1 = glen(l, 2)
l.remove(n1)
assert {'a', 'b'} == n1
n4 = glen(l, 4)
l.remove(n4)
assert {'e', 'a', 'f', 'b'} == n4
n7 = glen(l, 3)
l.remove(n7)
assert {'d', 'a', 'b'} == n7
n8 = glen(l, 7)
l.remove(n8)
assert {'a', 'c', 'e', 'd', 'g', 'f', 'b'} == n8

# print('n1 =', ''.join(sorted(n1)))
# print('n4 =', ''.join(sorted(n4)))
# print('n7 =', ''.join(sorted(n7)))
# print('n8 =', ''.join(sorted(n8)))


# all have lenght 6
n069 = slen(l, 6)

# six does not have common side with number 1
n6 = next(filter(lambda x: not n1.issubset(x), n069))
print('n6 =', ''.join(sorted(n6)))
l.remove(n6)
assert {'c', 'd', 'f', 'g', 'e', 'b'} == n6
    
# four is subset of nine
n9 = next(filter(lambda x: n4.issubset(x), n069))
print('n9 =', ''.join(sorted(n9)))
l.remove(n9)
assert {'c', 'e', 'f', 'a', 'b', 'd'} == n9

n0 = next(filter(lambda x: x not in (n6, n9), n069))
l.remove(n0)
assert {'c', 'a', 'g', 'e', 'd', 'b'} == n0


# 5 is subset of of 6 without one segment
n5 = next(filter(lambda x: x.issubset(n6), l))
l.remove(n5)
assert {'c', 'd', 'f', 'b', 'e'} == n5

# 3 is subset of nine and 2 is not
n3 = next(filter(lambda x: x.issubset(n9), l))
l.remove(n3)
n2 = l[0]
l.remove(n2)

assert {'f', 'b', 'c', 'a', 'd'} == n3
assert {'g', 'c', 'd', 'f', 'a'} == n2
assert not l


exit(1)



## digits 1, 4, 7, 8 are unqiue in lenght of characters


for row in d:
    print('row = ', repr(row))
    l, r = row.strip().split(' | ')
    print('r = ', repr(r))

    #     generate maping

    s = 0
    n = 1
    for w in r.split(' '):
        print(w)
        w = ''.join(sorted(w))
        d = mn[w] 

        print(d)

        s += d * n
        n *= 10

    print(s)
    p2 += s


print('p1 = ', p1)
print('p2 = ', p2)

