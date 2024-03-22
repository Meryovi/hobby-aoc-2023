import test, { describe } from "node:test";
import assert from "node:assert";

import { solve } from "./day8.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 8", () => {
  test("test set 1 should yield expected result", () => {
    const input = readProblemInput("day8_1");
    const result = solve(input);
    assert.equal(result, 2);
  });

  test("test set 2 should yield expected result", () => {
    const input = readProblemInput("day8_2");
    const result = solve(input);
    assert.equal(result, 6);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day8_3");
    const result = solve(input);
    assert.equal(result, 14893);
  });
});
