using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePoolManager : MonoBehaviour {
    // 仅有List容器和非静态时, 才能出现在unity的编辑窗口中
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
            // Instantiate创建的对象名称具有(Clone)后缀
            //spawnPoolManager.Add(spawnPrefabConfig[i].name + "(Clone)", newPool);
            /* 使用Tag代替GameObject的name作为区分不同spawnPool的Key值 */
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
