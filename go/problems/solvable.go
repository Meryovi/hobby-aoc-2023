package problems

type Solvable[rt int | int64 | float32 | float64 | string] interface {
	Solve(input string) rt
}
