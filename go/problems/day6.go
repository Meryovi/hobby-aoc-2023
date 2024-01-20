package problems

import (
	"strings"
)

type Day6 struct{}

func (d Day6) Solve(input string) int {
	return numberOfWaysToWin(input)
}

func numberOfWaysToWin(input string) int {
	w2winProd := 0

	lines := strings.Split(input, NewLine)
	times := parseNumbers(lines[0][strings.Index(lines[0], ":")+1:], "  ")
	dists := parseNumbers(lines[1][strings.Index(lines[1], ":")+1:], "  ")

	for i, time := range times {
		w2win := 0

		for j := 1; j < time-1; j++ {
			if j*(time-j) > dists[i] {
				w2win++
			}
		}

		w2winProd = max(w2winProd, 1) * max(w2win, 1)
	}

	return w2winProd
}
