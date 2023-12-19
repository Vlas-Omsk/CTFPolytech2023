#!/bin/python3

flag = open('flag.txt', 'r').readline()
flagNum = 0

for iter in range(len(flag)):
  flagNum = flagNum | (ord(flag[iter]) << 7*iter)

open('out.txt', 'w').write(hex(flagNum));

