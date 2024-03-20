package problems

import (
	"math/rand"
	"strings"
)

type Day25 struct{}

func (d Day25) Solve(input string) int {
	return productOfComponentGroups(input)
}

func productOfComponentGroups(input string) int {
	lines := strings.Split(input, NewLine)
	edges := make([]Edge, 0, len(lines)*3)

	for _, line := range lines {
		connections := strings.Split(line, " ")

		fromHash := connections[0]
		for _, connection := range connections[1:] {
			toHash := connection
			edges = append(edges, Edge{From: fromHash, To: toHash})
		}
	}

	mergeAlgorithm := NewKargerAlgorithm(edges)
	cutSize, group1Size, group2Size := 0, 0, 0

	for cutSize != 3 {
		cutSize, group1Size, group2Size = mergeAlgorithm.FindMinimumCut()
	}

	return group1Size * group2Size
}

type Edge struct {
	From, To string
}

type KargerAlgorithm struct {
	initialVerticesCount int
	initialEdges         []Edge
	rng                  *rand.Rand
	merged               map[string][]string
}

func NewKargerAlgorithm(edges []Edge) *KargerAlgorithm {
	ka := &KargerAlgorithm{
		initialEdges: edges,
		rng:          rand.New(rand.NewSource(72)),
		merged:       make(map[string][]string),
	}

	uniqueVertices := make(map[string]bool)
	for _, e := range edges {
		if _, ok := uniqueVertices[e.From]; !ok {
			ka.initialVerticesCount++
			uniqueVertices[e.From] = true
		}
		if _, ok := uniqueVertices[e.To]; !ok {
			ka.initialVerticesCount++
			uniqueVertices[e.To] = true
		}
	}
	return ka
}

func (ka *KargerAlgorithm) FindMinimumCut() (cutSize, group1Count, group2Count int) {
	mergedEdges := ka.initialEdges
	mergedVerticesCount := ka.initialVerticesCount
	ka.merged = make(map[string][]string)

	for mergedVerticesCount > 2 && len(mergedEdges) > 0 {
		index := ka.rng.Intn(len(mergedEdges))
		from, to := mergedEdges[index].From, mergedEdges[index].To

		ka.merged[from] = append(ka.merged[from], to)
		if connections, ok := ka.merged[to]; ok {
			ka.merged[from] = append(ka.merged[from], connections...)
			delete(ka.merged, to)
		}

		newEdges := make([]Edge, 0)

		for _, edge := range mergedEdges {
			if edge.To == to {
				if edge.From != from {
					newEdges = append(newEdges, Edge{From: edge.From, To: from})
				}
			} else if edge.From == to {
				if from != edge.To {
					newEdges = append(newEdges, Edge{From: from, To: edge.To})
				}
			} else {
				newEdges = append(newEdges, edge)
			}
		}

		mergedEdges = newEdges
		mergedVerticesCount--
	}

	vertexGroups := make([][]string, 0, len(ka.merged))
	for _, value := range ka.merged {
		vertexGroups = append(vertexGroups, value)
	}
	return len(mergedEdges), len(vertexGroups[0]) + 1, len(vertexGroups[len(vertexGroups)-1]) + 1
}
