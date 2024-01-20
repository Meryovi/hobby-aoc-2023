package problems

import (
	"strings"
)

type Day4 struct{}

func (d Day4) Solve(input string) int {
	return calculateScratchCardPoints(input)
}

func calculateScratchCardPoints(input string) int {
	cardPoints := 0

	for _, game := range strings.Split(input, NewLine) {
		gameSep := strings.Index(game, ":")
		numbSep := strings.Index(game, "|")

		found := game[gameSep+1 : numbSep-1]
		winners := game[numbSep+1:]

		gamePoints := 0

		for j := 0; j < len(found); j += 3 {
			if strings.Contains(winners, found[j:j+3]) {
				gamePoints = max(1, gamePoints*2)

			}
		}

		cardPoints += gamePoints
	}

	return cardPoints
}
