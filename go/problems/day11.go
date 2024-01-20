package problems

import (
	"image"
	"slices"
	"strings"
)

type Day11 struct{}

func (d Day11) Solve(input string) int64 {
	return sumDistanceBetweenGalaxies(input)
}

func sumDistanceBetweenGalaxies(input string) int64 {
	lines := strings.Split(input, NewLine)
	l := len(lines)

	emptyCols := make([]int, l)
	emptyRows := make([]int, l)
	galaxies := make([]*image.Point, 0)

	for i := range lines {
		emptyCols[i] = l - i
		emptyRows[i] = l - i
	}

	for y, line := range lines {
		for x, char := range line {
			if char == '#' {
				galaxies = append(galaxies, &image.Point{X: x, Y: y})
				emptyCols = removeElement(emptyCols, y)
				emptyRows = removeElement(emptyRows, x)
			}
		}
	}

	for _, galaxy := range galaxies { // Universe expansion.
		for _, col := range emptyCols {
			if col <= galaxy.Y {
				galaxy.Y++
			}
		}
		for _, row := range emptyRows {
			if row <= galaxy.X {
				galaxy.X++
			}
		}
	}

	var totalDist int64 = 0

	for i := 0; i < len(galaxies)-1; i++ {
		for j := i + 1; j < len(galaxies); j++ {
			var dist int64 = int64(abs(galaxies[j].X-galaxies[i].X) + abs(galaxies[j].Y-galaxies[i].Y))
			totalDist += dist
		}
	}

	return totalDist
}

func abs(x int) int {
	if x < 0 {
		return x * -1
	}
	return x
}

func removeElement(arr []int, el int) []int {
	if inx := slices.Index(arr, el); inx >= 0 {
		return slices.Delete(arr, inx, inx+1)
	}
	return arr
}
