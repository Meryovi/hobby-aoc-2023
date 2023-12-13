package main

import (
	"aoc2023/problems"
	"flag"
	"fmt"
	"log"
)

func main() {
	prob := parseProblem()
	fmt.Println("Running problem '" + prob.Name() + "', answer:")
	prob.Solve()
}

func parseProblem() problems.Solvable {
	problems := map[int]func() problems.Solvable{
		1: func() problems.Solvable { return problems.Day1{} },
	}

	opt := *flag.Int("prob", 1, "problem number")
	flag.Parse()

	prob, found := problems[opt]
	if !found || prob == nil {
		log.Fatal("could not parse problem number, provide --prob arg with valid value")
	}

	return prob()
}
