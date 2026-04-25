import math

sounds = {
  "0": "soft-hitnormal",
  "2": "soft-hitwhistle",
  "4": "soft-hitfinish",
  "8": "soft-hitclap"
}

f = open("osu.txt", "r")
result = ""
for line in f:
  x = int(line.split(",")[0])
  column = math.floor(x * 7 / 512) 

  time = int(line.split(",")[2])
  time = (time / 1000)

  hitsoundArray = line.split(",")[5].split(":")
  hitsound = hitsoundArray[4]
  
  result = result + str(column) + "," + str(time) + "," + hitsound[:-5] + '\n'
f.close()

with open("map.txt", "w") as file:
  file.write(result)