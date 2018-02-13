using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShufflerQueue<T> {
    private List<T> list;
    private Queue<T> queue = new Queue<T>();

    public ShufflerQueue(IEnumerable<T> list){
        this.list = new List<T>(list);
        GenerateQueue();
    }

    void GenerateQueue() {
        LinkedList<T> tempList = new LinkedList<T>(list);
        while(tempList.Count > 0) {
            var index = Random.Range(0, tempList.Count);
            var el = tempList.ElementAt(index);
            queue.Enqueue(el);
            tempList.Remove(el);
        }
    }

    public T next {
        get {
            if(queue.Count == 0) {
                GenerateQueue();
            }

            return queue.Dequeue();
        }
    }
}
