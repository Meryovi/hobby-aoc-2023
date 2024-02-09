package problems

type Day15 struct{}

func (d Day15) Solve(input string) int {
	return sumOfHashValues(input)
}

func sumOfHashValues(input string) int {
	sum := 0

	value := 0
	for i := 0; i < len(input); i++ {
		if input[i] == ',' {
			sum += value
			value = 0
			continue
		}

		value += int(input[i])
		value = value * 17 % 256
	}
	sum += value

	return sum
}
