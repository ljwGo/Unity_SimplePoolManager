using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePoolManager : MonoBehaviour {
    // ����List�����ͷǾ�̬ʱ, ���ܳ�����unity�ı༭������
    public List<GameObject> spawnPrefabConfig;
    public List<int> spawnQuantityConfig;
    private static Dictionary<string, SpawnPool> spawnPoolContainer = new Dictionary<string, SpawnPool>();

    void Start() {
        Init();
    }

    private void Init() {
        SpawnPool newPool;
        for (int i = 0; i < spawnPrefabConfig.Count; i++) {
            newPool = new SpawnPool(spawnPrefabConfig[i], spawnQuantityConfig[i]);
            // Instantiate�����Ķ������ƾ���(Clone)��׺
            //spawnPoolManager.Add(spawnPrefabConfig[i].name + "(Clone)", newPool);
            /* ʹ��Tag����GameObject��name��Ϊ���ֲ�ͬspawnPool��Keyֵ */
            spawnPoolContainer.Add(spawnPrefabConfig[i].tag, newPool);
        }
    }

    public static GameObject GetGameObj(GameObject go) {
        SpawnPool sp;

        if (spawnPoolContainer.TryGetValue(go.tag, out sp)) {
            return sp.GetGo();
        }
        else {
            throw new KeyNotFoundException("In all spawnPool, key " + go.tag + " isn't exist!");
        }
    }

    public static void SleepGameObj(GameObject go) {
        SpawnPool sp;

        if (spawnPoolContainer.TryGetValue(go.tag, out sp)) {
            sp.SleepGo(go);
        }
        else {
            throw new KeyNotFoundException("In all spawnPool, key " + go.tag + " isn't exist!");
        }
    }
}
