package problems

type Solvable interface {
	Name() string
	Solve()
}
