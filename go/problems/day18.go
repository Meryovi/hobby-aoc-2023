package problems

import (
	"image"
	"strconv"
	"strings"
)

type Day18 struct{}

func (d Day18) Solve(input string) int {
	return calculateLavaCubicMeters(input)
}

func calculateLavaCubicMeters(input string) int {
	digger := image.Point{0, 0}
	total := 0

	for _, line := range strings.Split(input, NewLine) {
		parts := strings.Split(line, " ")
		dir := parts[0][0]
		steps, _ := strconv.Atoi(parts[1])

		next := getNextMovePoint(dir, steps)
		end := digger.Add(next)

		total += digger.X*end.Y - digger.Y*end.X + steps // Gauss
		digger = end
	}

	total = total/2 + 1
	return total
}

func getNextMovePoint(dir byte, steps int) image.Point {
	if dir == 'U' {
		return image.Point{0, -steps}
	} else if dir == 'D' {
		return image.Point{0, steps}
	} else if dir == 'L' {
		return image.Point{-steps, 0}
	} else if dir == 'R' {
		return image.Point{steps, 0}
	}
	return image.Point{}
}
