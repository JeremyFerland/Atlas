# Atlas


### Contributors

- Charles-Olivier Bélanger
- Jérémy Ferland
- Marc-Olivier Héroux-Hould

### UDP Messages

#### Unity
	
	Incoming port : 5556

	Floor symbols : /Symbol[x] [int]
	Incoming door : /Door[x] [bool]

	Output port : 55555
	
	Road : /Road[x] [float] [float]
	Road hit : /RoadHit [int] (2 = good road // 1 = one road destroy // 0 = wrong road connected)
	Finish : /Success int (1 = finish)
	
#### Max MSP

	/Road[x] [int] [int] (x,y)
	/Door[x] [bool]
	/Success [int]
	/RoadHit [int] (2 = good road // 1 = one road destroy // 0 = wrong road connected)
	/Symbol[x] [int]