# Part one
import re

a = open('input').read()
a=a[:-1]

m = []


def get_op(f1, o, f2):
    def op(old):
        i1 = old if f1 == 'old' else int(f1)
        i2 = old if f2 == 'old' else int(f2)
        return (i1 + i2 if o == '+' else i1 * i2)
    return op

prod = 1
for mn in a.split('\n\n'):

    items = list(map(int, mn.split('\n')[1].split(': ')[1].split(', ')))

    f1, o, f2 = mn.split('\n')[2].split('= ')[1].split(' ')
    print('f1, o, f2', f1, o, f2)
    op = get_op(f1, o, f2)

    div = int(mn.split('\n')[3].split(' ')[-1])
    prod *= div

    if_true = int(mn.split('\n')[4].split(' ')[-1])
    if_false = int(mn.split('\n')[5].split(' ')[-1])

    m.append({
        'items': items,
        'op': op,
        'div': div,
        'if_true': if_true,
        'if_false': if_false,
        'ops': 0
    })

for r in range(10000):
    print('\nround ', r)
    for idx, mon in enumerate(m):
        for x in mon['items']:
            mon['ops'] += 1

            new = mon['op'](x) % prod

            if new % mon['div'] == 0:
                m[mon['if_true']]['items'].append(new)
            else:
                m[mon['if_false']]['items'].append(new)

        mon['items'] = []

    # for idx, mon in enumerate(m):
    #     print(idx, mon)

print(sorted([mon['ops'] for mon in m]))
s = list(sorted([mon['ops'] for mon in m]))
print(s[-2] * s[-1])