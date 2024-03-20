package problems

import (
	"sort"
	"strconv"
	"strings"
)

type Day22 struct{}

func (d Day22) Solve(input string) int {
	return countSafelyDisintegratedBlocks(input)
}

func countSafelyDisintegratedBlocks(input string) int {
	lines := strings.Split(input, NewLine)
	blocks := make([]Block, len(lines))

	for i, line := range lines {
		blocks[i] = parseBlock(line)
	}

	applyGravity(blocks)

	safelyDisintegrated := 0
	blockQueue := make([]Block, 0)
	supportChain := buildSupportChain(blocks)

	for _, disintegratingBlock := range blocks {
		falling := make(map[Block]bool)
		blockQueue = append(blockQueue[:0], disintegratingBlock)

		for len(blockQueue) > 0 {
			block := blockQueue[0]
			blockQueue = blockQueue[1:]

			falling[block] = true

			for _, above := range supportChain.BlocksAbove[block] {
				if isSubset(falling, supportChain.BlocksBelow[above]) {
					blockQueue = append(blockQueue, above)
				}
			}
		}

		if len(falling)-1 == 0 {
			safelyDisintegrated++
		}
	}

	return safelyDisintegrated
}

type Block struct {
	X, Y, Z LinearRange
}

func (b Block) Bottom() int {
	return b.Z.Start
}

func (b Block) Top() int {
	return b.Z.End
}

func parseBlock(blockString string) Block {
	parts := strings.FieldsFunc(blockString, func(c rune) bool {
		return c == ',' || c == '~'
	})

	numbers := make([]int, len(parts))
	for i, part := range parts {
		num, _ := strconv.Atoi(part)
		numbers[i] = num
	}

	return Block{
		X: LinearRange{Start: numbers[0], End: numbers[3]},
		Y: LinearRange{Start: numbers[1], End: numbers[4]},
		Z: LinearRange{Start: numbers[2], End: numbers[5]},
	}
}

func applyGravity(blocks []Block) {
	sort.Slice(blocks, func(i, j int) bool {
		return blocks[i].Bottom() < blocks[j].Bottom()
	})

	for i := range blocks {
		distance := 0

		for j := 0; j < i; j++ {
			if blocks[i].Intersects(blocks[j]) {
				distance = max(distance, blocks[j].Top())
			}
		}

		fall := blocks[i].Bottom() - distance - 1
		blocks[i].Z = blocks[i].Z.Reduce(fall)
	}
}

func (b Block) Intersects(other Block) bool {
	return b.X.Intersects(other.X) && b.Y.Intersects(other.Y)
}

func max(a, b int) int {
	if a > b {
		return a
	}
	return b
}

type LinearRange struct {
	Start, End int
}

func (l LinearRange) Intersects(other LinearRange) bool {
	return l.Start <= other.End && other.Start <= l.End
}

func (l LinearRange) Reduce(amount int) LinearRange {
	return LinearRange{l.Start - amount, l.End - amount}
}

type SupportChain struct {
	BlocksAbove map[Block][]Block
	BlocksBelow map[Block]map[Block]bool
}

func buildSupportChain(blocks []Block) SupportChain {
	blocksAbove := make(map[Block][]Block)
	blocksBelow := make(map[Block]map[Block]bool)

	for _, block := range blocks {
		blocksAbove[block] = []Block{}
		blocksBelow[block] = make(map[Block]bool)
	}

	for i, block := range blocks {
		for j := i + 1; j < len(blocks); j++ {
			zNeighbors := blocks[j].Bottom() == 1+blocks[i].Top()
			if zNeighbors && block.Intersects(blocks[j]) {
				blocksBelow[blocks[j]][block] = true
				blocksAbove[block] = append(blocksAbove[block], blocks[j])
			}
		}
	}

	return SupportChain{BlocksAbove: blocksAbove, BlocksBelow: blocksBelow}
}

func isSubset(set1, set2 map[Block]bool) bool {
	for k := range set2 {
		if _, ok := set1[k]; !ok {
			return false
		}
	}
	return true
}
