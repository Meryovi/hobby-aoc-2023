package problems

import (
	"slices"
	"strconv"
	"strings"
)

type Day7 struct{}

func (d Day7) Solve(input string) int64 {
	return calculateTotalCardWinnings(input)
}

func calculateTotalCardWinnings(input string) int64 {
	draws := make([]cardDraw, 0)

	for _, game := range strings.Split(input, NewLine) {
		draw := parseCardDraw(game)
		draws = append(draws, draw)
	}

	slices.SortFunc(draws, func(a cardDraw, b cardDraw) int {
		return int(a.DrawValue - b.DrawValue)
	})

	var winnings int64 = 0

	for rank := 1; rank <= len(draws); rank++ {
		winnings += int64(draws[rank-1].Bid * rank)
	}

	return winnings
}

type cardDraw struct {
	DrawValue int64
	Bid       int
}

func parseCardDraw(drawStr string) cardDraw {
	sep := strings.Index(drawStr, " ")

	bid, _ := strconv.Atoi(drawStr[sep+1:])
	draw := drawStr[:sep]
	drawVal := getDrawValue(draw)

	return cardDraw{DrawValue: drawVal, Bid: bid}
}

const cardTypes = "23456789TJQKA"

func getDrawValue(draw string) int64 {
	cardCnt := make(map[rune]int, 13)

	maxCount, typeCount, drawValue := 0, 0, 0

	for _, card := range draw {
		cnt, exists := cardCnt[card]
		if !exists {
			typeCount++
		}
		cardCnt[card] = cnt + 1
		maxCount = max(maxCount, cardCnt[card])
		drawValue = (drawValue * 100) + (strings.Index(cardTypes, string(card)) + 10)
	}

	drawType := 1

	if typeCount == 1 {
		drawType = 7
	} else if typeCount == 2 && maxCount == 4 {
		drawType = 6
	} else if typeCount == 2 {
		drawType = 5
	} else if typeCount == 3 && maxCount == 3 {
		drawType = 4
	} else if typeCount == 3 {
		drawType = 3
	} else if typeCount == 4 {
		drawType = 2
	}

	return int64(drawType*10_000_000_000 + drawValue)
}
