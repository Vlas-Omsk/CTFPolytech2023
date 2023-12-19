#!/bin/python3
import sys

key = [int(num) for num in open('key.txt', 'r').read().replace('\n', ' ').split(' ')[:-1]]

try:
  rounds = int(sys.argv[1])
except:
  rounds = 546935

with open('out.txt', 'w') as out:
  for sym in open('flag.txt', 'r').readline():
    byte = ord(sym)
    for o in range(rounds):
      for k in key:
        byte = byte^k

    out.write(hex(byte)[2:])

