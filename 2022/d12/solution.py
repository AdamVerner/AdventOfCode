import networkx.exception
import numpy as np
import networkx as nx

a = open('input').read()
a=a[:-1]

d = [[ord(x) - ord('a') for x in y]for y in a.split('\n')]
d = np.array(d)
s = a.index('S') // (d.shape[1] + 1), a.index('S') % (d.shape[1] + 1)
e = a.index('E') // (d.shape[1] + 1), a.index('E') % (d.shape[1] + 1)
d[s] = 0
d[e] = ord('z') - ord('a')

g = nx.DiGraph()


for x1 in range(0, d.shape[0]):
    for y1 in range(0, d.shape[1]):
        for x2,y2 in (
                (x1 + 1, y1),
                (x1 - 1, y1),
                (x1, y1 + 1),
                (x1, y1 - 1),
        ):
            if x1 not in range(d.shape[0]) or x2 not in range(d.shape[0]):
                continue
            if y1 not in range(d.shape[1]) or y2 not in range(d.shape[1]):
                continue

            if d[x1, y1] + 1 >= d[x2, y2]:
                g.add_edge(f'{x1}-{y1}', f'{x2}-{y2}')

print('p1=', len(nx.shortest_path(g, f'{s[0]}-{s[1]}', f'{e[0]}-{e[1]}')) - 1)

ms = []
for x, y in np.nditer(np.where(d == 0)):
    try:
        ms.append(len(nx.shortest_path(g, f'{x}-{y}', f'{e[0]}-{e[1]}')) - 1)
    except networkx.exception.NetworkXNoPath:
        pass

print('p2 = ', min(ms))
