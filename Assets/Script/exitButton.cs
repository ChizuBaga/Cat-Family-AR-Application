using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitARButtonWithCleanup : MonoBehaviour
{
    public string menuSceneName = "MainMenu"; // 主菜单场景名
    public ARImageTrackingAdjustable arManager; // AR 脚本引用

    public void ExitToMenu()
    {
        // 清理 AR 场景中的对象
        if (arManager != null)
        {
            arManager.ClearAllObjects();
        }

        // 切换到主菜单场景
        SceneManager.LoadScene(menuSceneName);
    }
}
