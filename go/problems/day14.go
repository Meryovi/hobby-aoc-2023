package problems

import "strings"

type Day14 struct{}

func (d Day14) Solve(input string) int {
	return tiltPlatformAndSumRockWeight(input)
}

func tiltPlatformAndSumRockWeight(input string) int {
	matrix := make([][]rune, 0)

	for _, line := range strings.Split(input, NewLine) {
		matrix = append(matrix, []rune(line))
	}

	sum := 0
	height := len(matrix)
	width := len(matrix[0])

	for j := 0; j < height; j++ {
		for i := 0; i < width; i++ {
			if matrix[j][i] != 'O' {
				continue
			}

			sum += height - j
			moves := 0

			for k := j - 1; k >= 0; k-- {
				if matrix[k][i] != '.' {
					break
				}

				matrix[k][i], matrix[j-moves][i] = matrix[j-moves][i], matrix[k][i]

				sum++
				moves++
			}
		}
	}

	return sum
}
