package problems

import (
	"slices"
)

// Priority Queue. Layman's version.

type PriorityQueue[T any] struct {
	items []priorityItem[T]
}

type priorityItem[T any] struct {
	item     *T
	priority int
}

func NewPriorityQueue[T any](capacity int) *PriorityQueue[T] {
	q := &PriorityQueue[T]{}
	q.items = make([]priorityItem[T], 0, capacity)
	return q
}

func (q *PriorityQueue[T]) Len() int {
	return len(q.items)
}

func (q *PriorityQueue[T]) Push(item *T, priority int) {
	q.items = append(q.items, priorityItem[T]{item, priority})
	slices.SortFunc(q.items, func(a, b priorityItem[T]) int {
		return a.priority - b.priority
	})
}

func (q *PriorityQueue[T]) Pop() (*T, int) {
	if len(q.items) == 0 {
		return nil, -1
	}
	item := q.items[0]
	q.items[0] = priorityItem[T]{} // clear slice pointer
	q.items = q.items[1:]
	return item.item, item.priority
}
