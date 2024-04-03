export const iterate = function <T>(
  iteration: (current: number, value?: T) => T,
  checker: (current: number, value?: T) => boolean,
  value?: T
): { iterations: number; value?: T } {
  let iterations = 0;
  let endValue = value;
  for (; checker(iterations, value); iterations++) {
    endValue = iteration(iterations, endValue);
  }
  return { iterations, value: endValue };
};

export const loop = function <T>(iteration: (current: number) => T, iterations: number) {
  for (let i = 0; i < iterations; i++) {
    iteration(i);
  }
};
