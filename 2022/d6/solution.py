l = open('input').readline()

p1 = [4 == len(set(l[x:x + 4])) for x in range(len(l))]
p2 = [14 == len(set(l[x:x + 14])) for x in range(len(l))]
print('p1 ', p1.index(True) + 4)
print('p2 ', p2.index(True) + 14)
