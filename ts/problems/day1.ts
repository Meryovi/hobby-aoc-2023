const sumFirstLastDigits = (input: string) =>
  input
    .split("\n")
    .map((str) => {
      let [d1, d2] = [0, 0];
      for (let i = 0, j = str.length - 1; j >= 0 && (!d1 || !d2); i++, j--) {
        if (!d1 && Number(str[i])) d1 = Number(str[i]);
        if (!d2 && Number(str[j])) d2 = Number(str[j]);
      }
      return d1 * 10 + d2;
    })
    .reduce((acc, val) => acc + val, 0);

export const solve = sumFirstLastDigits;
