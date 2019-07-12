using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransitionManager : Manager
{

    public Image progressBar;

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    int _targetScene;
    AsyncOperation _loadOperation;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void BeginLoadScene(int targetSceneBuildIndex)
    {
        _targetScene = targetSceneBuildIndex;
        _animator.SetTrigger("FadeIn");
    }



    public void LoadTargetScene()
    {
        if (_targetScene >= 0 && _loadOperation == null)
        {
            _loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_targetScene);
            _loadOperation.completed += OnSceneLoad;
        }
        else
        {
            OnSceneLoad(null);
        }
    }

    void OnSceneLoad(AsyncOperation op)
    {
        _animator.SetTrigger("FadeOut");
        _loadOperation = null;
        _targetScene = default;
    }

    // Update is called once per frame
    void Update()
    {
        if (_loadOperation != null)
        {
            progressBar.fillAmount = _loadOperation.progress;
        }
    }
}
