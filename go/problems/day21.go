package problems

import (
	"image"
	"slices"
	"strings"
)

type Day21 struct{}

func (d Day21) Solve(input string) int {
	return countNumberOfPlotsInSteps(input)
}

func countNumberOfPlotsInSteps(input string) int {
	lines := strings.Split(input, NewLine)
	size := len(lines)
	lastVisits := []image.Point{{X: size / 2, Y: size / 2}}

	for step := 0; step < 64; step++ {
		nextVisits := make([]image.Point, 0, len(lastVisits)*4)

		for _, visit := range lastVisits {
			for _, direction := range ALL_DIRECTIONS {
				next := visit.Add(direction)

				if next.Y >= size-1 || next.Y < 0 || next.X >= size-1 || next.X < 0 {
					continue
				}

				var tile = lines[next.Y][next.X]

				if tile != '#' && !slices.Contains(nextVisits, next) {
					nextVisits = append(nextVisits, next)
				}
			}
		}

		lastVisits = nextVisits
	}

	numberOfPlots := len(lastVisits)
	return numberOfPlots
}

var ALL_DIRECTIONS []image.Point = []image.Point{
	DIRECTION_UP,
	DIRECTION_DOWN,
	DIRECTION_LEFT,
	DIRECTION_RIGHT,
}
