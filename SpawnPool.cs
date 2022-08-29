using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool{
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
        GameObject go;

        if (sleepGos.Count > 0) {
            // ��ȡ��˯����������һ������
            go = sleepGos[sleepGos.Count - 1];
            // �����һ����˯�Ķ���ת���Ծ�������
            activeGos.Add(go);
            // ���ö���ӳ�˯�����Ƴ�
            sleepGos.Remove(go);
            go.SetActive(true);
            return go;
        }
        else {
            go = Object.Instantiate(prefab);
            activeGos.Add(go);
            return go;
        }
    }

    public void SleepGo(GameObject go) {
        activeGos.Remove(go);
        sleepGos.Add(go);
        go.SetActive(false);
    }
}
