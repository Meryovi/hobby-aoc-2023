package problems

import "strings"

type Day13 struct{}

func (d Day13) Solve(input string) int {
	return sumOfReflectionValues(input)
}

func sumOfReflectionValues(input string) int {
	sum := 0

	for _, rawPuzzle := range strings.Split(input, NewLine+NewLine) {
		puzzle := strings.Split(rawPuzzle, NewLine)

		value := getHorizontalMirrorValue(puzzle)
		if value < 0 {
			value = getVerticalMirrorValue(puzzle)
		}

		sum += value
	}

	return sum
}

func getHorizontalMirrorValue(puzzle []string) int {
	height := len(puzzle)
	width := len(puzzle[0])

	for i := 0; i < height-1; i++ {
		for ji, jd := i+1, i; ; jd, ji = (jd - 1), (ji + 1) {
			reflections := 0

			for x := 0; x < width; x++ {
				if puzzle[jd][x] != puzzle[ji][x] {
					break
				}

				reflections++
			}

			if reflections != width {
				break
			}

			if jd == 0 || ji == height-1 {
				return (i + 1) * 100
			}
		}
	}

	return -1
}

func getVerticalMirrorValue(puzzle []string) int {
	height := len(puzzle)
	width := len(puzzle[0])

	for i := 0; i < width-1; i++ {
		for ji, jd := i+1, i; ; jd, ji = (jd - 1), (ji + 1) {
			reflections := 0

			for y := 0; y < height; y++ {
				if puzzle[y][ji] != puzzle[y][jd] {
					break
				}

				reflections++
			}

			if reflections != height {
				break
			}

			if jd == 0 || ji == width-1 {
				return i + 1
			}
		}
	}

	return -1
}
