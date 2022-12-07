# Part one
import re

a = open('input').read()
a=a[:-1]
lines = [x for x in a.split('$ ')][1:]
print(lines)

score = 0

path = []
dir_sizes = {}
for l in lines:
    cmd, *rest = l.split('\n')
    # print(cmd, rest)

    if cmd == 'ls':
        for f in rest:
            if f == '':
                continue
            fs, fn = f.split(' ')
            if fs == 'dir':
                continue
            for pl in range(len(path)):
                dir_sizes.setdefault('/'.join(path[:pl+1]), 0)
                dir_sizes['/'.join(path[:pl+1])] += int(fs)

    else:  # cmd == 'cd
        _, p = cmd.split(' ')
        if p == '/':
            path = ['/']
        elif p == '..':
            path.pop()
        else:
            path.append(p)
    pass


ss = sum(v for v in dir_sizes.values() if v <= 100000)
print('Part one ', ss)

missing = 30000000 - 70000000 + dir_sizes['/']
print('Part two ', min([s for p, s in dir_sizes.items() if s >= missing]))
