package problems

import (
	"strconv"
	"strings"
)

type Day2 struct{}

func (d Day2) Solve(input string) int {
	return numberOfPossibleGames(input)
}

func numberOfPossibleGames(input string) int {
	posGames := 0
	gameNo := 1
	splitter := func(r rune) bool {
		return r == ',' || r == ';'
	}

	for _, line := range strings.Split(input, NewLine) {
		game := line[strings.Index(line, ": ")+2:]
		possible := true

		for _, game := range strings.FieldsFunc(game, splitter) {
			game = strings.TrimSpace(game)
			inx := strings.LastIndex(game, " ")
			cnt, _ := strconv.Atoi(game[:inx])
			cube := game[inx+1:]

			if cube == "red" && cnt > 12 {
				possible = false
			} else if cube == "green" && cnt > 13 {
				possible = false
			} else if cube == "blue" && cnt > 14 {
				possible = false
			}
		}

		if possible {
			posGames += gameNo
		}
		gameNo++
	}

	return posGames
}
