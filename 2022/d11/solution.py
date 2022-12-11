# Part one
import re

a = open('input').read()
a=a[:-1]

m = []
for mn in a.split('\n\n'):

    items = list(map(int, mn.split('\n')[1].split(': ')[1].split(', ')))
    op = mn.split('\n')[2].split('= ')[1]
    op = eval('lambda old: (' + op + ') // 3')
    div = int(mn.split('\n')[3].split(' ')[-1])
    if_true = int(mn.split('\n')[4].split(' ')[-1])
    if_false = int(mn.split('\n')[5].split(' ')[-1])

    print('items', items)
    print('op', op)

    m.append({
        'items': items,
        'op': op,
        'div': div,
        'if_true': if_true,
        'if_false': if_false,
        'ops': 0
    })

for r in range(20):
    print('\nround ', r)
    for idx, mon in enumerate(m):
        print(idx, mon)
        for old in mon['items']:
            mon['ops'] += 1
            new = mon['op'](old)

            if new % mon['div'] == 0:
                print('divisible', mon['if_true'])
                m[mon['if_true']]['items'].append(new)
            else:
                print('indivisible', mon['if_false'])
                m[mon['if_false']]['items'].append(new)

        mon['items'] = []

    for idx, mon in enumerate(m):
        print(idx, mon)
    print()
    print()

print(sorted([mon['ops'] for mon in m]))
s = list(sorted([mon['ops'] for mon in m]))
print(s[-2] * s[-1])