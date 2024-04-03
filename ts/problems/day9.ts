import { NEW_LINE } from "../inputReader.js";

const predictEnvironmentalInstabilities = (input: string) =>
  input
    .split(NEW_LINE)
    .map((line) => {
      const sequence = line.split(" ").map(Number);
      return predictNextSequenceValue(sequence, sequence.length);
    })
    .reduce((sum, val) => sum + val, 0);

const predictNextSequenceValue = (sequence: number[], readings: number): number => {
  const differences = Array.from({ length: readings })
    .fill(0)
    .map((_, i) => (i === 0 ? sequence[0] : sequence[i + 1] - sequence[i]));

  return differences[readings - 2] === 0
    ? sequence[readings - 1]
    : sequence[readings - 1] + predictNextSequenceValue(differences, readings - 1);
};

export default predictEnvironmentalInstabilities;
