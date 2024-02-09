package problems

import (
	"image"
	"math"
	"strconv"
	"strings"
)

type Day17 struct{}

func (d Day17) Solve(input string) int {
	return calculateMinimumHeatLoss(input)
}

func calculateMinimumHeatLoss(input string) int {
	matrix := strings.Split(input, NewLine)
	size := len(matrix)

	q := NewPriorityQueue[crucible](size * size * 2)
	history := make(map[crucible]bool)

	minLoss := math.MaxInt

	partsFactory := image.Point{size - 1, size - 1}
	q.Push(newCrucible(DIRECTION_DOWN), 1)
	q.Push(newCrucible(DIRECTION_LEFT), 1)

	for q.Len() > 0 {
		cru, heat := q.Pop()
		if cru.Loc == partsFactory {
			minLoss = heat
			break
		}

		for _, dir := range NAVIGABLE_DIRECTIONS[cru.Dir] {
			newCru := cru.TryPush(dir, size, 3)
			if newCru != nil {
				cruVal := *newCru
				if _, exists := history[cruVal]; !exists {
					history[cruVal] = true
					nextHeat, _ := strconv.Atoi(string(matrix[cru.Loc.Y][cru.Loc.X]))
					q.Push(newCru, heat+nextHeat)
				}
			}
		}
	}

	return minLoss
}

var NAVIGABLE_DIRECTIONS = map[image.Point][]image.Point{
	DIRECTION_UP:    {DIRECTION_LEFT, DIRECTION_RIGHT, DIRECTION_UP},
	DIRECTION_DOWN:  {DIRECTION_LEFT, DIRECTION_RIGHT, DIRECTION_DOWN},
	DIRECTION_LEFT:  {DIRECTION_UP, DIRECTION_DOWN, DIRECTION_LEFT},
	DIRECTION_RIGHT: {DIRECTION_UP, DIRECTION_DOWN, DIRECTION_RIGHT},
}

type crucible struct {
	Loc         image.Point
	Dir         image.Point
	Consecutive int
}

func newCrucible(dir image.Point) *crucible {
	return &crucible{Loc: image.Point{}, Dir: dir, Consecutive: 0}
}

func (c *crucible) TryPush(dir image.Point, size int, maxCons int) *crucible {
	next := c.Loc.Add(dir)

	if next.Y < 0 || next.X < 0 || next.Y >= size || next.X >= size {
		return nil
	}

	nextCons := 1

	if dir == c.Dir {
		nextCons = c.Consecutive + 1
	}

	if dir == c.Dir && nextCons > maxCons {
		return nil
	}

	return &crucible{next, dir, nextCons}
}
