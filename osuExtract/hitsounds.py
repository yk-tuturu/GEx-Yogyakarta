import math

f = open("test.txt", "r")
result = ""
hitsound_set = set()
for line in f:
  parts = line.split(",")
  time = parts[1]
  time = float(time) + 3
  result += parts[0] + "," + str(time) + "," + parts[2][:-1] + "\n"

f.close()

with open("hd.txt", "w") as file:
  file.write(result)