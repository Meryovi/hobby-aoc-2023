package main

import (
	"aoc2023/problems"
	"fmt"
)

func main() {
	var problem = problems.Day1{}
	fmt.Println("Running problem '" + problem.Name() + "', answer:")
	problem.Solve()
}
