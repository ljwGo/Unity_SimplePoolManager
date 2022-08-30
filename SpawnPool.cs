using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool{

    private int instantiateQuantity = 3;

    private GameObject prefab;
    private List<GameObject> activeGos = new List<GameObject>();
    private List<GameObject> sleepGos = new List<GameObject>();

    private SpawnPool() {
        ;
    }

    public SpawnPool(GameObject go, int preQuantity) {
        prefab = go;
        Init(go, preQuantity);
    }

    public void Init(GameObject go, int preQuantity) {
        GameObject newGo;

        for (int i = 0; i < preQuantity; i++) {
            newGo = Object.Instantiate(go);
            newGo.SetActive(false);
            sleepGos.Add(newGo);
        }
    }

    public GameObject GetGo() {
        GameObject go = null;

        if (sleepGos.Count > 0) {
            // 获取沉睡对象池中最后一个对象
            go = sleepGos[sleepGos.Count - 1];
            // 将最后一个沉睡的对象转入活跃对象池中
            activeGos.Add(go);
            // 将该对象从沉睡池中移除
            sleepGos.Remove(go);
            go.SetActive(true);
        }

        ExtendPreGo();

        return go;
    }

    public void SleepGo(GameObject go) {
        go.SetActive(false);
        activeGos.Remove(go);
        sleepGos.Add(go);
    }

    // 如果一个沉睡池中没有对象了, 那么他会准备好多个新得对象, 而非只调用一次instantiate
    private void ExtendPreGo() {
        if (sleepGos.Count < 1) {
            GameObject go;
            for (int i = 0; i < instantiateQuantity; i++) {
                go = Object.Instantiate(prefab);
                go.SetActive(false);
                sleepGos.Add(go);
            }
        }
    }
}
