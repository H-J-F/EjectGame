using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using EjectGame.Tools;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


public class AsyncLoadScene : MonoBehaviour
{
    public static AsyncLoadScene instance;

    private float loadingSpeed = 1;
    private AsyncOperation operation = null;
    
    //public Tips tips;

    void Awake()
    {
        instance = this;

        Object[] objAry = Resources.FindObjectsOfTypeAll<Material>();

        for (int i = 0; i < objAry.Length; ++i)
        {
            objAry[i] = null;//解除资源的引用
        }

        Object[] objAry2 = Resources.FindObjectsOfTypeAll<Texture>();

        for (int i = 0; i < objAry2.Length; ++i)
        {
            objAry2[i] = null;
        }

        //卸载没有被引用的资源
        Resources.UnloadUnusedAssets();

        //立即进行垃圾回收
        GC.Collect();
        GC.WaitForPendingFinalizers();//挂起当前线程，直到处理终结器队列的线程清空该队列为止
        GC.Collect();
    }

    // Use this for initialization
    void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        if (SceneManager.GetActiveScene().name == EjectTools.loadingSceneName)
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    public float GetLoadingProgress()
    {
        return operation == null ? 0 : operation.progress;
    }

    public void ActivateNextScene()
    {
        GameStateManager.instance.currentScene = GameStateManager.instance.nextScene;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        operation.allowSceneActivation = true;
    }

    IEnumerator AsyncLoading()
    {
        yield return new WaitForSeconds(1f);
        operation = SceneManager.LoadSceneAsync(GameStateManager.instance.nextScene.ToString());
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }
}
