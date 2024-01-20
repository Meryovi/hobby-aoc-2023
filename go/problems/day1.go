package problems

import (
	"strconv"
	"strings"
)

type Day1 struct{}

func (d Day1) Solve(input string) int {
	return sumFirstAndLastDigits(input)
}

func sumFirstAndLastDigits(input string) int {
	sum := 0
	for _, line := range strings.Split(input, NewLine) {
		d1, d2 := 0, 0

		for i, j := 0, len(line)-1; j >= 0 && (d1 == 0 || d2 == 0); i, j = i+1, j-1 {
			if d1 == 0 {
				if n, err := strconv.Atoi(line[i : i+1]); err == nil {
					d1 = n
				}
			}
			if d2 == 0 {
				if n, err := strconv.Atoi(line[j : j+1]); err == nil {
					d2 = n
				}
			}
		}

		sum += d1*10 + d2
	}
	return sum
}
