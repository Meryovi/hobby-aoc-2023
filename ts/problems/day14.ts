import { NEW_LINE } from "../inputReader.js";

const tiltPlatformAndSumRockWeight = (input: string) => tiltAndSumWeight(input.split(NEW_LINE).map((line) => line.split("")));

const tiltAndSumWeight = (matrix: string[][]) =>
  matrix.reduce((weightSum, _, j, matrix) => {
    for (let i = 0; i < matrix[0].length; i++) {
      if (matrix[j][i] != "O") continue;
      weightSum += matrix.length - j;

      let moves = 0;
      for (let k = j - 1; k >= 0; k--) {
        if (matrix[k][i] != ".") break;
        [matrix[k][i], matrix[j - moves][i]] = [matrix[j - moves][i], matrix[k][i]];
        weightSum++;
        moves++;
      }
    }
    return weightSum;
  }, 0);

export default tiltPlatformAndSumRockWeight;
