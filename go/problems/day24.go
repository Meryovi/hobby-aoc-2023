package problems

import (
	"math"
	"strconv"
	"strings"
)

type Day24 struct{}

func (d Day24) Solve(input string) int {
	return countHailIntersections(input)
}

func countHailIntersections(input string) int {
	lines := strings.Split(input, NewLine)
	hailStones := make([]hailstone, len(lines))

	var minTime int64 = 0
	var maxTime int64 = 0

	for i, line := range lines {
		if i == 0 {
			inx := strings.Index(line, " ")
			minTime, _ = strconv.ParseInt(line[:inx], 10, 64)
			maxTime, _ = strconv.ParseInt(line[(inx+1):], 10, 64)
			continue
		}
		hailStones[i] = parseHailstone(i, line)
	}

	intersects := 0

	for i := 1; i < len(lines)-1; i++ {
		a := hailStones[i]
		for j := i + 1; j < len(lines); j++ {
			b := hailStones[j]
			if a.Intersects(b, minTime, maxTime) {
				intersects++
			}
		}
	}

	return intersects
}

type hailstone struct {
	Id       int
	Position LongPoint
	Velocity LongPoint
}

func parseHailstone(id int, hailstoneStr string) hailstone {
	separatorInx := strings.Index(hailstoneStr, " @ ")
	posStr := hailstoneStr[:separatorInx]
	velStr := hailstoneStr[(separatorInx + 3):]

	pos := parsePoint(posStr)
	vel := parsePoint(velStr)

	return hailstone{id, pos, vel}
}

func parsePoint(pointStr string) LongPoint {
	parts := parseNumbers64(pointStr, ", ")
	return LongPoint{X: parts[0], Y: parts[1]}
}

func (hs *hailstone) Intersects(other hailstone, minTime int64, maxTime int64) bool {
	// Intersection of dimensional lines formula, found it somewhere else.
	determinant := float64(hs.Velocity.X*other.Velocity.Y - hs.Velocity.Y*other.Velocity.X)

	if determinant == 0 {
		return false
	}

	t1 := float64(hs.Velocity.X*hs.Position.Y - hs.Velocity.Y*hs.Position.X)
	t2 := float64(other.Velocity.X*other.Position.Y - other.Velocity.Y*other.Position.X)

	xIntersect := (float64(other.Velocity.X)*t1 - float64(hs.Velocity.X)*t2) / determinant
	yIntersect := (float64(other.Velocity.Y)*t1 - float64(hs.Velocity.Y)*t2) / determinant

	if xIntersect < float64(minTime) || xIntersect > float64(maxTime) || yIntersect < float64(minTime) || yIntersect > float64(maxTime) {
		return false
	}

	thisIntersect := math.Signbit(xIntersect-float64(hs.Position.X)) != math.Signbit(float64(hs.Velocity.X))
	otherIntersect := math.Signbit(xIntersect-float64(other.Position.X)) != math.Signbit(float64(other.Velocity.X))

	if thisIntersect || otherIntersect {
		return false
	}

	return true
}

type LongPoint struct {
	X, Y int64
}
