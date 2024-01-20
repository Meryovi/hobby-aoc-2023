package problems

import (
	"image"
	"math"
	"strings"
)

type Day10 struct{}

func (d Day10) Solve(input string) int {
	return findFarthestTileFromStart(input)
}

var DIRECTION_RIGHT = image.Point{X: 1, Y: 0}
var DIRECTION_LEFT = image.Point{X: -1, Y: 0}
var DIRECTION_DOWN = image.Point{X: 0, Y: 1}
var DIRECTION_UP = image.Point{X: 0, Y: -1}

func findFarthestTileFromStart(input string) int {
	matrix := strings.Split(input, NewLine)
	start := image.Point{}

	for y, line := range matrix {
		if x := strings.Index(line, "S"); x >= 0 {
			start.X = x
			start.Y = y
			break
		}
	}

	steps := 0
	dir := DIRECTION_RIGHT
	loc := start.Add(dir)

	for loc != start {
		pipe := matrix[loc.Y][loc.X]

		if pipe == '|' || pipe == '-' {
			loc = loc.Add(dir)
		} else if pipe == 'L' && dir == DIRECTION_DOWN {
			loc, dir = loc.Add(DIRECTION_RIGHT), DIRECTION_RIGHT
		} else if pipe == 'L' && dir == DIRECTION_LEFT {
			loc, dir = loc.Add(DIRECTION_UP), DIRECTION_UP
		} else if pipe == 'J' && dir == DIRECTION_DOWN {
			loc, dir = loc.Add(DIRECTION_LEFT), DIRECTION_LEFT
		} else if pipe == 'J' && dir == DIRECTION_RIGHT {
			loc, dir = loc.Add(DIRECTION_UP), DIRECTION_UP
		} else if pipe == '7' && dir == DIRECTION_UP {
			loc, dir = loc.Add(DIRECTION_LEFT), DIRECTION_LEFT
		} else if pipe == '7' && dir == DIRECTION_RIGHT {
			loc, dir = loc.Add(DIRECTION_DOWN), DIRECTION_DOWN
		} else if pipe == 'F' && dir == DIRECTION_UP {
			loc, dir = loc.Add(DIRECTION_RIGHT), DIRECTION_RIGHT
		} else if pipe == 'F' && dir == DIRECTION_LEFT {
			loc, dir = loc.Add(DIRECTION_DOWN), DIRECTION_DOWN
		} else {
			loc = start
		}

		steps++
	}

	mid := int(math.Ceil(float64(steps) / 2))
	return mid
}
