export class PriorityQueue<T> {
  #keys: number[] = [];
  #queue: Record<number, T[]> = {};

  enqueue(item: T, priority: number) {
    if (!(priority in this.#queue)) this.#queue[priority] = [];
    this.#queue[priority].push(item);

    const keyInx = this.getPriorityInsertIndex(priority);
    this.#keys.splice(keyInx, 0, priority);
  }

  dequeue(): DequeueResult<T> {
    const priority = this.#keys.shift();
    if (priority === undefined) return [false, undefined, -1];

    const element = this.#queue[priority].shift()!;
    return [true, element, priority];
  }

  private getPriorityInsertIndex(priority: number) {
    let low = 0;
    let high = this.#keys.length - 1;

    while (low <= high) {
      const mid = Math.floor((low + high) / 2);
      if (this.#keys[mid] < priority) low = mid + 1;
      else high = mid - 1;
    }

    return low;
  }
}

export class SerializedKeySet<T> {
  #set = new Set<string>();
  #serializer: ((item: T) => string) | undefined = undefined;

  constructor(customSerializer: ((item: T) => string) | undefined) {
    this.#serializer = typeof customSerializer === "function" ? customSerializer : undefined;
  }

  add(item: T) {
    const key = this.buildKey(item);
    if (this.#set.has(key)) return false;

    this.#set.add(key);
    return true;
  }

  has(item: T) {
    const key = this.buildKey(item);
    return this.#set.has(key);
  }

  private buildKey(item: T) {
    const key = this.#serializer ? this.#serializer(item) : JSON.stringify(item);
    return key;
  }
}

type DequeueResult<T> = [ok: false, item: undefined, priority: -1] | [ok: true, item: T, priority: number];
