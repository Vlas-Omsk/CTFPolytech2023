#!/bin/python3
#pip install gmpy2 pycryptodome

from Crypto.Util.number import *
import gmpy2
import binascii
import random

def gcd(a, b):
   if a < b:
     a, b = b, a
   while b != 0:
     temp = a % b
     a = b
     b = temp

   return a


alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ{}_1234567890";
bits = math.ceil(math.log(len(alphabet), 2))

data = open('flag.txt').readline()[:-1]
hex_data = 0
for iter in range(len(data)):
  hex_data |= alphabet.find(data[iter]) << bits*iter

p = getPrime(100)
q = getPrime(100)
n = p*q
phi = (p-1)*(q-1)
e = getPrime(100)

while e < phi:
  test = gcd(e, phi)
  if test == 1:
      break;

  e += 1;

d = gmpy2.invert(e, phi)

open('out.txt', 'w').write(f'{hex(n)[2:]}:{hex(d)[2:]}:{hex(pow(hex_data, d, n))[2:]}')
