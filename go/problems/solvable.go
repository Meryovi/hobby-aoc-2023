package problems

type Solvable[rt int | float32 | string] interface {
	Solve(input string) rt
}
