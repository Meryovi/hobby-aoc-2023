package problems

import (
	"image"
	"strings"
)

type Day16 struct{}

func (d Day16) Solve(input string) int {
	return countEnergizedLightTiles(input)
}

func countEnergizedLightTiles(input string) int {
	matrix := strings.Split(input, NewLine)
	history := make(map[lightBeam]bool)
	unique := make(map[image.Point]bool)

	beam := lightBeam{Loc: image.Point{0, 0}, Dir: DIRECTION_RIGHT}
	startLightBeam(beam, matrix, history)

	for key := range history {
		unique[key.Loc] = true
	}

	total := len(unique)
	return total
}

func startLightBeam(beam lightBeam, matrix []string, history map[lightBeam]bool) {
	height := len(matrix)
	width := len(matrix[0])

	for !beam.IsOffset(width, height) {
		instruction := rune(matrix[beam.Loc.Y][beam.Loc.X])

		if _, exists := history[beam]; exists {
			break
		}

		history[beam] = true

		if instruction == '/' {
			if beam.Dir == DIRECTION_RIGHT {
				beam.Move(DIRECTION_UP)
			} else if beam.Dir == DIRECTION_LEFT {
				beam.Move(DIRECTION_DOWN)
			} else if beam.Dir == DIRECTION_UP {
				beam.Move(DIRECTION_RIGHT)
			} else if beam.Dir == DIRECTION_DOWN {
				beam.Move(DIRECTION_LEFT)
			}
		} else if instruction == '\\' {
			if beam.Dir == DIRECTION_RIGHT {
				beam.Move(DIRECTION_DOWN)
			} else if beam.Dir == DIRECTION_LEFT {
				beam.Move(DIRECTION_UP)
			} else if beam.Dir == DIRECTION_UP {
				beam.Move(DIRECTION_LEFT)
			} else if beam.Dir == DIRECTION_DOWN {
				beam.Move(DIRECTION_RIGHT)
			}
		} else if instruction == '|' {
			if beam.Dir == DIRECTION_RIGHT || beam.Dir == DIRECTION_LEFT {
				startLightBeam(beam.Split(DIRECTION_UP), matrix, history)
			} else {
				beam.Move(beam.Dir)
			}
		} else if instruction == '-' {
			if beam.Dir == DIRECTION_UP || beam.Dir == DIRECTION_DOWN {
				startLightBeam(beam.Split(DIRECTION_RIGHT), matrix, history)
			} else {
				beam.Move(beam.Dir)
			}
		} else {
			beam.Move(beam.Dir)
		}
	}
}

type lightBeam struct {
	Loc image.Point
	Dir image.Point
}

func (l *lightBeam) Move(dir image.Point) {
	l.Dir = dir
	l.Loc = l.Loc.Add(dir)
}

func (l *lightBeam) Split(dir image.Point) lightBeam {
	new := lightBeam{Loc: l.Loc, Dir: l.Dir}
	new.Move(dir)

	opp := dir.Mul(-1)
	l.Move(opp)

	return new
}

func (l *lightBeam) IsOffset(width int, height int) bool {
	return l.Loc.X < 0 || l.Loc.Y < 0 || l.Loc.X >= width || l.Loc.Y >= height
}
