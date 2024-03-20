package problems

import (
	"slices"
	"strings"
)

type Day20 struct{}

func (d Day20) Solve(input string) int64 {
	return countNumberOfPulsesSent(input)
}

func countNumberOfPulsesSent(input string) int64 {
	ps := NewPulseSender()

	for _, line := range strings.Split(input, NewLine) {
		ps.RegisterDestination(parsePulseReceivable(line))
	}

	ps.FeedModuleDeps()

	for i := 0; i < 1000; i++ {
		msg := pulseMessage{Source: "button", Destination: "broadcaster", Pulse: false}
		ps.FollowPulse(msg)
	}

	pulseValue := int64(ps.HighCount * ps.LowCount)
	return pulseValue
}

type pulseSender struct {
	HighCount    int
	LowCount     int
	destinations map[string]pulseReceivable
	queue        []pulseMessage
}

func NewPulseSender() *pulseSender {
	ps := pulseSender{}
	ps.destinations = make(map[string]pulseReceivable)
	ps.queue = make([]pulseMessage, 0)
	return &ps
}

func (ps *pulseSender) RegisterDestination(dest pulseReceivable) {
	ps.destinations[dest.Name()] = dest
}

func (ps *pulseSender) SendPulse(msg pulseMessage) {
	if msg.Pulse {
		ps.HighCount++
	} else {
		ps.LowCount++
	}
	ps.queue = append(ps.queue, msg)
}

func (ps *pulseSender) FollowPulse(msg pulseMessage) {
	ps.SendPulse(msg)

	for len(ps.queue) != 0 {
		next := ps.queue[0]
		ps.queue = slices.Delete(ps.queue, 0, 1)

		if mod, ok := ps.destinations[next.Destination]; ok {
			mod.ReceivePulse(next, ps)
		}
	}
}

func (ps *pulseSender) FeedModuleDeps() {
	for _, destination := range ps.destinations {
		conj, ok := destination.(*conjunctionModule)
		if !ok {
			continue
		}

		for key, curr := range ps.destinations {
			if slices.Contains(curr.Destinations(), destination.Name()) {
				conj.AddDependency(key)
			}
		}
	}
}

type pulseMessage struct {
	Source      string
	Destination string
	Pulse       bool
}

type pulseReceivable interface {
	Name() string
	Destinations() []string
	ReceivePulse(msg pulseMessage, ps *pulseSender)
}

func parsePulseReceivable(moduleStr string) pulseReceivable {
	inx := strings.Index(moduleStr, " -> ")

	typ := moduleStr[0]
	name := moduleStr[1:inx]
	dest := strings.Split(moduleStr[(inx+4):], ", ")

	if typ == 'b' { // Broadcaster
		return &broadcasterModule{"broadcaster", dest}
	} else if typ == '%' { // FlipFlop
		return &flipflopModule{name, dest, false}
	} else if typ == '&' { // Conjunction
		return &conjunctionModule{name, dest, make(map[string]bool)}
	}

	panic("invalid module type: " + string(typ))
}

type broadcasterModule struct {
	name         string
	destinations []string
}

func (bm *broadcasterModule) Name() string {
	return bm.name
}

func (bm *broadcasterModule) Destinations() []string {
	return bm.destinations
}

func (bm *broadcasterModule) ReceivePulse(msg pulseMessage, ps *pulseSender) {
	for _, dest := range bm.destinations {
		next := pulseMessage{Source: bm.name, Destination: dest, Pulse: msg.Pulse}
		ps.SendPulse(next)
	}
}

type flipflopModule struct {
	name         string
	destinations []string
	turnedOn     bool
}

func (fm *flipflopModule) Name() string {
	return fm.name
}

func (fm *flipflopModule) Destinations() []string {
	return fm.destinations
}

func (fm *flipflopModule) ReceivePulse(msg pulseMessage, ps *pulseSender) {
	if msg.Pulse {
		return
	}

	fm.turnedOn = !fm.turnedOn

	pulse := true
	if !fm.turnedOn {
		pulse = false
	}

	for _, dest := range fm.destinations {
		next := pulseMessage{Source: fm.name, Destination: dest, Pulse: pulse}
		ps.SendPulse(next)
	}
}

type conjunctionModule struct {
	name         string
	destinations []string
	memory       map[string]bool
}

func (cm *conjunctionModule) Name() string {
	return cm.name
}

func (cm *conjunctionModule) Destinations() []string {
	return cm.destinations
}

func (cm *conjunctionModule) ReceivePulse(msg pulseMessage, ps *pulseSender) {
	cm.memory[msg.Source] = msg.Pulse
	pulse := cm.getNextPulse()

	for _, dest := range cm.destinations {
		next := pulseMessage{Source: cm.name, Destination: dest, Pulse: pulse}
		ps.SendPulse(next)
	}
}

func (cm *conjunctionModule) AddDependency(source string) {
	cm.memory[source] = false
}

func (cm *conjunctionModule) getNextPulse() bool {
	for _, last := range cm.memory {
		if !last {
			return true
		}
	}
	return false
}
