package problems

import (
	"image"
	"math"
	"slices"
	"strings"
)

type Day23 struct{}

func (d Day23) Solve(input string) int {
	return findLongestHikeTrail(input)
}

func findLongestHikeTrail(input string) int {
	lines := strings.Split(input, NewLine)
	height := len(lines)
	width := len(lines[0])

	grid := make([][]rune, height)
	start := image.Point{}
	end := image.Point{}

	for j, line := range lines {
		grid[j] = make([]rune, width)
		for i, char := range line {
			grid[j][i] = char
			if char == '.' {
				if j == 0 {
					start = image.Point{Y: j, X: i}
				} else if j == height-1 {
					end = image.Point{Y: j, X: i}
				}
			}
		}
	}

	graph := newMapGraph(start, end)
	graph.BuildGraph(grid)

	longest := graph.GetLongestTrail(start, 0)
	return longest
}

type mapGraph struct {
	Start, End image.Point
	Graph      map[image.Point][]mapEdge
}

func newMapGraph(start, end image.Point) *mapGraph {
	graph := make(map[image.Point][]mapEdge)
	return &mapGraph{Start: start, End: end, Graph: graph}
}

func (m *mapGraph) BuildGraph(grid [][]rune) {
	maxY := len(grid)
	maxX := len(grid[0])

	nodes := m.FindAllNodes(grid, maxY, maxX)

	for _, node := range nodes {
		queue := []mapState{{Current: node, Previous: node, Steps: 0}}

		for len(queue) > 0 {
			state := queue[0]
			queue = queue[1:]

			if state.Current != node && slices.Contains(nodes, state.Current) {
				m.Graph[node] = append(m.Graph[node], mapEdge{To: state.Current, Length: state.Steps})
				continue
			}

			next := make([]image.Point, 0)
			for _, direction := range ALL_DIRECTIONS {
				current := state.Current.Add(direction)
				if current == state.Previous || current.Y < 0 || current.Y >= maxY || current.X < 0 || current.X >= maxX {
					continue
				}
				if grid[current.Y][current.X] == '#' {
					continue
				}
				if grid[current.Y][current.X] == '.' {
					next = append(next, current)
				} else if grid[current.Y][current.X] == 'v' && current.Y-state.Current.Y > 0 {
					next = append(next, current)
				} else if grid[current.Y][current.X] == '>' && current.X-state.Current.X > 0 {
					next = append(next, current)
				}
			}

			for _, following := range next {
				queue = append(queue, mapState{Current: following, Previous: state.Current, Steps: state.Steps + 1})
			}
		}
	}
}

func (m *mapGraph) GetLongestTrail(current image.Point, length int) int {
	if current == m.End {
		return length
	}

	max := math.MinInt64

	for _, edge := range m.Graph[current] {
		nl := m.GetLongestTrail(edge.To, length+edge.Length)
		max = int(math.Max(float64(nl), float64(max)))
	}

	return max
}

func (m *mapGraph) FindAllNodes(grid [][]rune, maxY, maxX int) []image.Point {
	nodes := []image.Point{m.Start, m.End}

	for j := 0; j < maxY; j++ {
		for i := 0; i < maxX; i++ {
			if grid[j][i] == '#' {
				continue
			}
			if m.CountNeighbors(grid, maxY, maxX, j, i) >= 3 {
				nodes = append(nodes, image.Point{Y: j, X: i})
			}
		}
	}

	return nodes
}

func (m *mapGraph) CountNeighbors(grid [][]rune, maxY, maxX, y, x int) int {
	cnt := 0
	p := image.Point{Y: y, X: x}

	for _, direction := range ALL_DIRECTIONS {
		tp := p.Add(direction)
		if tp.Y >= 0 && tp.X >= 0 && tp.Y < maxY && tp.X < maxX && grid[tp.Y][tp.X] != '#' {
			cnt++
		}
	}

	return cnt
}

type mapEdge struct {
	To     image.Point
	Length int
}

type mapState struct {
	Current, Previous image.Point
	Steps             int
}
